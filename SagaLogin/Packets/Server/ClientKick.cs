using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class ClientKick : Packet
    {
        public ClientKick()
        {
            this.data = new byte[8];
            this.offset = 4;
            this.ID = 0xFD02; 
        }

        public void SetSessionID(uint id)
        {
            this.PutUInt(id, 4);
        }

    }
}
