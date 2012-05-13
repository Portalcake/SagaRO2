using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SpecialSkill : Packet
    {
        private int numskills;
        public struct SkillInfo
        {
            public uint skillID;
            public uint exp;
            public byte slot;
        }
        public SpecialSkill(int numSkills)
        {
            numskills = numSkills;
            this.data = new byte[148];
            this.ID = 0x090C;
            this.offset = 4;
        }

        public void SetSkills(SkillInfo[] skills)
        {
            if (skills.Length != numskills) { return; }
            for (int i = 0; i < numskills; i++)
            {
                Skills.Skill skill = Skills.SkillFactory.GetSkill(skills[i].skillID);
                for (int j = 0; j < skill.special; j++)
                {
                    this.PutUInt(skills[i].skillID, (ushort)(4 + j * 9 + skills[i].slot * 9));
                    this.PutUInt(skills[i].exp, (ushort)(8 + j * 9 + skills[i].slot * 9));
                    this.PutByte((byte)(skills[i].slot + j), (ushort)(12 + j * 9 + skills[i].slot * 9));
                }
            }
        }


    }
}
