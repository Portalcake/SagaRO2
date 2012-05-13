using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTradeCancel : Packet // 0x0807
    {
        public GetTradeCancel()
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetTradeCancel();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradeCancel(this);
        }
    }
}