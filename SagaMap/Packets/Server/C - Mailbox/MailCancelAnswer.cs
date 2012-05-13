using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class MailCancelAnswer : Packet
    {
        public enum Results
        {
            OK,
            FAILED,
        }
        public MailCancelAnswer()
        {
            this.data = new byte[5];
            this.ID = 0x0C08;
            this.offset = 4;
        }

        public void SetResult(Results r)
        {
            this.PutByte((byte)r, 4);
        }
       
        
    }
}
