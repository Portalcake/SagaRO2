using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SkillCast : Packet
    {
        public SkillCast()
        {
            this.size = 13;
            this.offset = 4;
        }

        public byte GetSkillType()
        {
            return this.GetByte(4);
        }

        public uint GetSkillID()
        {
            return this.GetUInt(5);
        }

        public uint GetTargetActorID()
        {
            return this.GetUInt(9);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SkillCast();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSkillCast(this);
        }
    }
}


