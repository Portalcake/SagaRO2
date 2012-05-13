using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketDeleteItem : Packet
    {
        public MarketDeleteItem() // 0x0F05
        {
            this.size = 8;
        }

        public uint GetItemId()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketDeleteItem();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketDeleteItem(this);
        }
    }
}
