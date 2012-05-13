using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class OffensiveSkill : Packet
    {
        public OffensiveSkill()
        {
            this.data = new byte[25];
            this.ID = 0x0908;
            this.offset = 4;
            this.SetU2(0xFF);
        }

        public void SetSkillType(byte type)
        {
            this.PutByte(type, 4);
        }

        public void SetIsCritical(byte crit)
        {
            this.PutByte(crit, 5);
        }

        public void SetSkillID(uint skillID)
        {
            this.PutUInt(skillID, 6);
        }

        public void SetActors(uint sourceID, uint targetID)
        {
            this.PutUInt(sourceID, 10);
            this.PutUInt(targetID, 14);
        }

        public void SetDamage(uint dmg)
        {
            this.PutUInt(dmg, 18);
        }

        public void SetU1(ushort U1)
        {
            this.PutUShort(U1, 22);
        }

        public void SetU2(byte u2)
        {
            this.PutByte(u2);
        }
    }
}
