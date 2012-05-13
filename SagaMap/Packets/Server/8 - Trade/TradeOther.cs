using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class TradeOther : Packet
    {
        public TradeOther()
        {
            this.data = new byte[8];
            this.ID = 0x0802;
        }

        public void SetActorID(uint actor)
        {
            this.PutUInt(actor, 4);
        }
    }
}