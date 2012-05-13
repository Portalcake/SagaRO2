using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class UnregisterBlacklistChar : Packet
    {
        public UnregisterBlacklistChar() 
        {
            //0x1205
            this.size = 38;
            this.offset = 4;
        }

        public string GetName()
        {
            return this.GetString(4);
        }
    }
}
