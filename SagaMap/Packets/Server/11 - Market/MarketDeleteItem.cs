using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MarketDeleteIem : Packet
    {
        public MarketDeleteIem()
        {
            this.data = new byte[9];
            this.ID = 0x1105;
            this.offset = 4;               
        }

        public void SetReason(byte reason)
        {
            this.PutByte(reason, 4);
        }

        public void SetItemId(uint id)
        {
            this.PutUInt(id, 5);
        }
    }
}
