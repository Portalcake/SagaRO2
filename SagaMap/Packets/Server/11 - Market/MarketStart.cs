using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MarketStart : Packet
    {
        public MarketStart()
        {
            this.data = new byte[9];
            this.ID = 0x1109;
            this.offset = 4;
            this.SetUnknown(0);            
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }
        
    }
}
