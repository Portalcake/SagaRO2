using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class MailArrived : Packet
    {
        public MailArrived()
        {
            this.data = new byte[13];
            this.ID = 0x0C0B;
            this.offset = 4;
        }

        public void SetAmount(uint amount)
        {
            this.PutUInt(amount, 4);
        }
       
        
    }
}
