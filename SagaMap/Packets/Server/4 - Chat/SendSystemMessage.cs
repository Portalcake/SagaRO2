using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendSystemMessage : Packet
    {
        public SendSystemMessage()
        {
            this.data = new byte[5];
            this.ID = 0x0403;
            this.offset = 4;
        }

        public void SetType(byte type)
        {
            if (type == 1 || type == 2)
                this.PutByte(type);
            else
                this.PutByte(2);
        }
    }
}
