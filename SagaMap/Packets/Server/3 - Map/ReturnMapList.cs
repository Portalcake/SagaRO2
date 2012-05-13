using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ReturnMapList : Packet
    {
        public ReturnMapList()
        {
            this.data = new byte[7];
            this.ID = 0x031C;
            this.offset = 4;
            SetUnknown(1);
        }

        public void SetUnknown(byte type) // it is 1 for most of the time
        {
            this.PutByte(type, 4);
        }

        public void SetFromMap(byte id)
        {
            this.PutByte(id, 5);
        }

        public void SetToMap(byte id)
        {
            this.PutByte(id, 6);
        }
    }
}
