using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class WeaponAdjust : Packet
    {
        public enum Function { Level=1, EXP, Durability }
        public WeaponAdjust()
        {
            this.data = new byte[10];
            this.ID = 0x0516;
        }

        public void SetFunction(Function func)
        {
            this.PutByte((byte)func, 4);
        }

        public void SetValue(uint value)
        {
            this.PutUInt(value, 6);
        }

    }
}
