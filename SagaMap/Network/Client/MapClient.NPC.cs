//#define Preview_Version
using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;
using SagaDB.Mail;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skills;
using SagaMap.Scripting;

namespace SagaMap
{
    public partial class MapClient
    {

        #region "0x06"
        // 0x06 Packets =========================================

        public void OnGetHateInfo(SagaMap.Packets.Client.GetHateInfo p)
        {
            ActorNPC actor = (ActorNPC)map.GetActor(p.GetActorID());
            if (actor == null) return;
            if (actor.npcType < 10000) return;
            Mob mob = (Mob)actor.e;
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            Packets.Server.SendHateInfo sendPacket = new SagaMap.Packets.Server.SendHateInfo();
            sendPacket.SetActor(p.GetActorID());
            if (mob.Hate.ContainsKey(this.Char.id))
            {
                sendPacket.SetHateInfo(mob.Hate[this.Char.id]); // 0 hate = No reason to attack, client stops attacking.
            }
            else
            {
                sendPacket.SetHateInfo(0); // 0 hate = No reason to attack, client stops attacking.            
            }
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnNPCMenu(Packets.Client.NPCMenu p)
        {
            Npc.HandelMenu(this.Char, p.GetButtonID(), p.GetMenuID());
        }

        public void OnGetCancel(SagaMap.Packets.Client.GetTargetCancel p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            // Temporary Display Info
            Map.ActorSelArgs arg = new Map.ActorSelArgs(0);
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
        }

        public void OnNPCChat(Packets.Client.NPCChat p)
        {
            Actor aActor = map.GetActor(p.GetActorID());
            if (aActor == null || aActor.type == ActorType.PC) return;
            this.Char.CurTarget = aActor;
            ActorNPC CurNpc = (ActorNPC)aActor;
            Npc npc = (Npc)CurNpc.e;
            byte[] tmp = null;
            SagaDB.Quest.Quest q = Quest.QuestsManager.GetActiveQuest(this.Char);
            if (q != null)
            {
                if (npc.Questtable.ContainsKey(q.ID))
                {
                    foreach (SagaDB.Quest.Step s in npc.Questtable[q.ID].Steps.Values)
                    {
                        if (s.Status == Quest.QuestsManager.GetQuestStepStatus(this.Char, q.ID, s.ID))
                        {
                            tmp = new byte[CurNpc.Attribute.icons.Length + 1];
                            CurNpc.Attribute.icons.CopyTo(tmp, 0);
                            tmp[CurNpc.Attribute.icons.Length] = (byte)Npc.Functions.OfficialQuest;
                        }
                    }
                }
            }
            q = Quest.QuestsManager.GetActivePersonalQuest(this.Char);
            if (q != null)
            {
                if (npc.PersonalQuesttable.ContainsKey(q.ID))
                {
                    foreach (SagaDB.Quest.Step s in npc.PersonalQuesttable[q.ID].Steps.Values)
                    {
                        if (s.Status == Quest.QuestsManager.GetQuestStepStatus(this.Char, q.ID, s.ID))
                        {
                            tmp = new byte[CurNpc.Attribute.icons.Length + 1];
                            CurNpc.Attribute.icons.CopyTo(tmp, 0);
                            tmp[CurNpc.Attribute.icons.Length] = (byte)Npc.Functions.PersonalQuest;
                        }
                    }
                }
            }
            Npc.QuestReqirement pq = npc.GetAvaluablePersonalQuest(this.Char);
            if (pq != null)
            {
                if (tmp != null)
                {
                    byte[] tmp2 = new byte[tmp.Length + 1];
                    tmp.CopyTo(tmp2, 0);
                    tmp2[tmp.Length] = (byte)Npc.Functions.AcceptPersonalRequest;
                    tmp = tmp2;
                }
                else
                {
                    tmp = new byte[CurNpc.Attribute.icons.Length + 1];
                    CurNpc.Attribute.icons.CopyTo(tmp, 0);
                    tmp[CurNpc.Attribute.icons.Length] = (byte)Npc.Functions.AcceptPersonalRequest;
                }
            }
            if (tmp == null)
            {
                tmp = CurNpc.Attribute.icons;
            }
            Packets.Server.NPCChat sendPacket = new SagaMap.Packets.Server.NPCChat();
            sendPacket.SetActor(p.GetActorID());
            sendPacket.SetScript(CurNpc.Attribute.script);
            sendPacket.SetIcons((byte)tmp.Length, tmp);
            sendPacket.SetUnknown(1);

            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnCorpseCleared(Packets.Client.CorpseCleared p)
        {
            //TODO:
            //add something

        }

        public void OnLeaveNPC(Packets.Client.LeaveNPC p)
        {
            this.Char.CurTarget = null;
            //TODO:
            //Add more
        }

        public void OnNPCChat(Npc npc)
        {
            byte[] tmp = null;
            SagaDB.Quest.Quest q = Quest.QuestsManager.GetActiveQuest(this.Char);
            if (q != null)
            {
                if (npc.Questtable.ContainsKey(q.ID))
                {
                    foreach (SagaDB.Quest.Step s in npc.Questtable[q.ID].Steps.Values)
                    {
                        if (s.Status == Quest.QuestsManager.GetQuestStepStatus(this.Char, q.ID, s.ID))
                        {
                            tmp = new byte[npc.Actor.Attribute.icons.Length + 1];
                            npc.Actor.Attribute.icons.CopyTo(tmp, 0);
                            tmp[npc.Actor.Attribute.icons.Length] = (byte)Npc.Functions.OfficialQuest;
                        }
                    }
                }
            }
            q = Quest.QuestsManager.GetActivePersonalQuest(this.Char);
            if (q != null)
            {
                if (npc.PersonalQuesttable.ContainsKey(q.ID))
                {
                    foreach (SagaDB.Quest.Step s in npc.PersonalQuesttable[q.ID].Steps.Values)
                    {
                        if (s.Status == Quest.QuestsManager.GetQuestStepStatus(this.Char, q.ID, s.ID))
                        {
                            tmp = new byte[npc.Actor.Attribute.icons.Length + 1];
                            npc.Actor.Attribute.icons.CopyTo(tmp, 0);
                            tmp[npc.Actor.Attribute.icons.Length] = (byte)Npc.Functions.PersonalQuest;
                        }
                    }
                }
            }
            Npc.QuestReqirement pq = npc.GetAvaluablePersonalQuest(this.Char);
            if (pq != null)
            {
                if (tmp != null)
                {
                    byte[] tmp2 = new byte[tmp.Length + 1];
                    tmp.CopyTo(tmp2, 0);
                    tmp2[tmp.Length] = (byte)Npc.Functions.AcceptPersonalRequest;
                    tmp = tmp2;
                }
                else
                {
                    tmp = new byte[npc.Actor.Attribute.icons.Length + 1];
                    npc.Actor.Attribute.icons.CopyTo(tmp, 0);
                    tmp[npc.Actor.Attribute.icons.Length] = (byte)Npc.Functions.AcceptPersonalRequest;
                }
            }
            if (tmp == null)
            {
                tmp = npc.Actor.Attribute.icons;
            }
            Packets.Server.NPCChat sendPacket = new SagaMap.Packets.Server.NPCChat();
            sendPacket.SetActor(npc.Actor.id);
            sendPacket.SetScript(0);
            sendPacket.SetIcons((byte)tmp.Length, tmp);
            sendPacket.SetUnknown(1);

            this.netIO.SendPacket(sendPacket, this.SessionID); ;
        }

        public void OnPersonalRequest(Packets.Client.NPCPersonalRequest p)
        {
            if (this.Char.CurTarget == null) return;
            Npc npc = (Npc)this.Char.CurTarget.e;
            Npc.QuestReqirement quest = npc.GetAvaluablePersonalQuest(this.Char);
            if (quest != null)
            {
                if (npc.Functable.ContainsKey((byte)Npc.Functions.AcceptPersonalRequest))
                {
                    npc.Functable[(byte)Npc.Functions.AcceptPersonalRequest].DynamicInvoke(this.Char, quest.QID, p.GetValue());
                }
            }
        }

        public void OnGetAttribute(SagaMap.Packets.Client.GetTargetAttribute p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            Actor aActor = map.GetActor(p.GetActorID());
            if (aActor == null) return;
            this.Char.CurTarget = aActor;
            if (aActor.type == ActorType.NPC)
            {
                // Temporary Display Info
                Map.ActorSelArgs arg = new Map.ActorSelArgs(aActor.id);
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
            }
            else if (aActor.type == ActorType.PC)
            {
                Map.ActorSelArgs arg = new Map.ActorSelArgs(aActor.id);
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
            }
            else if (aActor.type == ActorType.Item)
            {
                ActorItem item = (ActorItem)aActor;
                MapItem e = (MapItem)item.e;
                e.OnClicked(this.Char);
            }
        }

        public void OnSelectButton(SagaMap.Packets.Client.GetSelectButton p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            int button = (int)p.GetSelection();
            if (this.Char.CurTarget == null) return;
            this.Char.CurTarget.e.OnSelectButton(this.Char, button);
        }

        public void OnSupplySelect(Packets.Client.SupplySelect p)
        {
            if (this.Char.CurTarget == null) return;
            if (this.Char.CurTarget.type != ActorType.NPC) return;
            try
            {
                Npc npc = (Npc)this.Char.CurTarget.e;
                Packets.Server.SendSupplyList p2 = new SagaMap.Packets.Server.SendSupplyList();
                uint supplyID = p.GetSupplyID();
                if (!npc.SupplyMatrials.ContainsKey(supplyID) || !npc.SupplyProducts.ContainsKey(supplyID))
                {
                    if (this.Char.GMLevel > 1)
                        SendMessage("System", "Cannot find Supply items for supplyID:" + supplyID + " for this NPC!", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
                    return;
                }
                p2.SetActor(npc.Actor.id);
                p2.SetSupplyID(supplyID);
                p2.SetProducts(npc.SupplyProducts[supplyID]);
                p2.SetMatrial(npc.SupplyMatrials[supplyID]);
                this.netIO.SendPacket(p2, this.SessionID);
                npc.NPCChat(this.Char, 0);
            }
            catch (Exception) { }
        }

        public void OnSupplyExchange(Packets.Client.SupplyExchange p)
        {
            if (this.Char.CurTarget == null) return;
            if (this.Char.CurTarget.type != ActorType.NPC) return;
            try
            {
                Npc npc = (Npc)this.Char.CurTarget.e;
                Packets.Server.SendSupplyResult p2 = new SagaMap.Packets.Server.SendSupplyResult();
                uint supplyID = p.GetSupplyID();
                if (!npc.SupplyMatrials.ContainsKey(supplyID) || !npc.SupplyProducts.ContainsKey(supplyID))
                {
                    if (this.Char.GMLevel > 1)
                        SendMessage("System", "Cannot find Supply items for supplyID:" + supplyID + " for this NPC!", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
                    return;
                }
                if (!this.Char.inv.HasFreeSpace())
                {
                    p2.SetResult(SagaMap.Packets.Server.SendSupplyResult.Results.NOT_ENOUGH_SPACE);
                    this.netIO.SendPacket(p2, this.SessionID);
                    return;
                }
                foreach (Packets.Server.SendSupplyList.SupplyItem i in npc.SupplyMatrials[supplyID])
                {
                    if (npc.CountItem(this.Char, (int)i.itemID) < i.amount)
                    {
                        p2.SetResult(SagaMap.Packets.Server.SendSupplyResult.Results.NOT_ENOUGH_SPACE);
                        this.netIO.SendPacket(p2, this.SessionID);
                        return;
                    }
                    npc.TakeItem(this.Char, (int)i.itemID, i.amount);
                }
                foreach (Packets.Server.SendSupplyList.SupplyItem i in npc.SupplyProducts[supplyID])
                {
                    npc.GiveItem(this.Char, (int)i.itemID, i.amount);
                }
                p2.SetResult(SagaMap.Packets.Server.SendSupplyResult.Results.OK);
                this.netIO.SendPacket(p2, this.SessionID);
            }
            catch (Exception)
            {
            }
        }

        public void OnWarp(Packets.Client.Warp p)
        {
            /*
             * Warp function, is used to warp you to a place in exchange of an price
             * WarpLocation is a index to indicated the location where to warp to.
             * 
             * This index found in portal.dat 
             * Note duplicate locations can occur
             * 
             * If the player can be warped:
             * SMSG_LEAVENPC
             * SMSG_UPDATEZENY
             * SMSG_ACTORDELETE -> for all actors in view range
             * SMSG_ACTORTELEPORT | SMSG_SENDMAPSTART
             * SMSG_ACTORINFO & SMSGNPCINFO & SMSG_ITEMINFO -> for all known objects in range
             * 
             * Unfortunally only price and index are known in portal.dat
             * so we need to obtain all x,y,z coords ourselfs.
             */
        }

        #endregion

    }
}
