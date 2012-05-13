using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class NPCLeave : Packet
    {
        public NPCLeave()
        {
            this.data = new byte[5];
            this.ID = 0x0621;
            this.offset = 4;
        }
    }
}
