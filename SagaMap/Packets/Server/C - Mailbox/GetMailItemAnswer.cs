using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Mail;

namespace SagaMap.Packets.Server
{
    public class GetMailItemAnswer : Packet
    {
        public enum Results
        {
            OK,
            FAILED,
            NOT_ENOUGH_SPACE,
        }

        public GetMailItemAnswer()
        {
            this.data = new byte[5];
            this.ID = 0x0C04;
            this.offset = 4;
        }

        public void SetResult(Results r)
        {
            this.PutByte((byte)r, 4);
        }
       
        
    }
}
