using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class Identify : Packet
    {
        public Identify()
        {
            this.data = new byte[8];
            this.offset = 4;
            this.ID = 0x010A; // Login Server
            // this.ID = 0x010B; // Map Server
        }

        public void SetSessionID(uint id)
        {
            this.PutUInt(id, 4);
        }

    }
}
