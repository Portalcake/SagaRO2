using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTradeOther : Packet //0x0802
    {
        public GetTradeOther()
        {
            this.size = 5;
        }

        public byte GetResponse()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetTradeOther();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradeOther(this);
        }
    }
}