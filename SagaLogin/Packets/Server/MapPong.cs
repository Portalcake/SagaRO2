using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class MapPong : Packet
    {
        public MapPong()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0xFE02; // Login Server
            // this.ID = 0x010B; // Map Server
        }

        public void SetResult(byte result)
        {
            this.PutByte(result, 4);
        }
    }
}
