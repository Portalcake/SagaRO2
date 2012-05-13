using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketGetComment: Packet
    {
        public MarketGetComment() // 0x0F07
        {
            this.size = 8;
        }

        public uint GetItemId()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketGetComment();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketGetComment(this);
        }
    }
}
