using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class LoginPong : Packet
    {
        public LoginPong()
        {
            this.data = new byte[4];
            this.offset = 4;
            this.ID = 0xFE01; // Login Server
            // this.ID = 0x010B; // Map Server
        }
       

    }
}
