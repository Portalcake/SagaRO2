using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login.Send
{
    public class MapPong : Packet
    {
        public MapPong()
        {
            this.data = new byte[4];
            this.offset = 4;
            this.ID = 0xFE03;
        }       

    }

}
