using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class WeaponMove : Packet
    {
        public WeaponMove()
        {
            this.data = new byte[8];
            this.ID = 0x0508;
        }

        public void SetDirection(byte dir)
        {
            this.PutByte(dir, 4);
        }

        public void SetSlot(byte slot)
        {
            this.PutByte(slot, 5);
        }

        public void SetPosition(byte pos)
        {
            this.PutByte(pos, 6);
        }

        public void SetStatus(byte stat)
        {
            this.PutByte(stat, 7);
        }
    }
}
