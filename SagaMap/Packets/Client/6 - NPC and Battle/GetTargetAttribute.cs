using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTargetAttribute : Packet
    {
        public GetTargetAttribute()
        {
            this.size = 9;
            this.offset = 4;
        }

        public uint GetActorID()
        {
            return this.GetUInt(4);
        }

        public byte GetUnknown()
        {
            return this.GetByte(8);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetTargetAttribute();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetAttribute(this);
        }
    }
}
