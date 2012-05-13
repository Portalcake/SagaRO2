using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class SendOutMailData : Packet
    {
        public SendOutMailData()
        {
            this.data = new byte[594];
            this.ID = 0x0C09;
            this.offset = 4;
        }

        public void SetUnknown(byte ID)
        {
            this.PutByte(ID, 4);
        }

        public void SetZeny(uint zeny)
        {
            this.PutUInt(zeny, 5);
        }

        public void SetSender(string sender)
        {
            this.PutString(Global.SetStringLength(sender, 16), 9);
        }

        public void SetDate(DateTime date)
        {
            string tmp = string.Format("{0}-{1}-{2} {3}:{4}:00", date.Year, date.Month, date.Day
                , date.Hour, date.Minute);
            this.PutString(Global.SetStringLength(tmp, 19), 43);
        }

        public void SetTopic(string topic)
        {
            this.PutString(Global.SetStringLength(topic, 20), 83);
        }
        

        public void SetContent(string content)
        {
            this.PutString(Global.SetStringLength(content, 200), 125);
        }

        public void SetItem(uint itemID)
        {
            this.PutUInt(itemID, 527);
        }

        public void SetCreator(string u3)
        {
            this.PutString(Global.SetStringLength(u3, 16), 539);
        }

        public void SetClv(ushort u4)
        {
            this.PutUShort(u4, 576);
        }

        public void Durability(ushort u5)
        {
            this.PutUShort(u5, 578);
        }

        public void Stack(ushort u6)
        {
            this.PutUShort(u6, 580);
        }

        
    }
}
