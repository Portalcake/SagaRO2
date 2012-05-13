using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillLearned : Packet
    {
        public enum LearnResult
        {
            OK,
        }
        public SkillLearned()
        {
            this.data = new byte[5];
            this.ID = 0x091A;
            this.offset = 4;
        }

        public void SetResult(SagaMap.Skills.SkillHandler.SkillAddResault r)
        {
            this.PutByte((byte)r, 4);
        }

    }
}
