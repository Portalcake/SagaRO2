using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Map.Send
{
     public class MapPing : Packet
    {
        public MapPing()
        {
            this.data = new byte[4];
            this.offset = 4;
            this.ID = 0xFE01;
        }
    }

}
