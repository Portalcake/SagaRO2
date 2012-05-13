using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class TradeCancel : Packet
    {
        public TradeCancel()
        {
            this.data = new byte[4];
            this.ID = 0x0808;
        }
    }
}