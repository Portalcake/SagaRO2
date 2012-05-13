using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class RequestHeartbeat : Packet
    {
        public RequestHeartbeat()
        {
            this.data = new byte[4];
            this.ID = 0xFF00;
        }
    }
}
