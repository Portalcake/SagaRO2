using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class MailOutbox : Packet
    {
        public MailOutbox()
        {
            this.data = new byte[5];
            this.ID = 0x0C07;
            this.offset = 4;
        }        

        public void SetMails(List<Mail> mails)
        {
            this.PutByte((byte)mails.Count, 4);
            int j = 0;
            byte[] tmp = new byte[5 + 130 * mails.Count];
            this.data.CopyTo(tmp, 0);
            this.data = tmp;
            foreach (Mail i in mails)
            {
                this.PutUInt(i.ID, (ushort)(5 + 130 * j)); 
                //9 unknown bytes
                if (i.item != 0) this.PutByte(2, (ushort)(9 + 130 * j));
                this.PutUInt(i.item , (ushort)(10 + 130 * j));
                this.PutByte(i.stack, (ushort)(14 + 130 * j));
                this.PutString(Global.SetStringLength(i.receiver, 16), (ushort)(18 + 130 * j));
                this.PutString(Global.SetStringLength(string.Format("{0}-{1}-{2} {3}:{4}:00", i.date.Year, i.date.Month, i.date.Day
                , i.date.Hour, i.date.Minute), 19), (ushort)(52 + 130 * j));
                this.PutByte(i.valid, (ushort)(92 + 130 * j));
                this.PutString(Global.SetStringLength(i.topic, 19), (ushort)(93 + 130 * j));
                j++;
            }
        }
    }
}
