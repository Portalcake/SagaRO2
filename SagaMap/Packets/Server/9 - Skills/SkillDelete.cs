using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillDelete : Packet
    {
        public SkillDelete()
        {
            this.data = new byte[10];
            this.ID = 0x0902;
            this.offset = 4;
            SetUnknown(3);
        }

        public void SetUnknown(byte id)
        {
            this.PutByte(id, 4);
        }

        public void SetSkill(uint id)
        {
            this.PutUInt(id, 5);
        }


    }
}
