using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillCastCancel : Packet
    {
        public SkillCastCancel()
        {
            this.data = new byte[15];
            this.ID = 0x0906;
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

        public void SetActors(uint sourceID)
        {
            this.PutUInt(sourceID, 9);
            
        }

        public void SetU1(ushort U1)
        {
            this.PutUShort(U1, 13);
        }
    }
}
