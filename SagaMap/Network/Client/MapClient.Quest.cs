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

namespace SagaMap
{
    public partial class MapClient
    {

        #region "0x07"

        // 0x07 Packets =========================================

        /// <summary>
        /// 07 01 Receive the quest group list from the client.
        /// </summary>
        // NOTE: Function is called sendquestgrouplist while it's receiving stuff????
        public void OnWantQuestGroupList(SagaMap.Packets.Client.WantQuestGroupList p)
        {
            /* if( this.state != SESSION_STATE.MAP_LOADED ) return;
             Logger.ShowInfo( "Got quest group list request (0x0701) from client.", null );
             SagaDB.Quest.Quest q = Quest.QuestsManager.GetActiveQuest( this.Char );
             if( q != null )
             {
                 Packets.Server.QuestInfo p1 = new SagaMap.Packets.Server.QuestInfo();
                 p1.SetQuestNum( 1 );
                 p1.SetMissionID( q.ID );
                 p1.SetQuestInfos( q.Steps, 0 );
                 this.netIO.SendPacket(p1, this.SessionID);
             }*/
        }

        //07 03 Droplist Select
        public void OnDropSelect(SagaMap.Packets.Client.DropSelect p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            try
            {
                if (this.Char.CurNPCinv == null) this.Char.CurNPCinv = new List<Item>();
                if (this.Char.CurNPCinv.Count != 0)
                {
                    Item nItem = this.Char.CurNPCinv[(int)p.GetIndex()];
                    this.Char.CurNPCinv.RemoveAt((int)p.GetIndex());
                    if (nItem != null)
                    {
                        this.map.AddItemToActor(this.Char, nItem, ITEM_UPDATE_REASON.FOUND);
                        if (this.Party != null)
                            this.Party.SendLoot(this, (uint)nItem.id);
                    }

                }
                Packets.Server.SendNpcInventory sendPacket = new Packets.Server.SendNpcInventory();
                sendPacket.SetActorID(this.Char.id);
                sendPacket.SetItems(this.Char.CurNPCinv);
                this.netIO.SendPacket(sendPacket, this.SessionID);
                Quest.QuestsManager.UpdateQuestItem(this.Char);
            }
            catch (Exception)
            {
                Packets.Server.SendNpcInventory sendPacket = new Packets.Server.SendNpcInventory();
                sendPacket.SetActorID(this.Char.id);
                sendPacket.SetItems(new List<Item>());
                this.netIO.SendPacket(sendPacket, this.SessionID);
                Quest.QuestsManager.UpdateQuestItem(this.Char);
            }
        }
        public void OnQuestConfirmCancel(Packets.Client.QuestConfirmCancel p)
        {
            this.QuestConfirm = false;
            uint QID = p.GetQuestID();
            SagaDB.Quest.Quest quest = Quest.QuestsManager.GetActiveQuest(this.Char);
            if (QID != 0 && quest != null)
            {
                if (QID == quest.ID)
                {
                    MapServer.charDB.DeleteQuest(this.Char, this.Char.QuestTable[QID]);
                    this.Char.QuestTable.Remove(QID);
                    this.SendQuestInfo();
                }
            }
            quest = Quest.QuestsManager.GetActivePersonalQuest(this.Char);
            if (QID != 0 && quest != null)
            {
                if (QID == quest.ID)
                {
                    MapServer.charDB.DeleteQuest(this.Char, this.Char.PersonalQuestTable[QID]);
                    this.Char.PersonalQuestTable.Remove(QID);
                    this.SendQuestInfo();
                }
            }
        }
        //07 05
        public void OnQuestConfirm(Packets.Client.QuestConfirm p)
        {
            if (SagaMap.Quest.QuestsManager.GetActiveQuest(this.Char) != null)
            {
                Packets.Server.QuestCancel p2 = new SagaMap.Packets.Server.QuestCancel();
                p2.SetQuestID(Quest.QuestsManager.GetActiveQuest(this.Char).ID);
                this.netIO.SendPacket(p2, this.SessionID); ;
                return;
            }
            if (this.QuestConfirm == false)
            {
                Packets.Server.QuestConfirm p1 = new SagaMap.Packets.Server.QuestConfirm();
                p1.SetQuestID(p.GetQuestID());
                this.netIO.SendPacket(p1, this.SessionID);
                this.QuestConfirm = true;
            }
            else
            {
                MapItem item = (MapItem)this.Char.LastMissionBoard.e;
                item.OnQuestConfirmed(this.Char, p.GetQuestID());
                this.QuestConfirm = false;
            }
        }
        //07 06
        public void OnQuestCompleted(Packets.Client.QuestCompleted p)
        {
            if (RewardChoice != null)
            {
                Packets.Server.QuestRewardChoice p1 = new SagaMap.Packets.Server.QuestRewardChoice();
                this.netIO.SendPacket(p1, this.SessionID); ;
                completingquest = p.GetQuestID();
            }
            else
            {
                if (RewardFunc != null)
                {
                    RewardFunc.DynamicInvoke(this.Char, p.GetQuestID());
                    completingquest = 0;
                    RewardFunc = null;
                }
            }
        }
        //07 07
        public void OnQuestRewardChoice(Packets.Client.QuestRewardChoice p)
        {
            if (RewardFunc != null)
            {
                RewardFunc.DynamicInvoke(this.Char, completingquest);
                completingquest = 0;
                RewardFunc = null;
            }
            if (RewardChoice != null)
            {
                this.map.AddItemToActor(this.Char, RewardChoice[p.GetChoice() - 1], ITEM_UPDATE_REASON.NPC_GAVE);
                RewardChoice = null;
            }
        }
        #endregion

    }
}
