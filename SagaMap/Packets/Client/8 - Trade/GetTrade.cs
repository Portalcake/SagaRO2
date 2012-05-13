using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTrade : Packet //0x0801
    {
        public GetTrade()
        {
            this.size = 8;
        }

        public uint GetTargetActor()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetTrade();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTrade(this);
        }
    }
}
