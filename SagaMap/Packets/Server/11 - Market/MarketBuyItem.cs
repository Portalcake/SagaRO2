using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MarketBuyItem : Packet
    {
        public MarketBuyItem()
        {
            this.data = new byte[5];
            this.ID = 0x1102;
            this.offset = 4;                     
        }

        public void SetResult(byte u)
        {
            this.PutByte(u, 4);
        }
        
    }
}
