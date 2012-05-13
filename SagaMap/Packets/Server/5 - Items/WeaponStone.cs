using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class WeaponStone : Packet
    {
        public WeaponStone()
        {
            this.data = new byte[12];
            this.ID = 0x050F;
        }

        public void SetUnknown(ushort u)
        {
            this.PutUShort(u, 4);
        }

        public void SetWeaponSlot(byte slot)
        {
            this.PutByte(slot, 6);
        }
        
        public void SetValue(uint value)
        {
            this.PutUInt(value, 7);
        }
    }
}
