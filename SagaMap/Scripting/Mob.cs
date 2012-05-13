using System;
using System.Collections.Generic;
using System.Text;
using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using System.Xml;

using SagaMap.Tasks;

namespace SagaMap.Scripting
{
    public class Mob : Npc
    {
        //Status
        public enum Space { Land, Water, Amphibian }
        public uint MinAtk, MaxAtk;
        public ushort ASPD;
        public byte Cri;
        public ushort Size;
        public ushort mWalkSpeed, mRunSpeed;
        public float range;
        public int WalkBonus, RunBonus;

        public ushort WalkSpeed
        {
            get
            {
                if (this.ai != null)
                {
                    switch (ai.AIActivity)
                    {
                        case MobAI.Activity.LAZY:
                            {
                                return (ushort)(mWalkSpeed + WalkBonus);
                            }
                        case MobAI.Activity.BUSY:
                            return (ushort)((mWalkSpeed + WalkBonus) / 5);
                    }
                }
                return mWalkSpeed;
            }
            set
            {
                mWalkSpeed = value;
            }
        }

        public ushort RunSpeed
        {
            get
            {
                if (this.ai != null)
                {
                    switch (ai.AIActivity)
                    {
                        case MobAI.Activity.LAZY:
                            {
                                return (ushort)(mRunSpeed + RunBonus);
                            }
                        case MobAI.Activity.BUSY:
                            return (ushort)((mRunSpeed + RunBonus) / 5);
                    }
                }
                return mRunSpeed;
            }
            set
            {
                mRunSpeed = value;
            }
        }

        //Looting related
        public struct TimeSpan
        {
            public uint actorID;
            public DateTime time;
        }

        public Space LivingSpace = Space.Amphibian;

        public TimeSpan timeSignature = new TimeSpan();
        private SpawnOnce respawnTask;
        public DeleteCorpse corpsetask;
        public SpawnOnce RespawnTask { set { this.respawnTask = value; } }


        //AI Related
        public SagaMap.Tasks.MobAI ai;
        public int AIMode;
        public Dictionary<uint, ushort> Hate = new Dictionary<uint, ushort>();
        public Dictionary<uint, ushort> Damage = new Dictionary<uint, ushort>();        


        private int delay = -1;
        public int Delay { get { return this.delay; } set { this.delay = value; } }

        public override void OnActorSkillUse( Actor sActor, MapEventArgs args )
        {
            ai.OnSkillUse( sActor, args );
        }

        public void BeenAttacked(Actor sActor, MapEventArgs args)
        {
            ai.OnBeenAttacked(sActor, args);
        }

        public override void OnActorAppears( Actor dActor )
        {
            if( Hate.ContainsKey( dActor.id ) == false && dActor.type == ActorType.PC && IsAggresive() )
            {
                if( Global.Random.Next( 0, 99 ) <= 75 )//aggressive mobs are in ro2 randomly aggressive
                    Hate.Add( dActor.id, 20 );
            }
        }

        public override void OnActorDisappears( Actor dActor )
        {
            if( Hate.ContainsKey( dActor.id ) ) Hate.Remove( dActor.id );
        }

        public bool IsAggresive()
        {
            return ( AIMode | 1 ) == AIMode;
        }

        public bool IsSupporter()
        {
            return ( AIMode | 2 ) == AIMode;
        }

        public bool IsCallForHelp()
        {
            return ( AIMode | 4 ) == AIMode;
        }

        public override void OnActorChat( Actor cActor, MapEventArgs args )
        {

        }

        public override void OnDie()
        {
            base.OnDie();
            if( delay != -1 )
                respawnTask.Activate();
            corpsetask.Activate();
            ai.Pause();
            if( this.map.GetActor( this.timeSignature.actorID ) != null )
            {
                ActorPC pc = (ActorPC)this.map.GetActor( this.timeSignature.actorID );
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                Packet p = new Packet();//don't know its name,maybe for some animation.
                p.data = new byte[9];
                p.ID = 0x060E;
                p.PutUInt( this.Actor.id, 4 );
                p.PutByte( 4, 8 );
                eh.C.netIO.SendPacket(p, eh.C.SessionID);
                SagaDB.Quest.Quest quest = Quest.QuestsManager.GetActiveQuest( pc );
                if( quest != null )//Add a temporary loot for a specificial quest
                {
                    if( Quest.QuestsManager.MobQuestItem.ContainsKey( this.Actor.npcType ) )
                    {
                        foreach( Quest.QuestsManager.LootInfo i in Quest.QuestsManager.MobQuestItem[this.Actor.npcType] )
                        {
                            if( i.QID == quest.ID )
                            {
                                if( quest.Steps.ContainsKey( i.SID ) )
                                {
                                    if( quest.Steps[i.SID].Status == 1 )
                                    {
                                        if( this.Actor.NPCinv == null ) Actor.NPCinv = new List<Item>();
                                        int j = Global.Random.Next( 0, 9999 );
                                        if( j < i.rate )
                                            this.Actor.NPCinv.Add( new Item( i.itemID ) );
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                /*if (eh.C.QuestMobItem != null)
                {
                    if (eh.C.QuestMobItem.ContainsKey(this.Actor.npcType))
                    {
                        if (this.Actor.NPCinv == null) Actor.NPCinv = new List<Item>();
                        this.Actor.NPCinv.Add(ItemFactory.GetItem((int)eh.C.QuestMobItem[this.Actor.npcType]));
                    }
                }*/
            }
        }

        public void AddLoot( int id )
        {
            if( this.Actor.NPCinv == null ) this.Actor.NPCinv = new List<Item>();
            Item item = null;
            try
            {
                item = new Item(id);
            }
            catch(Exception)
            {
                Logger.ShowWarning("Unknown Item ID(" + id + ") for adding loot for mob:" + this.Actor.npcType);
                return;
            }
            item.creatorName = this.Name;
            this.Actor.NPCinv.Add(item);
        }
    }

    
}
