using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillEXP : Packet
    {
        public SkillEXP()
        {
            this.data = new byte[12];
            this.ID = 0x0915;
            this.offset = 4;
        }

        public void SetSkillID(uint id)
        {
            this.PutUInt(id, 4);        
        }

        public void SetEXP(uint exp)
        {
            this.PutUInt(exp, 8);
        }


    }
}
