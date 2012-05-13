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

        #region "0x08"

        // 0x08 Packets =========================================

        // 08 01
        public void OnTrade(SagaMap.Packets.Client.GetTrade p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            try
            {
                ActorPC target = (ActorPC)map.GetActor(p.GetTargetActor());
                if (target != null)
                {

                    if (this.Char.trading != Trading.NOT_TRADING || target.trading != Trading.NOT_TRADING)
                    {
                        //Error - Do not send to other actor
                        SendTradeStatus(p.GetTargetActor(), TradeResults.TARGET_TRADE_ACTIVE);
                    }
                    else
                    {
                        target.TradeTarget = this.Char.id;
                        target.trading = Trading.TRADING;
                        this.Char.TradeTarget = target.id;
                        this.Char.trading = Trading.TRADING;
                        Logger.ShowInfo("Trade Request by " + this.Char.name + " to " + target.name, null);

                        // reset this tradeitems list (the other is done in the OnTradeStart
                        TradeItems = new Dictionary<Item, byte>();
                        target.e.OnTradeStart(this.Char);
                    }
                }
                else
                    return;
            }

            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }

        // 08 02
        public void OnTradeOther(SagaMap.Packets.Client.GetTradeOther p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            byte status = p.GetResponse();
            if (status == 0)
            // Accept the trade
            {
                Logger.ShowInfo("Trade accepted (status " + p.GetResponse() + ")", null);
                ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);

                SendTradeStatusOther(this.Char.TradeTarget, TradeResults.SUCCESS);
            }
            else
            // Reject the trade
            {
                Logger.ShowInfo("Trade rejected (status " + p.GetResponse() + ")", null);
                SendTradeStatus(this.Char.TradeTarget, TradeResults.TARGET_CANCELLED);
                SendTradeStatusOther(this.Char.id, TradeResults.TARGET_CANCELLED);
                ResetTradeStatus(2);
            }
        }

        // 08 03
        public void OnTradeItem(SagaMap.Packets.Client.GetTradeItem p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (this.Char.trading != Trading.TRADING) return;
            byte Tradeslot = p.GetSlot();
            byte ItemIndex = p.GetItem();
            byte quantity = p.GetQuantity();
            byte status;
            //Lookup trade items information from character's inventory
            Item TradeItem = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, ItemIndex);
            if (quantity > TradeItem.stack) quantity = TradeItem.stack;
            TradeItem = new Item(TradeItem.id, TradeItem.creatorName, TradeItem.durability, TradeItem.stack);// make sure it's a different instance than the old one
            if (this.Char.trading == Trading.TRADING)
            {
                if (!TradeItem.tradeAble)
                {
                    this.SendTradeResult(TradeResults.NOT_TRADEABLE);
                    status = 9;
                }
                else
                {
                    status = 0;

                    // Add item to chars list of items to trade
                    try
                    {
                        TradeItems.Add(TradeItem, quantity);
                    }
                    catch
                    {
                    }
                    Logger.ShowInfo("Trade item added", null);
                    //Send info about item to clients
                    Packets.Server.TradeItem sendPacket = new SagaMap.Packets.Server.TradeItem();
                    sendPacket.SetTradeSlot(Tradeslot);
                    sendPacket.SetItemIndex(ItemIndex);
                    sendPacket.SetQuantity(quantity);
                    sendPacket.SetStatus(status);
                    netIO.SendPacket(sendPacket, this.SessionID);

                    ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);
                    if (target == null) return;
                    target.e.OnTradeItem(Tradeslot, TradeItem);
                }
            }

        }

        // 08 04
        public void OnTradeMoney(SagaMap.Packets.Client.GetTradeMoney p)
        {
            if (this.Char.trading != Trading.TRADING)
                return;

            this.TradeMoney = p.GetMoney();
            Packets.Server.TradeZeny sendPacket = new SagaMap.Packets.Server.TradeZeny();
            sendPacket.SetMoney(p.GetMoney());
            netIO.SendPacket(sendPacket, this.SessionID);

            ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);
            if (target == null) return;
            target.e.OnTradeZeny(p.GetMoney());

        }

        // 08 05
        public void OnTradeListConfirm(SagaMap.Packets.Client.GetTradeListConfirm p)
        {
            if (this.Char.trading != Trading.TRADING) return;
            this.Char.TradeStatus = TradeStatus.LIST_CONFIRM;
            this.Char.e.OnTradeConfirm();
            ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);
            target.e.OnTradeConfirm();
            Console.WriteLine("trade confirm send to both parties");
        }

        // 08 06
        public void OnTradeConfirm(SagaMap.Packets.Client.GetTradeConfirm p)
        {
            if (this.Char.trading != Trading.TRADING) return;
            if (this.Char.TradeStatus == TradeStatus.LIST_CONFIRM)
            {
                ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);
                if (target.TradeStatus == TradeStatus.TRADE_CONFIRM)
                    //this.PerformTrade();
                    this.Char.e.PerformTrade();
                else
                    this.Char.TradeStatus = TradeStatus.TRADE_CONFIRM;
            }
        }

        // 08 07
        public void OnTradeCancel(SagaMap.Packets.Client.GetTradeCancel p)
        {
            if (this.Char.trading != Trading.TRADING) return;
            Logger.ShowInfo("Trade cancelled", null);
            this.Char.TradeStatus = TradeStatus.NOT_CONFIRMED;
            SendTradeStatus(this.Char.id, TradeResults.TARGET_CANCELLED);
            SendTradeStatusOther(this.Char.TradeTarget, TradeResults.TARGET_CANCELLED);
            ResetTradeStatus(2);
            ResetTradeItems(2);
        }
        #endregion

        #region "Methods for trading"
        public void SendTradeStatus(uint targetid, TradeResults status)
        {
            Logger.ShowInfo("Sending trade status: " + status, null);
            /*Packets.Server.Trade sendPacket = new SagaMap.Packets.Server.Trade();
            sendPacket.SetActorID(targetid);
            sendPacket.SetStatus(status);
            this.netIO.SendPacket(sendPacket, this.SessionID);;*/
            this.Char.e.OnTradeStatus(targetid, status);
        }

        public void SendTradeStatusOther(uint targetid, TradeResults status)
        {
            Logger.ShowInfo("Sending trade status other: " + status, null);
            ActorPC target = (ActorPC)map.GetActor(targetid);
            if (target == null) return;
            target.e.OnTradeStatus(this.Char.id, status);
        }


        public void ResetTradeStatus(byte type)
        {
            switch (type)
            {
                case 1:
                    this.Char.TradeTarget = 0;
                    this.Char.trading = Trading.NOT_TRADING;
                    break;
                case 2:
                    this.Char.TradeTarget = 0;
                    this.Char.trading = Trading.NOT_TRADING;
                    ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);
                    if (target == null) return;
                    target.trading = Trading.NOT_TRADING;
                    target.TradeTarget = 0;
                    break;
            }
        }

        public void ResetTradeItems(byte type)
        {
            switch (type)
            {
                case 1:
                    this.TradeItems.Clear();
                    this.TradeMoney = 0;
                    break;
                case 2:
                    this.TradeItems.Clear();
                    this.TradeMoney = 0;
                    ActorPC target = (ActorPC)map.GetActor(this.Char.TradeTarget);
                    if (target == null) return;
                    target.e.OnResetTradeItems();
                    break;
            }
        }

        public void SendTradeResult(TradeResults result)
        {
            Packets.Server.TradeResult sendpacket = new SagaMap.Packets.Server.TradeResult();
            sendpacket.SetStatus(result);
            netIO.SendPacket(sendpacket, this.SessionID);

            // and update monies
            Packets.Server.SendZeny p1 = new SagaMap.Packets.Server.SendZeny();
            p1.SetZeny(this.Char.zeny);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void SendPartyInviteResult(Packets.Server.SendPartyInviteResult.Result result)
        {
            Packets.Server.SendPartyInviteResult p1 = new SagaMap.Packets.Server.SendPartyInviteResult();
            p1.SetResult(result);
            this.netIO.SendPacket(p1, this.SessionID);
        }
        #endregion


    }
}
