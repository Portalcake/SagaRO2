using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ActorTeleport : Packet
    {
        public ActorTeleport()
        {
            this.data = new byte[18];
            this.offset = 6;
            this.ID = 0x0315;

            this.data[4] = 0;  //Unknown, but constant?
            this.data[5] = 1;
        }

        public void SetLocation(float x, float y, float z)
        {
            this.PutFloat(x, 6);
            this.PutFloat(y, 10);
            this.PutFloat(z, 14);
        }
    }
}
