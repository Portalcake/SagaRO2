using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class RegisterFriendlistChar : Packet
    {
        public RegisterFriendlistChar() 
        {
            //0x1201 
            this.size = 38;
            this.offset = 4;
        }

        public string GetName()
        {
            return this.GetString(4);
        }
    }
}
