using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SetSpecialSkill : Packet
    {
        public SetSpecialSkill()
        {
            this.data = new byte[10];
            this.ID = 0x0918;
            this.offset = 4;
        }

        public void SetSlot(byte id)
        {
            this.PutByte(id, 4);
        }

        public void SetSkill(uint id)
        {
            this.PutUInt(id, 5);
        }


    }
}
