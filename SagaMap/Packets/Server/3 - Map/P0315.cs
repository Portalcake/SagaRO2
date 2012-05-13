using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class P0315 : Packet
    {
        public P0315()
        {
            byte[] data = new byte[4];
            this.ID = 0x0315;
            this.data[4] = 0;
        }
    }
}
