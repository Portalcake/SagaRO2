using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class TradeResult : Packet
    {
        public TradeResult()
        {
            this.data = new byte[8];
            this.ID = 0x0809;
        }

        public void SetStatus(TradeResults status)
        {
            uint gstnum = (uint)status;            
            this.PutUInt(gstnum);
        }
    }
}
