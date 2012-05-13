using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendDelShortcut : Packet
    {
        public SendDelShortcut()
        {
            this.data = new byte[5];
            this.ID = 0x0A02;
        }

        public void SetSlot(byte slot)
        {
            this.PutByte(slot, 4);
        }
    }
}
