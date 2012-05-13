using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTargetCancel : Packet
    {
        public GetTargetCancel()
        {
            this.size = 8;
            this.offset = 4;
        }

        public uint GetActorID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetTargetCancel();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetCancel(this);
        }
    }
}
