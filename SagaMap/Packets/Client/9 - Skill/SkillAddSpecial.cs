using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SkillAddSpecial : Packet
    {
        public SkillAddSpecial()
        {
            this.size = 5;
            this.offset = 4;
        }

        public byte GetIndex()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SkillAddSpecial();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillAddSpecial(this);
        }
    }
}


