using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillCast : Packet
    {
        public SkillCast()
        {
            this.data = new byte[19];
            this.ID = 0x0904;
            this.offset = 4;
        }

        public void SetSkillType(byte type)
        {
            this.PutByte(type, 4);
        }

        public void SetSkillID(uint skillID)
        {
            this.PutUInt(skillID, 5);
        }

        public void SetActors(uint sourceID, uint targetID)
        {
            this.PutUInt(sourceID, 9);
            this.PutUInt(targetID, 13);
        }

        public void SetU1(ushort U1)
        {
            this.PutUShort(U1, 17);
        }
    }
}
