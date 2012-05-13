using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

namespace SagaMap
{
    public abstract class MapItem : Npc
    {
        protected new ActorItem I;
        protected List<Item> NPCItem;

        public ActorItem ActorI
        {
            get
            {
                return I;
            }
            set
            {
                I = value;
            }
        }

        public MapItem()
        {
            this.isItem = true;
        }
        public override void OnCreate( bool success )
        {
            if( success )
            {
                I.invisble = false;
                map.OnActorVisibilityChange( I );
                this.Actor = new ActorNPC( "Mapitem" );
                this.Actor.mapID = this.ActorI.mapID;
                this.Actor.x = this.ActorI.x;
                this.Actor.y = this.ActorI.y;
                this.Actor.z = this.ActorI.z;
                this.Actor.region = this.ActorI.region;
                this.map.SendVisibleActorsToActor( I );
            }
        }

        public void AddNPCItem( int id )
        {
            if( this.NPCItem == null ) this.NPCItem = new List<Item>();
            this.NPCItem.Add( new Item( id ) );
        }

        public void ClearNPCItem()
        {
            if( this.NPCItem != null ) this.NPCItem.Clear();
        }

        public void SetAnimation( ActorPC pc, uint ani )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.ActorAnimation p = new SagaMap.Packets.Server.ActorAnimation();
            p.SetActor( this.ActorI.id );
            p.SetAnimation( ani );
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }
        public void SendLootList( ActorPC pc )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if( this.NPCItem == null ) this.NPCItem = new List<Item>();
            pc.CurNPCinv = this.NPCItem;
            Packets.Server.SendNpcInventory sendPacket = new Packets.Server.SendNpcInventory();
            sendPacket.SetActorID( pc.id );
            sendPacket.SetItems( this.NPCItem );
            eh.C.netIO.SendPacket(sendPacket, eh.C.SessionID);
        }

        public virtual void OnClicked( ActorPC pc )
        {
            this.Map.SendEventToAllActorsWhoCanSeeActor( Map.EVENT_TYPE.CHAT, new Map.ChatArgs( SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.NORMAL, string.Format( "I'm a Mapitem of type:{0}", this.ActorI.itemtype ) ), this.ActorI, false );
        }

        public virtual void OnQuestConfirmed( ActorPC pc, uint QuestID ) { }

        public virtual void OnOpen( ActorPC pc ) { }

        public override void OnReSpawn() { }

        public override void OnDie() { }
    }
}
