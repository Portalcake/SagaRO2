using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTradeItem : Packet //0x0803
    {
        public GetTradeItem()
        {
            this.size = 7;
        }

        public byte GetSlot()
        {
            return this.GetByte(4);
        }

        public byte GetItem()
        {
            return this.GetByte(5);
        }

        public byte GetQuantity()
        {
            return this.GetByte(6);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetTradeItem();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradeItem(this);
        }
    }
}