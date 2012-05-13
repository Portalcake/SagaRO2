using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SetInventorySlotCount : Packet
    {
        public SetInventorySlotCount()
        {
            this.data = new byte[5];
            this.ID = 0x050C;
            this.offset = 4;
        }

        public void SetSlotCount(byte count)
        {
            this.PutByte(count, 4);
        }
    }
}
