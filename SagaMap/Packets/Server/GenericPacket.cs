using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class GenericPacket : Packet
    {
        public GenericPacket()
        {
            this.data = new byte[2];
            this.offset = 2;
        }

        public void SetData(byte data)
        {
            byte[] temp = new byte[(this.data.Length + 1)];
            this.data.CopyTo(temp, 0);
            this.data = temp;

            this.PutByte(data, this.offset);
        }
            
    }
}
