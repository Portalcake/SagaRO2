using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ItemAdjust : Packet
    {
        public enum Function { Level = 1, EXP, Durability, Active = 5, Tradable = 7 }
        public ItemAdjust()
        {
            this.data = new byte[10];
            this.ID = 0x0513;
        }

        public void SetContainer(byte con)
        {
            this.PutByte(con, 4);        
        }

        public void SetFunction(Function func)
        {
            this.PutByte((byte)func, 5);
        }

        public void SetSlot(byte slot)
        {
            this.PutByte(slot, 7);        
        }

        public void SetValue(ushort value)
        {
            this.PutUShort(value, 8);
        }

    }
}
