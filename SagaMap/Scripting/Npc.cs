using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

using SagaMap.Manager;
using SagaMap.Packets.Server;

namespace SagaMap
{
    public class Npc : ActorEventHandler
    {
        public enum Functions { EverydayConversation = 1, LocationGuide, OfficialQuest, PersonalQuest, ScenarioQuest, EventQuest, Shop = 10, Kafra, Regenbogen, BookStore, U2, EnterShip, LeaveShip, EnterTrain, LeaveTrain, Smith = 35, Supply = 42, AcceptPersonalRequest = 43 };
        public enum StepStatus { Hiden, Active, Completed };
        public enum IconType { None, Personal, Official, Both };
        protected Map map;
        protected uint money = 0;
        public Map Map { get { return map; } set { map = value; } }
        protected ActorNPC I;
        public ActorNPC Actor { get { return I; } set { I = value; } }

        public bool isItem = false;
        public string MapName;
        public uint Type;
        public uint ID;
        public string Name;
        public float StartX;
        public float StartY;
        public float StartZ;
        public float[] SavePoint;
        public int Startyaw;
        public bool Persistent = false;
        public int Version = 1;
        protected uint lastStep = 0;
        private List<QuestReqirement> QuestList = new List<QuestReqirement>();
        public Dictionary<byte, Delegate> Functable = new Dictionary<byte, Delegate>();
        private Scripting.QuestGroup questgroup;
        public Dictionary<uint, SagaDB.Quest.Quest> Questtable = new Dictionary<uint, SagaDB.Quest.Quest>();
        public Dictionary<uint, SagaDB.Quest.Quest> PersonalQuesttable = new Dictionary<uint, SagaDB.Quest.Quest>();
        public List<QuestReqirement> PersonalQuests = new List<QuestReqirement>();

        public uint SupplyMenuID;
        public Dictionary<uint, List<SendSupplyList.SupplyItem>> SupplyProducts = new Dictionary<uint, List<SendSupplyList.SupplyItem>>();
        public Dictionary<uint, List<SendSupplyList.SupplyItem>> SupplyMatrials = new Dictionary<uint, List<SendSupplyList.SupplyItem>>();

        public class QuestReqirement
        {
            public uint clv;
            public uint previousquest;
            public uint QID;
            public uint script;
        }

        public delegate void func( ActorPC pc );
        public delegate void rewardfunc(ActorPC pc, uint QID);
        public delegate void personalfunc(ActorPC pc, uint QID, byte button);

        private SagaDB.Quest.Quest Quest = null;

        public Npc() { }

        public Npc( ActorNPC I, Map M )
        {
            this.I = I;
            this.map = M;
        }

        public virtual void OnCreate( bool success )
        {
            if( success )
            {
                I.invisble = false;
                map.OnActorVisibilityChange( I );
                this.map.SendVisibleActorsToActor( I );
            }
        }

        public virtual void OnInit() { }

        public virtual void Dispose() { }

        public virtual void OnReSpawn()
        {
            I.HP = I.maxHP;
            I.state = 0;
            I.stance = Global.STANCE.REBORN;
        }

        public virtual void OnMapLoaded() { }

        public virtual void OnDie()
        {
            I.state = 0;
            I.stance = Global.STANCE.DIE;
        }

        public void OnSelectButton( ActorPC sActor, int button )
        {
            Functions func = (Functions)button;
            //Logger.CurrentLogger.WriteLog("NPC", string.Format("Player:{0} invoked the {1} function of NPC:{2}", sActor.name, func.ToString(), this.ToString()));
            try
            {
                SagaDB.Quest.Quest quest;
                switch (func)
                {
                    case Functions.EverydayConversation:
                        if (Functable.ContainsKey((byte)button)) Functable[(byte)button].DynamicInvoke(sActor);
                        break;
                    case Functions.Shop:
                        //Temporary info. Should lookup npc inventory from DB?
                        sActor.e.OnSendShopList(this.Actor.NPCinv, this.money, this.Actor.id);
                        break;
                    case Functions.Kafra:
                        HandelKafra(sActor);
                        break;
                    case Functions.Regenbogen:
                        HandelRegenbogen(sActor);
                        break;
                    case Functions.BookStore:
                        HandelBookstore(sActor);
                        break;
                    case Functions.AcceptPersonalRequest:
                        HandelPersonalRequest(sActor);
                        break;
                    case Functions.Supply:
                        HandelSupply(sActor);
                        break;
                    case Functions.PersonalQuest:
                        if (this.questgroup != null)
                        {
                            quest = SagaMap.Quest.QuestsManager.GetActivePersonalQuest(sActor);
                            if (questgroup.handlers.ContainsKey((int)quest.ID))
                            {
                                questgroup.handlers[(int)quest.ID].ProcessQuest(sActor, this, quest);
                            }
                        }
                        else
                            if (Functable.ContainsKey((byte)button)) Functable[(byte)button].DynamicInvoke(sActor);
                        break;
                    case Functions.OfficialQuest:
                        if (this.questgroup != null)
                        {
                            quest = SagaMap.Quest.QuestsManager.GetActiveQuest(sActor);
                            if (questgroup.handlers.ContainsKey((int)quest.ID))
                            {
                                questgroup.handlers[(int)quest.ID].ProcessQuest(sActor, this, quest);
                            }
                        }
                        else
                            if (Functable.ContainsKey((byte)button)) Functable[(byte)button].DynamicInvoke(sActor);
                        break;
                        
                    case Functions.EnterShip:
                        if (Functable.ContainsKey((byte)button)) Functable[(byte)button].DynamicInvoke(sActor);
                        break;
                    case Functions.Smith:
                        HandelSmith(sActor);
                        break;
                    default:
                        if (Functable.ContainsKey((byte)button)) Functable[(byte)button].DynamicInvoke();
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowWarning("Script Error: NPC:" + this.ToString() + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, Logger.CurrentLogger);
            }
        }
        /// <summary>
        /// Add a function of a npc
        /// </summary>
        /// <param name="func">Function</param>
        /// <param name="dele">Handling Method</param>
        public void AddButton( Functions func, func dele )
        {
            byte[] tmp = new byte[this.Actor.Attribute.icons.Length + 1];
            this.Actor.Attribute.icons.CopyTo( tmp, 0 );
            tmp[this.Actor.Attribute.icons.Length] = (byte)func;
            this.Actor.Attribute.icons = tmp;
            this.Functable.Add( (byte)func, dele );
        }

        /// <summary>
        /// Add a function of a npc
        /// </summary>
        /// <param name="func">Function</param>
        public void AddButton( Functions func )
        {
            byte[] tmp = new byte[this.Actor.Attribute.icons.Length + 1];
            this.Actor.Attribute.icons.CopyTo( tmp, 0 );
            tmp[this.Actor.Attribute.icons.Length] = (byte)func;
            this.Actor.Attribute.icons = tmp;
        }

        /// <summary>
        /// Add a function of a npc
        /// </summary>
        /// <param name="func">Function</param>
        /// <param name="dele">Handling Method</param>
        /// <param name="hide">Should this button be hiden in NPC chat box</param>
        public void AddButton( Functions func, Delegate dele, bool hide )
        {
            this.Functable.Add( (byte)func, dele );
        }

        /// <summary>
        /// Set the Quest Group handler for this npc
        /// </summary>
        /// <param name="group"></param>
        public void SetQuestGroup(Scripting.QuestGroup group)
        {
            this.questgroup = group;
        }

        public void AddNavPoint(uint QID, uint SID, byte mapID, uint npcType, float x, float y, float z)
        {
            Dictionary<uint, List<SagaMap.Quest.QuestsManager.WayPointInfo>> list;
            if (SagaMap.Quest.QuestsManager.WayPoints.ContainsKey(QID))
            {
                list = SagaMap.Quest.QuestsManager.WayPoints[QID];
            }
            else
            {
                list = new Dictionary<uint, List<SagaMap.Quest.QuestsManager.WayPointInfo>>();
                SagaMap.Quest.QuestsManager.WayPoints.Add(QID, list);
            }
            List<SagaMap.Quest.QuestsManager.WayPointInfo> list2;
            if (list.ContainsKey(SID))
            {
                list2 = list[SID];
            }
            else
            {
                list2 = new List<SagaMap.Quest.QuestsManager.WayPointInfo>();
                list.Add(SID, list2);
            }
            SagaMap.Quest.QuestsManager.WayPointInfo info = new SagaMap.Quest.QuestsManager.WayPointInfo();
            info.mapID = mapID;
            info.QID = QID;
            info.SID = SID;
            info.npcType = npcType;
            info.x = x;
            info.y = y;
            info.z = z;
            list2.Add(info);
        }

        /// <summary>
        /// Add a Quest to a npc
        /// </summary>
        /// <param name="id">Quest ID</param>
        public void AddQuest(uint id)
        {
            AddQuest(id, 0, 0);
        }

        public void AddQuest(uint qid, uint clv, uint previous)
        {
            QuestReqirement req = new QuestReqirement();
            req.QID = qid;
            req.previousquest = previous;
            req.clv = clv;
            this.QuestList.Add(req);
        }

        public void AddPersonalQuest(uint qid, uint clv, uint previous, uint script)
        {
            QuestReqirement quest = new QuestReqirement();
            quest.QID = qid;
            quest.clv = clv;
            quest.script = script;
            quest.previousquest = previous;
            this.PersonalQuests.Add(quest);
        }

        /// <summary>
        /// Add a Quest Step trigger to a npc
        /// </summary>
        /// <param name="QID">Quest ID</param>
        /// <param name="SID">Step ID</param>
        /// <param name="s">Step Status</param>
        public void AddQuestStep( uint QID, uint SID, StepStatus s )
        {
            SagaDB.Quest.Quest quest;
            if( Questtable.ContainsKey( QID ) )
                quest = Questtable[QID];
            else
            {
                quest = new SagaDB.Quest.Quest();
                Questtable.Add( QID, quest );
            }
            if( quest.Steps.ContainsKey( SID ) ) return;
            else
            {
                SagaDB.Quest.Step step = new SagaDB.Quest.Step();
                step.ID = SID;
                step.Status = (byte)s;
                quest.Steps.Add( SID, step );
            }

        }

        /// <summary>
        /// Add a Quest Step trigger to a npc
        /// </summary>
        /// <param name="QID"></param>
        /// <param name="SID"></param>
        public void AddQuestStep(uint QID, uint SID)
        {
            AddQuestStep(QID, SID, StepStatus.Active);
        }

        public void AddPersonalQuestStep(uint QID, uint SID)
        {
            AddPersonalQuestStep(QID, SID, StepStatus.Active);
        }

        public void AddPersonalQuestStep(uint QID, uint SID, StepStatus s)
        {
            SagaDB.Quest.Quest quest;
            if (PersonalQuesttable.ContainsKey(QID))
                quest = PersonalQuesttable[QID];
            else
            {
                quest = new SagaDB.Quest.Quest();
                PersonalQuesttable.Add(QID, quest);
            }
            if (quest.Steps.ContainsKey(SID)) return;
            else
            {
                SagaDB.Quest.Step step = new SagaDB.Quest.Step();
                step.ID = SID;
                step.Status = (byte)s;
                quest.Steps.Add(SID, step);
            }
        }

        public List<uint> GetAvaluableQuest(ActorPC pc)
        {
            SagaDB.Quest.Quest quest = SagaMap.Quest.QuestsManager.GetActiveQuest(pc);
            List<uint> IDs = new List<uint>();
            foreach (QuestReqirement i in this.QuestList)
            {
                if (pc.QuestTable.ContainsKey(i.QID))
                {
                    if (quest != null)
                    {
                        if (quest.ID == i.QID) continue;
                        else
                            if (pc.cLevel >= i.clv && !SagaMap.Quest.QuestsManager.ifCompletedQuest(pc, i.QID) && (SagaMap.Quest.QuestsManager.ifCompletedQuest(pc, i.previousquest) || i.previousquest == 0))
                                IDs.Add(i.QID);
                    }
                    else
                    {
                        if (pc.cLevel >= i.clv && !SagaMap.Quest.QuestsManager.ifCompletedQuest(pc, i.QID) && (SagaMap.Quest.QuestsManager.ifCompletedQuest(pc, i.previousquest) || i.previousquest == 0))
                            IDs.Add(i.QID);
                    }
                }
                else
                {
                    if (pc.cLevel >= i.clv && !SagaMap.Quest.QuestsManager.ifCompletedQuest(pc, i.QID) && (SagaMap.Quest.QuestsManager.ifCompletedQuest(pc, i.previousquest) || i.previousquest == 0))
                        IDs.Add(i.QID);
                }
            }
            return IDs;
        }

        public QuestReqirement GetAvaluablePersonalQuest(ActorPC pc)
        {
            SagaDB.Quest.Quest quest = SagaMap.Quest.QuestsManager.GetActivePersonalQuest(pc);
            foreach (QuestReqirement i in this.PersonalQuests)
            {
                if (pc.PersonalQuestTable.ContainsKey(i.QID))
                {
                    if (quest != null)
                    {
                        if (quest.ID == i.QID) return i;
                        else
                            if (pc.cLevel >= i.clv && !SagaMap.Quest.QuestsManager.ifCompletedPersonalQuest(pc, i.QID) && (SagaMap.Quest.QuestsManager.ifCompletedPersonalQuest(pc, i.previousquest) || i.previousquest == 0))
                                return i;
                    }
                    else
                    {
                        if (pc.cLevel >= i.clv && !SagaMap.Quest.QuestsManager.ifCompletedPersonalQuest(pc,i.QID) && (SagaMap.Quest.QuestsManager.ifCompletedPersonalQuest(pc, i.previousquest) || i.previousquest == 0))
                            return i;
                    }
                }
                else
                {
                    if (pc.cLevel >= i.clv && !SagaMap.Quest.QuestsManager.ifCompletedPersonalQuest(pc, i.QID) && (SagaMap.Quest.QuestsManager.ifCompletedPersonalQuest(pc, i.previousquest) || i.previousquest == 0))
                        return i;
                }
            }
            
            return null;
        }


        /// <summary>
        /// Get the Step Status of a Quest/Step
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="QID">Quest ID</param>
        /// <param name="SID">Step ID</param>
        /// <returns></returns>
        public StepStatus GetQuestStepStatus( ActorPC pc, uint QID, uint SID )
        {
            return (StepStatus)SagaMap.Quest.QuestsManager.GetQuestStepStatus( pc, QID, SID );
        }

        /// <summary>
        /// Get the current active Quest of a player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <returns></returns>
        public uint GetActiveQuest( ActorPC pc )
        {
            SagaDB.Quest.Quest quest = SagaMap.Quest.QuestsManager.GetActiveQuest( pc );
            if( quest != null )
                return quest.ID;
            else
                return 0;
        }

        /// <summary>
        /// Update the Step Status of a player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="QID">Quest ID</param>
        /// <param name="SID">Step ID</param>
        /// <param name="s">Status</param>
        public void UpdateQuest( ActorPC pc, uint QID, uint SID, StepStatus s )
        {
            SagaMap.Quest.QuestsManager.SetQuestStepStatus( pc, QID, SID, (byte)s );
        }

        /// <summary>
        /// Complete a Quest
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="QID">Quest</param>
        public void QuestCompleted( ActorPC pc, uint QID )
        {
            Packets.Server.QuestCompleted p = new SagaMap.Packets.Server.QuestCompleted();
            p.SetQuestID( QID );
            SendPacket(pc, p);
            //eh.C.QuestMobItem = null;
        }

        /// <summary>
        /// Delete a Quest
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="QID">Quest ID</param>
        public void RemoveQuest( ActorPC pc, uint QID )
        {
            Packets.Server.RemoveQuest p = new SagaMap.Packets.Server.RemoveQuest();
            p.SetQuestID( QID );
            SendPacket(pc, p);
        }

        /// <summary>
        /// The the Reward handling method
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="func">Delegate to a Method</param>
        public void SetReward( ActorPC pc, rewardfunc func )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.RewardFunc = func;
        }

        public void AddRewardChoice(ActorPC pc, int itemid)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            Item item = new Item(itemid);
            item.creatorName = this.Name;
            if (eh.C.RewardChoice == null) eh.C.RewardChoice = new List<Item>();
            eh.C.RewardChoice.Add(item);
        }

        /// <summary>
        /// Give EXP to a player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="cexp">CEXP</param>
        /// <param name="jexp">JEXP</param>
        public void GiveExp( ActorPC pc, uint cexp, uint jexp )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            pc.cExp += cexp;
            pc.jExp += jexp;
            eh.C.SendCharStatus(0);
            ExperienceManager.Instance.CheckExp(eh.C, ExperienceManager.LevelType.CLEVEL);
			ExperienceManager.Instance.CheckExp(eh.C, ExperienceManager.LevelType.JLEVEL);

        }


        /// <summary>
        /// Give money to a Player
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="amount"></param>
        public void GiveZeny( ActorPC pc, uint amount )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            pc.zeny += amount;
            eh.C.SendZeny();
        }

        /// <summary>
        /// Take money from a player
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="amount"></param>
        public void TakeZeny( ActorPC pc, uint amount )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if( pc.zeny > amount )
                pc.zeny -= amount;
            else
                pc.zeny = 0;
            eh.C.SendZeny();
        }

        /// <summary>
        /// Updated the icon shown above the current npc's head
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="i">Icon type</param>
        public void UpdateIcon(ActorPC pc)
        {
            Packets.Server.ActorUpdateIcon p = new SagaMap.Packets.Server.ActorUpdateIcon();
            p.SetActor(this.Actor.id);
            p.SetIcon(SagaMap.Quest.QuestsManager.GetNPCIcon(pc, this));
            SendPacket(pc, p);
        }

        /// <summary>
        /// Check if a player has got a certain Quest
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="id">Quest ID</param>
        /// <returns></returns>
        public bool IfGotQuest( ActorPC pc, uint id )
        {
            return SagaMap.Quest.QuestsManager.ifGotQuest( pc, id );
        }

        /// <summary>
        /// Send the QuestList of the current NPC
        /// </summary>
        /// <param name="pc"></param>
        public void SendQuestList( ActorPC pc )
        {
            Packets.Server.SendQuestList p = new SagaMap.Packets.Server.SendQuestList();
            if( this.isItem )
            {
                MapItem item = (MapItem)this;
                p.SetActor( item.ActorI.id );
                pc.LastMissionBoard = item.ActorI;
            }
            else p.SetActor( this.Actor.id );
            List<uint> IDs = GetAvaluableQuest(pc);
            p.SetQuestList( IDs );
            SendPacket(pc, p);
        }

        /// <summary>
        /// Add a Quest Step
        /// </summary>
        /// <param name="QuestID"></param>
        /// <param name="StepID"></param>
        public void AddStep( uint QuestID, uint StepID )
        {
            if( Quest == null )
            {
                Quest = new SagaDB.Quest.Quest();
                Quest.ID = QuestID;
                lastStep = 0;
            }
            SagaDB.Quest.Step step = new SagaDB.Quest.Step();
            step.ID = StepID;
            if( lastStep != 0 )
            {
                step.Status = 0;
                step.step = 2;
                Quest.Steps[lastStep].nextStep = StepID;
            }
            else
            {
                step.Status = 1;
                step.step = 2;
            }
            Quest.Steps.Add( StepID, step );
            lastStep = StepID;
        }

        /// <summary>
        /// Add a Quest Item
        /// </summary>
        /// <param name="QID">Quest Item</param>
        /// <param name="SID">Step Item</param>
        /// <param name="itemID">Item ID</param>
        /// <param name="ammount"></param>
        public void AddQuestItem( uint QID, uint SID, uint itemID, byte ammount )
        {
            SagaMap.Quest.QuestsManager.AddQuestItem(QID, SID, 0, itemID, ammount);
        }

        public void AddQuestItem(uint QID, uint SID, byte SubSID, uint itemID, byte ammount)
        {
            SagaMap.Quest.QuestsManager.AddQuestItem(QID, SID, SubSID, itemID, ammount);
        }

        public void AddEnemyInfo(uint QID, uint SID,List<uint> MobID, byte ammount)
        {
            this.AddEnemyInfo(QID, SID, 0, MobID, ammount);
        }

        public void AddEnemyInfo(uint QID, uint SID, byte SubSID, List<uint> MobID, byte ammount)
        {
            SagaMap.Quest.QuestsManager.AddEnemyInfo(QID, SID, SubSID, MobID, ammount);
        }

        /// <summary>
        /// Add a temporary loot for a Quest
        /// </summary>
        /// <param name="mobid"></param>
        /// <param name="QID"></param>
        /// <param name="SID"></param>
        /// <param name="ItemID"></param>
        /// <param name="rate"></param>
        public void AddMobLoot( uint mobid, uint QID, uint SID, int ItemID, uint rate )
        {
            SagaMap.Quest.QuestsManager.AddMobLoot( mobid, QID, SID, ItemID, rate );
            /*ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.QuestMobItem.Add(mobid, lootid);*/
        }

        /// <summary>
        /// Start a quest
        /// </summary>
        /// <param name="pc"></param>
        public void QuestStart( ActorPC pc )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            pc.QuestTable.Add( Quest.ID, Quest );
            MapServer.charDB.NewQuest(pc, SagaDB.Quest.QuestType.OfficialQuest, Quest);
            eh.C.SendQuestInfo();
            Quest = null;
            //eh.C.QuestMobItem = new Dictionary<uint, uint>();
        }

        public void PersonalQuestStart(ActorPC pc)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if (Quest == null) return;
            if (pc.PersonalQuestTable.ContainsKey(Quest.ID))
            {
                MapServer.charDB.DeleteQuest(pc, pc.PersonalQuestTable[Quest.ID]);
                pc.PersonalQuestTable.Remove(Quest.ID);
            }
            pc.PersonalQuestTable.Add(Quest.ID, Quest);
            MapServer.charDB.NewQuest(pc, SagaDB.Quest.QuestType.PersonalQuest, Quest);
            eh.C.SendQuestInfo();
            Packets.Server.NPCNote p = new SagaMap.Packets.Server.NPCNote();
            p.SetQuestID(Quest.ID);
            SagaDB.Quest.Step[] steps = new SagaDB.Quest.Step[Quest.Steps.Count];
            Quest.Steps.Values.CopyTo(steps, 0);
            p.SetSetpID(steps[0].ID);
            SendPacket(pc, p);
            Packets.Server.QuestNote p1 = new SagaMap.Packets.Server.QuestNote();
            p1.SetQuestID(Quest.ID);
            p1.SetUnknown(1);
            SendPacket(pc, p1);
            Quest = null;
            //eh.C.QuestMobItem = new Dictionary<uint, uint>();
        }

        /// <summary>
        /// Send a Navigation Point to a Player
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="QID"></param>
        /// <param name="npcType"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SendNavPoint( ActorPC pc, uint QID, uint npcType, float x, float y, float z )
        {
            Packets.Server.SendNavPoint p = new SagaMap.Packets.Server.SendNavPoint();
            p.SetQuestID( QID );
            p.SetNPCType( npcType );
            p.SetPosition( x, y, z );
            SendPacket(pc, p);
        }

        public void SendNavPoint(ActorPC pc)
        {
            SagaMap.Quest.QuestsManager.SendNavPoint(pc);
        }


        /// <summary>
        /// Remove Navigation Point of a Quest
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="QID"></param>
        public void RemoveNavPoint( ActorPC pc, uint QID )
        {
            Packets.Server.RemoveNavPoint p = new SagaMap.Packets.Server.RemoveNavPoint();
            p.SetQuestID( QID );
            SendPacket(pc, p);
        }

        /// <summary>
        /// Set the chating script for current NPC
        /// </summary>
        /// <param name="script"></param>
        public void SetScript( uint script )
        {
            this.Actor.Attribute.script = script;
        }

        /// <summary>
        /// Speak out a certain script
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="script"></param>
        public void NPCChat( ActorPC pc, uint script )
        {
            if (script != 0)
            {
                Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
                p.SetActor(this.Actor.id);
                p.SetIcons((byte)this.Actor.Attribute.icons.Length, this.Actor.Attribute.icons);
                p.SetU(1);
                p.SetScript(script);
                p.SetUnknown(1);
                SendPacket(pc, p);
            }
            else
            {
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                eh.C.OnNPCChat(this);
            }
        }

        public void NPCSpeech(ActorPC pc, uint script)
        {
            Packets.Server.NPCSpeech p = new SagaMap.Packets.Server.NPCSpeech();
            p.SetActor(this.Actor.id);
            p.SetScript(script);
            SendPacket(pc, p);
        }

        /// <summary>
        /// Make a NPC to speak something
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="npc"></param>
        /// <param name="script"></param>
        public static void NPCChat( ActorPC pc, ActorNPC npc, uint script )
        {
            if (pc == null || npc == null) return;
            Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
            p.SetActor( npc.id );
            p.SetIcons( (byte)npc.Attribute.icons.Length, npc.Attribute.icons );
            p.SetU( 1 );
            p.SetScript( script );
            p.SetUnknown( 1 );
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }

        protected static void SendPacket(ActorPC pc, Packet p)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }

        private void HandelPersonalRequest(ActorPC pc)
        {
            QuestReqirement quest = this.GetAvaluablePersonalQuest(pc);
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e; 
            if (quest != null)
            {
                Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
                p.SetU(43);
                p.SetScript(quest.script);
                p.SetActor(this.Actor.id);
                p.SetIcons(0, null);
                p.SetUnknown(1);
                eh.C.netIO.SendPacket(p, eh.C.SessionID);
            }
            eh.C.OnNPCChat(this);

        }

        private void HandelSmith( ActorPC pc )
        {
            Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
            p.SetActor( this.Actor.id );
            p.SetIcons( 4, new byte[4] { 50, 52, 53, 55 } );
            p.SetU( 35 );//marked as Smith function
            p.SetScript( 1746 );
            p.SetUnknown( 2 );
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);

        }
        private void HandelBookstore( ActorPC pc )
        {
            Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
            p.SetActor( this.Actor.id );
            p.SetIcons( 3, new byte[3] { 31, 32, 33 } );
            p.SetU( 13 );//marked as Bookstore function
            p.SetScript( 1758 );
            p.SetUnknown( 2 );
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);

        }

        private void HandelKafra(ActorPC pc)
        {
            Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
            p.SetActor(this.Actor.id);
            p.SetIcons(3, new byte[3] { 10, 11, 12 });
            //p.SetIcons( 1, new byte[1] { 10 } );
            p.SetU(11);//marked as Kafra function
            p.SetScript(2507);
            p.SetUnknown(2);
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
            
        }

        private void HandelSupply(ActorPC pc)
        {
            Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
            p.SetActor(this.Actor.id);
            p.SetIcons((byte)this.Actor.Attribute.icons.Length, this.Actor.Attribute.icons);
            //p.SetIcons( 1, new byte[1] { 10 } );
            p.SetU(42);//marked as Supply function
            p.SetScript(0);
            p.SetUnknown(1);
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
            SendSupplyMenu p2 = new SendSupplyMenu();
            p2.SetMenuID(this.SupplyMenuID);            
            eh.C.netIO.SendPacket(p2, eh.C.SessionID);
        }



        private void HandelRegenbogen(ActorPC pc)
        {
            Packets.Server.NPCChat p = new SagaMap.Packets.Server.NPCChat();
            p.SetActor(this.Actor.id);
            p.SetIcons(1, new byte[1] { 21 });
            p.SetU(12);//marked as Regenbogen function
            p.SetScript(4462);
            p.SetUnknown(2);
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }

        public static void HandelMenu( ActorPC pc, byte Button, byte Menu )
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            Npc npc;
            if( Button == (byte)Functions.BookStore )
            {
                switch( Menu )
                {
                    case 31: //Jobchange
                        Packets.Server.ChangeJob p = new SagaMap.Packets.Server.ChangeJob();
                        List<byte> jobs = new List<byte>();
                        for( byte i = 1; i < 7; i++ )
                        {
                            if( i != (byte)pc.job ) jobs.Add( i );
                        }
                        p.SetJobCounts( (byte)jobs.Count );
                        p.SetPossibleJobs( jobs );
                        eh.C.netIO.SendPacket(p, eh.C.SessionID);
                        break;
                    case 32: //Special Skills
                        Packets.Server.SendSpecialSkills p2 = new SagaMap.Packets.Server.SendSpecialSkills();
                        List<uint> skills = new List<uint>();
                        List<uint> tmptable = new List<uint>();
                        foreach( uint i in pc.InactiveSkills.Keys )
                        {
                            if( SagaMap.Skills.SkillFactory.GetSkill( i ).special > 0 && !pc.BattleSkills.ContainsKey(i)) skills.Add( i );
                        }
                        p2.SetSkills( skills );
                        Packets.Server.JobLevels p3 = new SagaMap.Packets.Server.JobLevels();
                        p3.SetJobLevels(pc.JobLevels);
                        eh.C.netIO.SendPacket(p3, eh.C.SessionID);
                        eh.C.netIO.SendPacket(p2, eh.C.SessionID);
                        break;
                    case 33: //BookStore
                        npc = (Npc)pc.CurTarget.e;
                        npc.SendBooks( pc );
                        break;
                }
            }
            if( Button == (byte)Functions.Kafra )
            {
                switch( Menu )
                {
                    case 10: //Save Point
                        if( pc.CurTarget != null )
                        {
                            npc = (Npc)pc.CurTarget.e;
                            if( npc.SavePoint != null )
                            {
                                pc.save_map = (byte)npc.SavePoint[0];
                                pc.save_x = npc.SavePoint[1];
                                pc.save_y = npc.SavePoint[2];
                                pc.save_z = npc.SavePoint[3];
                            }
                        }
                        break;
                    case 11:
                        Packets.Server.ListWarehouse p = new SagaMap.Packets.Server.ListWarehouse((byte)pc.inv.GetStorageList().Count);
                        p.SetSortType(ITEM_TYPE.USEABLE);
                        p.SetItems(pc.inv.GetStorageList());
                        eh.C.netIO.SendPacket(p, eh.C.SessionID);
                        break;
                    case 12:
                        npc = (Npc)pc.CurTarget.e;
                        if (npc.Actor.NPCinv == null) npc.Actor.NPCinv = new List<Item>();
                        eh.OnSendShopList(npc.Actor.NPCinv, npc.money, npc.Actor.id);
                        break;
                }
            }
            if (Button == (byte)Functions.Regenbogen)
            {
                switch (Menu)
                {
                    case 21:
                        Packets.Server.MarketStart p = new SagaMap.Packets.Server.MarketStart();
                        p.SetActorID(pc.CurTarget.id);
                        eh.C.netIO.SendPacket(p, eh.C.SessionID);
                        break;
                }
            }
            Packets.Server.NPCMenu p1 = new SagaMap.Packets.Server.NPCMenu();
            p1.SetButtonID( Button );
            p1.SetMenuID( Menu );
            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
            Npc.NPCChat( pc, (ActorNPC)pc.CurTarget, 0 );
        }

        public static void RepaireEquip( ActorPC pc, byte Container, byte slot )
        {
            uint price;
            float tmp;
            if( Container == 8 )
            {
                Weapon weapon = WeaponFactory.GetActiveWeapon( pc );
                WeaponInfo info = WeaponFactory.GetWeaponInfo( (byte)weapon.type, (byte)weapon.level );
                float perc = (float)( info.maxdurability - weapon.durability ) / (float)info.maxdurability;
                tmp = ( 14 * weapon.level ) * perc;
                price = (uint)tmp;
                if( pc.zeny < price ) return;
                weapon.durability = (ushort)info.maxdurability;
                Packets.Server.WeaponAdjust p = new SagaMap.Packets.Server.WeaponAdjust();
                p.SetFunction( SagaMap.Packets.Server.WeaponAdjust.Function.Durability );
                p.SetValue( weapon.durability );
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                eh.C.netIO.SendPacket(p, eh.C.SessionID);
                pc.zeny -= price;
                eh.C.SendZeny();
            }
            if( Container == 1 )
            {
                Item item = pc.inv.EquipList[(EQUIP_SLOT)slot];
                float perc = (float)( item.maxDur - item.durability ) / (float)item.maxDur;
                tmp = ( item.price / 2 ) * perc;
                price = (uint)tmp;
                if( pc.zeny < price ) return;
                Packets.Server.ItemAdjust p = new SagaMap.Packets.Server.ItemAdjust();
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                if( item.durability == 0 )
                {
                    Bonus.BonusHandler.Instance.EquiqItem( pc, item, false );
                    p.SetContainer( Container );
                    p.SetFunction( SagaMap.Packets.Server.ItemAdjust.Function.Active );
                    p.SetSlot( slot );
                    p.SetValue( 1 );
                    eh.C.netIO.SendPacket(p, eh.C.SessionID); ;
                    Skills.SkillHandler.CalcHPSP( ref pc );
                    eh.C.SendCharStatus(0);
                    eh.C.SendExtStats();
                    eh.C.SendBattleStatus();
                    item.active = 1;
                }
                p = new SagaMap.Packets.Server.ItemAdjust();
                item.durability = item.maxDur;
                p.SetContainer( Container );
                p.SetFunction( SagaMap.Packets.Server.ItemAdjust.Function.Durability );
                p.SetSlot( slot );
                p.SetValue( item.durability );
                eh.C.netIO.SendPacket(p, eh.C.SessionID);
                pc.zeny -= price;
                eh.C.SendZeny();
                MapServer.charDB.UpdateItem(pc, item);
            }
            if( Container == 2 )
            {
                Item item = pc.inv.GetItem( CONTAINER_TYPE.INVENTORY, slot );
                if (item == null) return;
                float perc = (float)( item.maxDur - item.durability ) / (float)item.maxDur;
                tmp = ( item.price / 10 ) * perc;
                price = (uint)tmp;
                if( pc.zeny < price ) return;
                item.durability = item.maxDur;
                Packets.Server.ItemAdjust p = new SagaMap.Packets.Server.ItemAdjust();
                p.SetContainer( Container );
                p.SetFunction( SagaMap.Packets.Server.ItemAdjust.Function.Durability );
                p.SetSlot( slot );
                p.SetValue( item.durability );
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                eh.C.netIO.SendPacket(p, eh.C.SessionID);
                pc.zeny -= price;
                eh.C.SendZeny();
                MapServer.charDB.UpdateItem(pc, item);
            }
        }

        public virtual void OnKick() { }

        public virtual void OnDelete() { }

        public virtual void OnActorAppears( Actor dActor ) { }

        public virtual void OnActorChangesState(Actor aActor, MapEventArgs args) { }

        public virtual void OnActorStartsMoving( Actor mActor, float[] pos, float[] accel, int yaw, ushort speed, uint delayTime ) { }

        public virtual void OnActorStartsMoving( ActorNPC mActor, byte count, float[] waypoints, ushort speed ) { }

        public virtual void OnActorStopsMoving( Actor mActor, float[] pos, int yaw, ushort speed, uint delayTime ) { }

        public virtual void OnActorChat( Actor cActor, MapEventArgs args ) { }

        public virtual void OnActorDisappears( Actor dActor ) { }

        public virtual void OnActorSkillUse( Actor sActor, MapEventArgs args ) { }

        //public virtual void OnSelectButton(ActorPC sActor, int button) { }

        /// <summary>
        /// Send the Inventory list of a npc
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sActor"></param>
        /// <param name="NPCinv"></param>
        private void SendInventroy( SagaLib.Client client, ActorPC sActor, List<Item> NPCinv )
        {
            MapClient client_ = (MapClient)client;
            Packets.Server.SendNpcInventory sendPacket = new Packets.Server.SendNpcInventory();
            sendPacket.SetActorID( sActor.id );
            sendPacket.SetItems( NPCinv );
            client.netIO.SendPacket(sendPacket, client_.SessionID);
        }


        /// <summary>
        /// Send the Book list
        /// </summary>
        /// <param name="pc"></param>
        private void SendBooks( ActorPC pc )
        {
            if (this.Actor.NPCinv == null) return;
            Packets.Server.SendBookList p = new SagaMap.Packets.Server.SendBookList();
            p.SetActorID( this.Actor.id );
            p.SetMoney( this.money );
            p.SetBooks( this.Actor.NPCinv );
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }

        /// <summary>
        /// Set the save point for current Kafra
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetSavePoint( byte map, float x, float y, float z )
        {
            this.SavePoint = new float[4] { map, x, y, z };
        }

        public void OpenMailBox(ActorPC pc)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.MailList p1 = new SagaMap.Packets.Server.MailList();
            p1.SetMails(MapServer.charDB.GetMail(SagaDB.Mail.SearchType.Receiver, pc.name));
            p1.SetActorID(pc.CurTarget.id);
            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
        }

        /// <summary>
        /// Add goods to NPC Shop
        /// </summary>
        /// <param name="id"></param>
        public void AddGoods( int id )
        {
            if( this.Actor.NPCinv == null ) this.Actor.NPCinv = new List<Item>();
            this.Actor.NPCinv.Add( new SagaDB.Items.Item( id ) );
        }
        /// <summary>
        /// Add a Supply Product
        /// </summary>
        /// <param name="supplyID">Supply ID</param>
        /// <param name="id">item ID</param>
        /// <param name="amount">amount</param>
        public void AddSupplyProduct(uint supplyID, uint id, byte amount)
        {
            List<SendSupplyList.SupplyItem> list;
            if (this.SupplyProducts.ContainsKey(supplyID))
                list = this.SupplyProducts[supplyID];
            else
            {
                list = new List<SendSupplyList.SupplyItem>();
                this.SupplyProducts.Add(supplyID, list);
            }
            SendSupplyList.SupplyItem item = new SendSupplyList.SupplyItem();
            item.itemID = id;
            item.amount = amount;
            list.Add(item);
        }
        /// <summary>
        /// Add a Supply Matrial
        /// </summary>
        /// <param name="supplyID">Supply ID</param>
        /// <param name="id">item ID</param>
        /// <param name="amount">amount</param>        
        public void AddSupplyMatrial(uint supplyID, uint id, byte amount)
        {
            List<SendSupplyList.SupplyItem> list;
            if (this.SupplyMatrials.ContainsKey(supplyID))
                list = this.SupplyMatrials[supplyID];
            else
            {
                list = new List<SendSupplyList.SupplyItem>();
                this.SupplyMatrials.Add(supplyID, list);
            }
            SendSupplyList.SupplyItem item = new SendSupplyList.SupplyItem();
            item.itemID = id;
            item.amount = amount;
            list.Add(item);
        }

        public void OnActorChangeEquip( Actor sActor, MapEventArgs args ) { }

        public void OnSendToMap( bool success ) { }

        public void OnAddItem( Item nitem, SagaDB.Items.ITEM_UPDATE_REASON reason ) { }

        /// <summary>
        /// Say something
        /// </summary>
        /// <param name="sentence"></param>
        public void Say( string sentence )
        {
            Script.Say( this, sentence );
        }

        /// <summary>
        /// Count a certain Item
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public byte CountItem( ActorPC pc, int ItemID )
        {
            Item item = pc.inv.GetItem( CONTAINER_TYPE.INVENTORY, ItemID );
            if( item == null ) return 0;
            return item.stack;
        }

        /// <summary>
        /// Give item to a player
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="itemID"></param>
        /// <param name="ammount"></param>
        public void GiveItem( Actor dActor, int itemID, byte ammount )
        {
            for( byte i = 0; i < ammount; i++ )
                Script.GiveItem( this, (ActorPC)dActor, itemID );
            SagaMap.Quest.QuestsManager.UpdateQuestItem((ActorPC)dActor);
        }

        /// <summary>
        /// Take Item from a player
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="itemID"></param>
        /// <param name="ammount"></param>
        public void TakeItem( ActorPC pc, int itemID, byte ammount )
        {            
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.map.RemoveItemFromActorPC(pc, itemID, ammount, ITEM_UPDATE_REASON.NPC_TOOK);            
        }

        /// <summary>
        /// Warp a player
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="mapid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Warp( ActorPC pc, byte mapid, float x, float y, float z )
        {
            Script.Warp( pc, mapid, x, y, z );
        }

        /// <summary>
        /// Warp a player
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="mapid"></param>
        public void Warp( ActorPC pc, byte mapid )
        {
            Script.Warp( pc, mapid );
        }


        public void OnSendShopList( List<Item> items, uint money, uint ActorID )
        { }

        public void OnTimeWeatherChange( byte[] gameTime, Global.WEATHER_TYPE weather )
        { }

        public void OnTeleport( float x, float y, float z )
        { }

        public void OnPartyInvite( ActorPC sActor ) { }

        public void OnPartyAccept( ActorPC sActor ) { }

        public void OnTradeStart( ActorPC sActor ) { }

        public void OnTradeStatus( uint targetid, TradeResults status ) { }

        public void OnTradeItem( byte Tradeslot, Item TradeItem ) { }

        public void OnTradeConfirm() { }

        public void OnTradeZeny( int monies ) { }

        public void OnResetTradeItems() { }

        public void PerformTrade() { }

        public void OnSendWhisper( string name, string message, byte flag ) { }

        public void OnSendMessage( string from, string message ) { }

        public void OnChangeStatus( Actor sActor, MapEventArgs args ) { }

        public void OnActorSelection(ActorPC sActor, MapEventArgs args) { }
    }
}
