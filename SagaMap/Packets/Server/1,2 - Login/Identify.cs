using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class Identify : Packet
    {
        public Identify()
        {
            this.data = new byte[4];
            this.offset = 4;
            // this.ID = 0x010A; // Login Server
            this.ID = 0x010B; // Map Server
        }

    }
}
