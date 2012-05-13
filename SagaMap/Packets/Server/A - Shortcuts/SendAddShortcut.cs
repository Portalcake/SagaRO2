using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendAddShortcut : Packet
    {
        public SendAddShortcut()
        {
            this.data = new byte[10];
            this.ID = 0x0A01;
        }

        public void SetType(byte type)
        {
            this.PutByte(type, 4);
        }
        
        public void SetSlot(byte slot)
        {
            this.PutByte(slot, 5);
        }

        public void SetID(uint id)
        {
            this.PutUInt(id, 6);
        }
    }
}
