using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class MailSendAnswer : Packet
    {
        public enum Results
        {
            OK,
            FAILED,
            CHARACTER_NAME_NOT_EXIST,
            NOT_ENOUGH_ZENY,
        }
        public MailSendAnswer()
        {
            this.data = new byte[5];
            this.ID = 0x0C02;
            this.offset = 4;
        }

        public void SetResult(Results r)
        {
            this.PutByte((byte)r, 4);
        }
       
        
    }
}
