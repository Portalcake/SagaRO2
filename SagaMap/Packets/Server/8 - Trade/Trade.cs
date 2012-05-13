using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class Trade : Packet
    {
        public Trade()
        {
            this.data = new byte[9];
            this.ID = 0x0801;
        }

        public void SetActorID(uint actor)
        {
            this.PutUInt(actor, 4);
        }

        public void SetStatus(TradeResults status)
        {
            byte stat = (byte)status;            
            this.PutByte(stat);
        }
    }
}
