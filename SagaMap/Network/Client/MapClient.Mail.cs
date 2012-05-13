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

        #region "0x0C"
        //0x0C Mail Packets

        public void OnGetInbox(SagaMap.Packets.Client.GetInbox p)
        {
            Packets.Server.MailList p1 = new SagaMap.Packets.Server.MailList();
            p1.SetMails(MapServer.charDB.GetMail(SearchType.Receiver, this.Char.name));
            p1.SetActorID(this.Char.CurTarget.id);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMailSend(SagaMap.Packets.Client.MailSend p)
        {
            Mail mail = new Mail();
            Item item;
            Packets.Server.MailSendAnswer p2 = new SagaMap.Packets.Server.MailSendAnswer();
            if (!MapServer.charDB.CharExists(0, p.GetName()))
            {
                p2.SetResult(SagaMap.Packets.Server.MailSendAnswer.Results.CHARACTER_NAME_NOT_EXIST);
                this.netIO.SendPacket(p2, this.SessionID);
                return;
            }
            uint fee = p.GetZeny();
            mail.content = p.GetContent();
            mail.date = DateTime.Now;
            mail.read = 0;
            mail.receiver = p.GetName();
            mail.sender = this.Char.name;
            mail.topic = p.GetTopic();
            mail.zeny = p.GetZeny();
            if (p.Unknown() != 0)
            {
                item = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, p.GetSlot());
                if (item == null)
                {
                    mail.valid = 7;
                }
                else
                {
                    fee += 10;
                    if (this.Char.zeny < fee)
                    {
                        p2.SetResult(SagaMap.Packets.Server.MailSendAnswer.Results.NOT_ENOUGH_ZENY);
                        this.netIO.SendPacket(p2, this.SessionID);
                        return;
                    }
                    mail.valid = 30;
                    mail.creator = item.creatorName;
                    mail.durability = item.durability;
                    mail.item = (uint)item.id;
                    mail.stack = item.stack;
                    this.map.RemoveItemFromActorPC(this.Char, p.GetSlot(), item.id, item.stack, ITEM_UPDATE_REASON.SOLD);
                }
            }
            else
            {
                mail.valid = 7;
            }
            this.Char.zeny -= fee;
            this.SendZeny();
            MapServer.charDB.NewMail(mail);
            MapClient receiver = MapClientManager.Instance.GetClient(p.GetName());
            if (receiver != null)
            {
                Packets.Server.MailArrived p1 = new SagaMap.Packets.Server.MailArrived();
                p1.SetAmount(1);
                receiver.netIO.SendPacket(p1, receiver.SessionID);
            }
            p2.SetResult(SagaMap.Packets.Server.MailSendAnswer.Results.OK);
            this.netIO.SendPacket(p2, this.SessionID);
        }

        public void OnGetMail(SagaMap.Packets.Client.GetMail p)
        {
            Packets.Server.SendMailData p1 = new SagaMap.Packets.Server.SendMailData();
            List<Mail> list = MapServer.charDB.GetMail(SearchType.MailID, p.GetMailID().ToString());
            if (list.Count > 0)
            {
                Mail mail = list[0];
                p1.SetSender(mail.sender);
                p1.SetDate(mail.date);
                p1.SetZeny(mail.zeny);
                p1.SetTopic(mail.topic);
                p1.SetItem(mail.item);
                p1.SetContent(mail.content);
                p1.SetUnknown(2);
                p1.SetCreator(mail.creator);
                p1.SetClv(4);
                p1.Durability(mail.durability);
                p1.Stack(mail.stack);
                this.netIO.SendPacket(p1, this.SessionID);
                mail.read = 1;
                MapServer.charDB.SaveMail(mail);
            }
        }

        public void OnGetMailItem(Packets.Client.GetMailItem p)
        {
            Packets.Server.GetMailItemAnswer p1 = new SagaMap.Packets.Server.GetMailItemAnswer();
            List<Mail> list = MapServer.charDB.GetMail(SearchType.MailID, p.GetMailID().ToString());
            if (list.Count > 0)
            {
                Mail mail = list[0];
                Item item;
                if (mail.item != p.GetItemID())
                {
                    p1.SetResult(SagaMap.Packets.Server.GetMailItemAnswer.Results.FAILED);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }
                if (!this.Char.inv.HasFreeSpace())
                {
                    p1.SetResult(SagaMap.Packets.Server.GetMailItemAnswer.Results.NOT_ENOUGH_SPACE);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }
                try
                {
                    item = new Item((int)mail.item, mail.creator, mail.durability, mail.stack);
                    mail.item = 0;
                    mail.creator = "";
                    mail.durability = 0;
                    mail.stack = 0;
                    MapServer.charDB.SaveMail(mail);
                    this.map.AddItemToActor(this.Char, item, ITEM_UPDATE_REASON.FOUND);
                    p1.SetResult(SagaMap.Packets.Server.GetMailItemAnswer.Results.OK);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }
                catch (Exception)
                {
                    p1.SetResult(SagaMap.Packets.Server.GetMailItemAnswer.Results.FAILED);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }

            }
            else
            {
                p1.SetResult(SagaMap.Packets.Server.GetMailItemAnswer.Results.FAILED);
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }

        public void OnGetMailZeny(Packets.Client.GetMailZeny p)
        {
            Packets.Server.GetMailZenyAnswer p1 = new SagaMap.Packets.Server.GetMailZenyAnswer();
            List<Mail> list = MapServer.charDB.GetMail(SearchType.MailID, p.GetMailID().ToString());
            if (list.Count > 0)
            {
                Mail mail = list[0];
                if (mail.zeny != p.GetZeny())
                {
                    p1.SetResult(SagaMap.Packets.Server.GetMailZenyAnswer.Results.FAILED);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }

                try
                {
                    this.Char.zeny += mail.zeny;
                    this.SendZeny();
                    mail.zeny = 0;
                    MapServer.charDB.SaveMail(mail);
                    p1.SetResult(SagaMap.Packets.Server.GetMailZenyAnswer.Results.OK);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }
                catch (Exception)
                {
                    p1.SetResult(SagaMap.Packets.Server.GetMailZenyAnswer.Results.FAILED);
                    this.netIO.SendPacket(p1, this.SessionID);
                    return;
                }
            }
            else
            {
                p1.SetResult(SagaMap.Packets.Server.GetMailZenyAnswer.Results.FAILED);
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }

        public void OnMailDelete(SagaMap.Packets.Client.MailDelete p)
        {
            Packets.Server.MailDeleteAnswer p1 = new SagaMap.Packets.Server.MailDeleteAnswer();
            try
            {
                MapServer.charDB.DeleteMail(p.GetMailID());
                p1.SetResult(SagaMap.Packets.Server.MailDeleteAnswer.Results.OK);
                this.netIO.SendPacket(p1, this.SessionID);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                p1.SetResult(SagaMap.Packets.Server.MailDeleteAnswer.Results.FAILED);
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }

        public void OnGetOutbox(SagaMap.Packets.Client.GetOutbox p)
        {
            Packets.Server.MailOutbox p1 = new SagaMap.Packets.Server.MailOutbox();
            List<Mail> list = MapServer.charDB.GetMail(SearchType.Sender, this.Char.name);
            List<Mail> tmp = new List<Mail>();
            foreach (Mail i in list)
            {
                //if it's not read
                if (i.read == 0)
                    tmp.Add(i);
            }
            p1.SetMails(tmp);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnMailCancel(SagaMap.Packets.Client.MailCancel p)
        {
            Packets.Server.MailCancelAnswer p1 = new SagaMap.Packets.Server.MailCancelAnswer();
            try
            {
                Mail mail = MapServer.charDB.GetMail(SearchType.MailID, p.GetMailID().ToString())[0];
                if (mail.item != 0)
                {
                    Item item = new Item((int)mail.item, mail.creator, mail.durability, mail.stack);
                    this.map.AddItemToActor(this.Char, item, ITEM_UPDATE_REASON.FOUND);
                }
                if (mail.zeny != 0)
                {
                    this.Char.zeny += mail.zeny;
                    this.SendZeny();
                }
                MapServer.charDB.DeleteMail(p.GetMailID());
                p1.SetResult(SagaMap.Packets.Server.MailCancelAnswer.Results.OK);
                this.netIO.SendPacket(p1, this.SessionID);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                p1.SetResult(SagaMap.Packets.Server.MailCancelAnswer.Results.FAILED);
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }

        public void OnGetOutMail(SagaMap.Packets.Client.GetOutMail p)
        {
            Packets.Server.SendOutMailData p1 = new SagaMap.Packets.Server.SendOutMailData();
            List<Mail> list = MapServer.charDB.GetMail(SearchType.MailID, p.GetMailID().ToString());
            if (list.Count > 0)
            {
                Mail mail = list[0];
                p1.SetSender(mail.receiver);
                p1.SetDate(mail.date);
                p1.SetZeny(mail.zeny);
                p1.SetTopic(mail.topic);
                p1.SetItem(mail.item);
                p1.SetContent(mail.content);
                p1.SetUnknown(2);
                p1.SetCreator(mail.creator);
                p1.SetClv(4);
                p1.Durability(mail.durability);
                p1.Stack(mail.stack);
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }
        #endregion

        #region "Method for mail"
        public void CheckNewMail()
        {
            List<Mail> list = MapServer.charDB.GetMail(SearchType.Receiver, this.Char.name);
            if (list == null) return;
            uint count = 0;
            foreach (Mail i in list)
            {
                //if it's not read
                if (i.read == 0)
                    count++;
            }
            if (count != 0)
            {
                Packets.Server.MailArrived p = new SagaMap.Packets.Server.MailArrived();
                p.SetAmount(count);
                this.netIO.SendPacket(p, this.SessionID);
            }
        }
        #endregion

    }
}
