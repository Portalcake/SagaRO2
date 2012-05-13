using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketOwnItem : Packet
    {
        public MarketOwnItem() // 0x0F03
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketOwnItem();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketOwnItem(this);
        }
    }
}
