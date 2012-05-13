using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class WeaponTypeChange : Packet
    {
        public WeaponTypeChange()
        {
            this.data = new byte[14];
            this.ID = 0x051E;
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 4);
        }

        public void SetWeaponAuge(uint auge)
        {
            this.PutUInt(auge, 5);
        }
        
        public void SetType(ushort value)
        {
            this.PutUShort(value, 9);
        }

        public void SetPostFix(ushort value)
        {
            this.PutUShort(value, 11);
        }

    }
}
