using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;

namespace SagaMap.Quest
{
    public static class QuestsManager
    {
        public class questI
        {
            public byte SubSID;
            public uint id;
            public byte ammount;
            public bool completed;
        }

        public struct LootInfo
        {
            public uint QID;
            public uint SID;
            public int itemID;
            public uint rate;
        }

        public class EnemyInfo
        {
            public byte SubSID;
            public List<uint> id;
            public byte ammount;
            public bool completed;
        }

        public class WayPointInfo
        {
            public uint QID;
            public uint SID;
            public uint npcType;
            public byte mapID;
            public float x;
            public float y;
            public float z;
        }

        public static Dictionary<uint, Dictionary<uint, List<questI>>> QuestItem = new Dictionary<uint, Dictionary<uint, List<questI>>>();
        public static Dictionary<uint, List<LootInfo>> MobQuestItem = new Dictionary<uint, List<LootInfo>>();
        public static Dictionary<uint, Dictionary<uint, List<EnemyInfo>>> Enemys = new Dictionary<uint, Dictionary<uint, List<EnemyInfo>>>();
        public static Dictionary<uint, Dictionary<uint, List<WayPointInfo>>> WayPoints = new Dictionary<uint, Dictionary<uint, List<WayPointInfo>>>();

        public static void SendNavPoint(ActorPC pc)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.SendNavPoint p = new SagaMap.Packets.Server.SendNavPoint();
            SagaDB.Quest.Quest quest = GetActiveQuest(pc);
            if (quest == null) return;
            if (!WayPoints.ContainsKey(quest.ID)) return;
            Dictionary<uint, List<WayPointInfo>> list1 = WayPoints[quest.ID];
            uint sid = 0;
            foreach (uint i in quest.Steps.Keys)
            {
                if (quest.Steps[i].Status == 1) sid = i;
                if (sid != 0) break;
            }
            if (sid == 0) return;
            if (!list1.ContainsKey(sid)) return;
            List<WayPointInfo> list2 = list1[sid];
            List<WayPointInfo> list3 = new List<WayPointInfo>();
            foreach (WayPointInfo j in list2)
            {
                if (j.mapID == pc.mapID)
                    list3.Add(j);
            }
            if (list3.Count == 0) return;
            p.SetQuestID(quest.ID);
            p.SetPosition(list3);
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }
        
        public static bool ifGotQuest(ActorPC pc, uint id)
        {
            if (id == 0) return false;
            if (pc.QuestTable.ContainsKey(id)) return true; else return false;
            
        }

        public static bool ifCompletedPersonalQuest(ActorPC pc, uint id)
        {
            if (id == 0) return false;
            SagaDB.Quest.Quest quest = GetActivePersonalQuest(pc);
            if (quest != null)
            {
                if (quest.ID == id)
                    return false;
            }
            return pc.PersonalQuestTable.ContainsKey(id);
        }

        public static bool ifCompletedQuest(ActorPC pc, uint id)
        {
            if (id == 0) return false;
            SagaDB.Quest.Quest quest = GetActiveQuest(pc);
            if (quest != null)
            {
                if (quest.ID == id)
                    return false;
            }
            return pc.QuestTable.ContainsKey(id);
        }

        public static void AddEnemyInfo(uint QID, uint SID, byte SubSID, List<uint> MobID, byte ammount)
        {
            Dictionary<uint, List<EnemyInfo>> enemys;
            List<EnemyInfo> tmp = new List<EnemyInfo>();
            if (Enemys.ContainsKey(QID))
            {
                enemys = Enemys[QID];
            }
            else
            {
                enemys = new Dictionary<uint, List<EnemyInfo>>();
                Enemys.Add(QID, enemys);
            }
            if (enemys.ContainsKey(SID) == false) enemys.Add(SID, tmp);
            EnemyInfo I = new EnemyInfo();
            I.SubSID = SubSID;
            I.id = MobID;
            I.completed = false;
            I.ammount = ammount;
            enemys[SID].Add(I);
        }

        public static void AddQuestItem(uint QID, uint SID, byte SubSID, uint ItemID, byte ammount)
        {
            Dictionary<uint, List<questI>> item;
            List<questI> tmp = new List<questI>();
            if (QuestItem.ContainsKey(QID))
            {
                item = QuestItem[QID];
            }
            else
            {
                item = new Dictionary<uint, List<questI>>();
                QuestItem.Add(QID, item);
            }
            if (item.ContainsKey(SID) == false) item.Add(SID, tmp);
            questI I = new questI();
            I.SubSID = SubSID;
            I.id = ItemID;
            I.completed = false;
            I.ammount = ammount;
            item[SID].Add(I);
        }

        public static void AddMobLoot(uint mob, uint QID, uint sID, int itemID, uint rate)
        {
            LootInfo info = new LootInfo();
            List<LootInfo> table;
            info.itemID = itemID;
            info.QID = QID;
            info.SID = sID;
            info.rate = rate;
            if (MobQuestItem.ContainsKey(mob))
                table = MobQuestItem[mob];
            else
            {
                table = new List<LootInfo>();
                MobQuestItem.Add(mob, table);
            }

            table.Add(info);
        }

        public static byte GetQuestStepStatus(ActorPC pc, uint id, uint step)
        {
            if (id == 0 || step == 0) return 0;
            if (pc.QuestTable.ContainsKey(id) == false)
            {
                if (!pc.PersonalQuestTable.ContainsKey(id))
                    return 0;
                else
                {
                    SagaDB.Quest.Quest quest2 = pc.PersonalQuestTable[id];
                    if (quest2.Steps.ContainsKey(step) == false) return 0;
                    SagaDB.Quest.Step step3 = quest2.Steps[step];
                    return step3.Status;
                }
            }
            SagaDB.Quest.Quest quest = pc.QuestTable[id];
            if (quest.Steps.ContainsKey(step) == false) return 0;
            SagaDB.Quest.Step step2 = quest.Steps[step];
            return step2.Status;
        }

        public static byte GetNPCIcon(ActorPC pc, Npc npc)
        {
            byte icon = 0;
            SagaDB.Quest.Quest q = Quest.QuestsManager.GetActiveQuest(pc);
            if (q != null)
            {
                if (npc.Questtable.ContainsKey(q.ID))
                {
                    foreach (SagaDB.Quest.Step s in npc.Questtable[q.ID].Steps.Values)
                    {
                        if (s.Status == Quest.QuestsManager.GetQuestStepStatus(pc, q.ID, s.ID))
                        {
                            icon += 2;
                        }
                        if (icon >= 2) break;
                    }
                }
            }
            Npc.QuestReqirement pq = npc.GetAvaluablePersonalQuest(pc);
            if (pq != null)
            {
                icon += 1;
            }
            q = QuestsManager.GetActivePersonalQuest(pc);
            if (icon != 3 && icon !=2)
            {
                if (q != null)
                {
                    if (npc.PersonalQuesttable.ContainsKey(q.ID))
                    {
                        foreach (SagaDB.Quest.Step s in npc.PersonalQuesttable[q.ID].Steps.Values)
                        {
                            if (s.Status == Quest.QuestsManager.GetQuestStepStatus(pc, q.ID, s.ID))
                            {
                                icon += 2;
                            }
                            if (icon >= 2) break;
                        }
                    }
                }
            }
            return icon;
        }

        public static void UpdateEnemyInfo(ActorPC pc, uint MobID, bool maploded)
        {
            SagaDB.Quest.Quest quest = GetActiveQuest(pc);
            UpdateEnemyInfoSub(pc, MobID, quest, maploded);
            quest = GetActivePersonalQuest(pc);
            UpdateEnemyInfoSub(pc, MobID, quest, maploded);
        }

        public static void UpdateEnemyInfo(ActorPC pc,uint MobID)
        {
            UpdateEnemyInfo(pc, MobID, false);
        }

        private static void UpdateEnemyInfoSub(ActorPC pc, uint MobID, SagaDB.Quest.Quest quest, bool maploded)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if (quest == null) return;
            if (!Enemys.ContainsKey(quest.ID)) return;
            foreach (uint i in quest.Steps.Keys)
            {
                if (GetQuestStepStatus(pc, quest.ID, i) == 1)
                {
                    if (!Enemys[quest.ID].ContainsKey(i)) return;
                    bool completed = true;
                    SagaDB.Quest.Step step = quest.Steps[i];
                    foreach (EnemyInfo q in Enemys[quest.ID][i])
                    {
                        if (step.SubSteps == null)
                        {
                            step.SubSteps = new Dictionary<byte, byte>();
                        }
                        if (!step.SubSteps.ContainsKey(q.SubSID))
                            step.SubSteps.Add(q.SubSID, 0);
                        if (q.id.Contains(MobID))
                        {
                            if (step.SubSteps[q.SubSID] >= q.ammount)
                            {
                                if (maploded)
                                {
                                    Packets.Server.UpdateQuestSubStep p2 = new SagaMap.Packets.Server.UpdateQuestSubStep();
                                    p2.SetQuestID(quest.ID);
                                    p2.SetStep(i);
                                    if (q.SubSID != 0) p2.SetSubStep((byte)(q.SubSID - 1));
                                    p2.SetAmmount(step.SubSteps[q.SubSID]);
                                    eh.C.netIO.SendPacket(p2, eh.C.SessionID);
                                }
                                continue;
                            }
                            step.SubSteps[q.SubSID]++;
                            Packets.Server.UpdateQuestSubStep p1 = new SagaMap.Packets.Server.UpdateQuestSubStep();
                            p1.SetQuestID(quest.ID);
                            p1.SetStep(i);
                            if (q.SubSID != 0) p1.SetSubStep((byte)(q.SubSID - 1));
                            p1.SetAmmount(step.SubSteps[q.SubSID]);
                            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
                        }

                    }
                    foreach (EnemyInfo j in Enemys[quest.ID][i])
                    {
                        if (step.SubSteps[j.SubSID] < j.ammount)
                            completed = false;
                    }
                    if (completed)
                        SetQuestStepStatus(pc, quest.ID, i, 2);
                }
            }
        }

        private static void UpdateQuestItemSub(ActorPC pc, SagaDB.Quest.Quest quest,bool maploaded)
        {
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if (quest == null) return;
            if (!QuestItem.ContainsKey(quest.ID)) return;
            foreach (uint i in quest.Steps.Keys)
            {
                if (GetQuestStepStatus(pc, quest.ID, i) == 1)
                {
                    if (!QuestItem[quest.ID].ContainsKey(i)) return;
                    bool completed = true;
                    SagaDB.Quest.Step step = quest.Steps[i];
                    foreach (questI q in QuestItem[quest.ID][i])
                    {
                        List<SagaDB.Items.Item> l = pc.inv.GetInventoryList();
                        if (step.SubSteps == null)
                        {
                            step.SubSteps = new Dictionary<byte, byte>();
                        }
                        if (!step.SubSteps.ContainsKey(q.SubSID))
                            step.SubSteps.Add(q.SubSID, 0);
                        foreach (SagaDB.Items.Item j in l)
                        {
                            if (j.id == q.id)
                            {
                                if (step.SubSteps[q.SubSID] >= q.ammount)
                                {
                                    if (maploaded)
                                    {
                                        Packets.Server.UpdateQuestSubStep p2 = new SagaMap.Packets.Server.UpdateQuestSubStep();
                                        p2.SetQuestID(quest.ID);
                                        p2.SetStep(i);
                                        if (q.SubSID != 0) p2.SetSubStep((byte)(q.SubSID - 1));
                                        p2.SetAmmount(j.stack);
                                        eh.C.netIO.SendPacket(p2, eh.C.SessionID);
                                    }
                                    continue;
                                }
                                Packets.Server.UpdateQuestSubStep p1 = new SagaMap.Packets.Server.UpdateQuestSubStep();
                                p1.SetQuestID(quest.ID);
                                p1.SetStep(i);
                                if (q.SubSID != 0) p1.SetSubStep((byte)(q.SubSID - 1));
                                p1.SetAmmount(j.stack);
                                eh.C.netIO.SendPacket(p1, eh.C.SessionID);
                                step.SubSteps[q.SubSID] = j.stack;
                            }
                        }
                    }
                    foreach (questI j in QuestItem[quest.ID][i])
                    {
                        if (step.SubSteps[j.SubSID] < j.ammount)
                            completed = false;
                    }
                    if (completed)
                        SetQuestStepStatus(pc, quest.ID, i, 2);
                }
            }
        }

        public static void UpdateQuestItem(ActorPC pc)
        {
            UpdateQuestItem(pc, false);
        }

        public static void UpdateQuestItem(ActorPC pc, bool maploaded)
        {
            SagaDB.Quest.Quest quest = GetActiveQuest(pc);
            UpdateQuestItemSub(pc, quest, maploaded);
            quest = GetActivePersonalQuest(pc);
            UpdateQuestItemSub(pc, quest, maploaded);
        }

        public static void SetQuestStepStatus(ActorPC pc, uint id, uint step, byte status)
        {
            SagaDB.Quest.QuestType type;
            if (id == 0 || step == 0) return;
            SagaDB.Quest.Quest quest;
            if (pc.QuestTable.ContainsKey(id))
            {
                quest = pc.QuestTable[id];
                type = SagaDB.Quest.QuestType.OfficialQuest;
            }
            else
            {
                if (pc.PersonalQuestTable.ContainsKey(id))
                {
                    quest = pc.PersonalQuestTable[id];
                    type = SagaDB.Quest.QuestType.PersonalQuest;
                }
                else
                    return;
            }
            if (quest.Steps.ContainsKey(step) == false) return;
            SagaDB.Quest.Step step2 = quest.Steps[step];
            step2.Status = status;
            if (status == 2 && step2.nextStep != 0)
            {
                quest.Steps[step2.nextStep].Status = 1;
            }
            MapServer.charDB.UpdateQuest(pc, type, quest);
            Packets.Server.UpdateQuest p = new SagaMap.Packets.Server.UpdateQuest();
            p.SetQuestID(id);
            p.SetStep(step2);
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            eh.C.netIO.SendPacket(p, eh.C.SessionID);
        }

        public static SagaDB.Quest.Quest GetActiveQuest(ActorPC pc)
        {
            foreach (SagaDB.Quest.Quest i in pc.QuestTable.Values)
            {
                foreach (SagaDB.Quest.Step j in i.Steps.Values)
                {
                    if (j.Status != 2) return i;
                }
            }
            return null;
        }

        public static SagaDB.Quest.Quest GetActivePersonalQuest(ActorPC pc)
        {
            foreach (SagaDB.Quest.Quest i in pc.PersonalQuestTable.Values)
            {
                foreach (SagaDB.Quest.Step j in i.Steps.Values)
                {
                    if (j.Status != 2) return i;
                }
            }
            return null;
        }
    }
}
