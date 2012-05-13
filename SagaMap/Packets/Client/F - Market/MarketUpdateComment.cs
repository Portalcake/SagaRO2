using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketUpdateComment: Packet
    {
        public MarketUpdateComment() // 0x0F06
        {
        }

        public string GetComment()
        {
            return this.GetString(1);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketUpdateComment();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketUpdateComment(this);
        }
    }
}
