using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class RefreshBlacklist : Packet
    {
        public RefreshBlacklist() 
        {
            //0x1206 
            this.size = 4;
            this.offset = 4;
        }
    }
}
