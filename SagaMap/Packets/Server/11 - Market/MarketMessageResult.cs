using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MarketMessageResult : Packet
    {
        public MarketMessageResult()
        {
            this.data = new byte[4];
            this.ID = 0x1106;
            this.offset = 4;               
        }
    }
}
