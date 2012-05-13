using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketGetMessage : Packet
    {
        public MarketGetMessage() // 0x0F08
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketGetMessage();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketGetMessage(this);
        }
    }
}
