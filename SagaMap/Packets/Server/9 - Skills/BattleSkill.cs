using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class BattleSkill : Packet
    {
        private int numskills;
        public struct SkillInfo
        {
            public uint skillID;
            public uint exp;
            public byte unknown;
        }
        public BattleSkill(int numSkills)
        {
            numskills = numSkills;
            this.data = new byte[5+numSkills*9];
            this.ID = 0x090A;
            this.offset = 4;
            this.PutByte(Convert.ToByte(numSkills));
           }

        public void SetSkills(SkillInfo[] skills)
        {
            if (skills.Length != numskills) { return; }
            for (int i = 0; i < numskills; i++)
            {
                this.PutUInt(skills[i].skillID);
                this.PutUInt(skills[i].exp);
                skills[i].unknown = 1;
                this.PutByte(skills[i].unknown);
            }
        }


    }
}
