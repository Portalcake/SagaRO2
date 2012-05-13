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

        #region "0x0F"
        //0x0F Packets======================================
        /// <summary>
        /// check if a marketplace item is still valid
        /// </summary>
        /// <param name="list"></param>
        private void CheckMarketItemValid(List<MarketplaceItem> list)
        {
            foreach (MarketplaceItem i in list)
            {
                if (i.expire < DateTime.Now)
                {
                    MapClient owner;
                    MapServer.charDB.DeleteMarketItem(i);
                    Mail mail = new Mail();
                    mail.content = "We have to inform you, that your registered item at <Regenbogen>\n" +
                        " has just expired. We've returned your item to you.\n We are looking forward to see you at <Regenbogen> again!";
                    mail.date = DateTime.Now;
                    mail.read = 0;
                    mail.receiver = i.owner;
                    mail.sender = "<Regenbogen>";
                    mail.topic = "Your item has expired at <Regenbogen>";
                    mail.valid = 30;
                    mail.creator = "";
                    mail.durability = i.item.durability;
                    mail.item = (uint)i.item.id;
                    mail.stack = i.item.stack;
                    MapServer.charDB.NewMail(mail);
                    owner = MapClientManager.Instance.GetClient(i.owner);
                    if (owner != null)
                    {
                        owner.CheckNewMail();
                    }
                }
            }
        }

        public void OnMarketSearch(Packets.Client.MarketSearch p)
        {
            /*
             * This function is handeld when the users does a search. The user can maximum show
             * 36 items per time. This is same as 3 pages, after those 3 pages it needs to be
             * requeried by the database.
             * 
             * p.GetSearchStartOffset is a zero based index, that indicated the amount at which offset 
             * is. So you can use SEARCHOFFSET * 36 and use that number in your query by using
             * LIMIT COMPUTED_OFFSET, 36
             * 
             */

            List<MarketplaceItem> list = MapServer.charDB.SearchMarketItem(MarketSearchOption.ItemType, p.GetSearchStartOffset(), p.GetItemType());
            CheckMarketItemValid(list);
            Packets.Server.MarketSearchResult p1 = new SagaMap.Packets.Server.MarketSearchResult();
            p1.SetUnknown(1);
            p1.SetItems(list);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMarketOwnItem(Packets.Client.MarketOwnItem p)
        {
            List<MarketplaceItem> list = MapServer.charDB.SearchMarketItem(MarketSearchOption.Owner, 0, this.Char.Name);
            Packets.Server.MarketOwnItemResult p1 = new SagaMap.Packets.Server.MarketOwnItemResult();
            p1.SetUnknown(1);
            p1.SetItems(list);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMarketGetMessage(Packets.Client.MarketGetMessage p)
        {
            /*
             * Expected packets:
             * SMSG_MARKETCOMMENT
             * 
             * This function is called when the user opens their own items he/she has registered
             * At that time a request to get your own comment is send, and ends up here.
             */

            Packets.Server.MarketMessage p1 = new SagaMap.Packets.Server.MarketMessage();
            p1.SetReason(0);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMarketBuyItem(Packets.Client.MarketBuyItem p)
        {
            uint id = p.GetItemId();
            MarketplaceItem item = MapServer.charDB.GetMarketItem(id);
            Packets.Server.MarketBuyItem p1 = new SagaMap.Packets.Server.MarketBuyItem();
            if (item == null)
            {
                p1.SetResult(1);
                this.netIO.SendPacket(p1, this.SessionID);
                return;
            }
            if (this.Char.zeny < item.price)
            {
                p1.SetResult(1);
                this.netIO.SendPacket(p1, this.SessionID);
                return;
            }
            Mail mail = new Mail();
            mail.content = "We are glad to inform you that your registered item at <Regenbogen> Market Place was bought by another Player" +
                 ".\n  Here is the money for the sold item.\n We are looking forward to see you at <Regenbogen> again!";
            mail.date = DateTime.Now;
            mail.read = 0;
            mail.receiver = item.owner;
            mail.sender = "<Regenbogen>";
            mail.topic = "Your item at <Regenbogen> was sold";
            mail.zeny = item.price;
            mail.valid = 30;
            MapServer.charDB.NewMail(mail);
            MapServer.charDB.DeleteMarketItem(item);
            MapClient receiver = MapClientManager.Instance.GetClient(item.owner);
            if (receiver != null)
            {
                receiver.CheckNewMail();
            }
            this.Char.zeny -= item.price;
            this.SendZeny();
            mail = new Mail();
            mail.content = "We are glad to inform you that you've successfully bought an item at <Regenbogen> Market Place from another Player" +
                 ".\n  Here is the bought item.\n We are looking forward to see you at <Regenbogen> again!";
            mail.date = DateTime.Now;
            mail.read = 0;
            mail.receiver = this.Char.Name;
            mail.sender = "<Regenbogen>";
            mail.topic = "You've bought an item at <Regenbogen>";
            mail.valid = 30;
            mail.creator = "";
            mail.durability = item.item.durability;
            mail.item = (uint)item.item.id;
            mail.stack = item.item.stack;
            MapServer.charDB.NewMail(mail);
            this.CheckNewMail();
            p1.SetResult(0);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMarketDeleteItem(Packets.Client.MarketDeleteItem p)
        {
            /*
             * Expected packets:
             * SMSG_MarketDelete
             * 
             * Additional expected packet:
             * SMSG_NewMailRecieved
             * 
             * Approach: check if the selected itemid still exists in the database
             * if so send marketdelete packet with a 0 reason (successfull) and a new mail message
             * 
             * If it fails you'ld need to send it with a failure reason, no clue which id's it are
             */
            uint id = p.GetItemId();
            MarketplaceItem item = MapServer.charDB.GetMarketItem(id);
            Packets.Server.MarketDeleteIem p1 = new SagaMap.Packets.Server.MarketDeleteIem();
            if (item == null)
            {
                p1.SetReason(1);
                this.netIO.SendPacket(p1, this.SessionID);
                return;
            }
            MapServer.charDB.DeleteMarketItem(item);
            Mail mail = new Mail();
            mail.content = "You've canceled a registered Auction at <Regenbogen> Market Place" +
                 ".\n  Here is the item you've handed to us.\n We are looking forward to see you at <Regenbogen> again!";
            mail.date = DateTime.Now;
            mail.read = 0;
            mail.receiver = this.Char.Name;
            mail.sender = "<Regenbogen>";
            mail.topic = "You've canceled an auction at <Regenbogen>";
            mail.valid = 30;
            mail.creator = "";
            mail.durability = item.item.durability;
            mail.item = (uint)item.item.id;
            mail.stack = item.item.stack;
            MapServer.charDB.NewMail(mail);
            this.CheckNewMail();
            p1.SetReason(0);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMarketGetComment(Packets.Client.MarketGetComment p)
        {
            /*
             * Expected packets:
             * SMSG_MARKETCOMMENT
             * 
             * This function is used when a user requests to see the comment message
             * of the selected auctionid. Of his current search results.
             * 
             * Approach: If not message was found or comment is 0-length send a reason
             * of 0x0B. This pops up a message thing user doesn't have comment set.
             */
            Packets.Server.MarketMessage p1 = new SagaMap.Packets.Server.MarketMessage();
            p1.SetReason(0xb);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMarketUpdateComment(Packets.Client.MarketUpdateComment p)
        {
            /*
             * Expected packets:
             * SMSG_MARKETMESSAGERESULT 
             * SMSG_MARKETCOMMENT
             * 
             * This function is used right after the user sets her comment message
             * 
             * Approach: Send SMSG_MARKETMESSAGERESULT to indicate we are handeling the 
             * update request. Send SMSG_MARKETCOMMENT to update the players comment.
             */
        }

        public void OnMarketRegister(Packets.Client.MarketRegister p)
        {
            /*
             * Expexted packets:
             * SMSG_MARKETREGISTER
             * SMSG_UPDATEZENY
             * SMSG_DELETEITEM | SMSG_UPDATEITEM
             * 
             * This packet registers a new item on the market
             * Costs for registering is 50 rufi per day, the expression days
             * are expressed in real-time days not gametime.
             * 
             * Index - describes the slot index.
             * 
             * Note: This packet isn't completly reversed, still searching for the number of days
             * some kind of reason. To indicate it failed registering, durabillity
             *
             */
            byte index = p.ItemIndex();
            byte stack = p.StackCount();
            uint price = p.Zeny();
            byte days = p.NumberOfDays();
            if (this.Char.zeny < (50 * days)) return;
            MarketplaceItem item = new MarketplaceItem();
            Item olditem = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, index);
            if (stack > olditem.stack) stack = olditem.stack;
            Item newitem = new Item(olditem.id, "", olditem.durability, stack);
            item.item = newitem;
            item.expire = (DateTime.Now + new TimeSpan(days, 0, 0, 0));
            item.owner = this.Char.Name;
            item.price = price;
            MapServer.charDB.RegisterMarketItem(item);
            this.map.RemoveItemFromActorPC(this.Char, index, olditem.id, stack, ITEM_UPDATE_REASON.OTHER);
            Packets.Server.MarketRegister p1 = new SagaMap.Packets.Server.MarketRegister();
            p1.SetAuctionID(item.id);
            p1.SetItemID((uint)item.item.id);
            p1.SetCount(stack);
            p1.SetReqClvl((byte)newitem.req_clvl);
            p1.SetZeny(price);
            this.netIO.SendPacket(p1, this.SessionID);
            this.Char.zeny -= (uint)(50 * days);
            this.SendZeny();
        }

        #endregion 

    }
}
