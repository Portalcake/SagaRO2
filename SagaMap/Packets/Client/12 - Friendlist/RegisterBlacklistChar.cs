using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class RegisterBlacklistChar: Packet
    {
        public RegisterBlacklistChar() 
        {
            //0x1204 
            this.size = 39;
            this.offset = 4;
        }

        public string GetName()
        {
            return this.GetString(4);
        }

        public byte GetReason()
        {
            return this.data[38];
        }
    }
}
