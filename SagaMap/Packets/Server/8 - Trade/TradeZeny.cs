using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class TradeZeny : Packet
    {
        public TradeZeny()
        {
            this.data = new byte[8];
            this.ID = 0x0805;
        }

        public void SetMoney(int money)
        {
            this.PutInt(money, 4);
        }
    }
}
