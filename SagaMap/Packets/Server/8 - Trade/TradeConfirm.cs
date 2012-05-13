using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class TradeConfirm : Packet
    {
        public TradeConfirm()
        {
            this.data = new byte[4];
            this.ID = 0x0807;
        }
    }
}
