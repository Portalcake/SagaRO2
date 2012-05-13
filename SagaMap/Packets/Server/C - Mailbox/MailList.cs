using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class MailList : Packet
    {
        public MailList()
        {
            this.data = new byte[9];
            this.ID = 0x0C01;
            this.offset = 4;
        }

        public void SetActorID(uint ID)
        {
            this.PutUInt(ID, 5);
        }

        public void SetMails(List<Mail> mails)
        {
            this.PutByte((byte)mails.Count, 4);
            int j = 0;
            byte[] tmp = new byte[9 + 131 * mails.Count];
            this.data.CopyTo(tmp, 0);
            this.data = tmp;
            foreach (Mail i in mails)
            {
                this.PutUInt(i.ID, (ushort)(9 + 131 * j)); 
                if (i.item != 0) this.PutByte(2, (ushort)(13 + 131 * j));
                this.PutUInt(i.item, (ushort)(14 + 131 * j));
                this.PutByte(i.stack, (ushort)(18 + 131 * j));
                this.PutString(Global.SetStringLength(i.sender, 16), (ushort)(22 + 131 * j));
                this.PutString(Global.SetStringLength(string.Format("{0}-{1}-{2} {3}:{4}:00", i.date.Year, i.date.Month, i.date.Day
                , i.date.Hour, i.date.Minute), 19), (ushort)(56 + 131 * j));
                this.PutByte(i.valid, (ushort)(96 + 131 * j));
                this.PutString(Global.SetStringLength(i.topic, 19), (ushort)(97 + 131 * j));
                this.PutByte(i.read, (ushort)(137 + 131 * j));
                j++;
            }
        }
    }
}
