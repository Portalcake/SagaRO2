using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class OtherActorTeleport : Packet
    {
        public OtherActorTeleport()
        {
            this.data = new byte[20];
            this.ID = 0x031A;
            this.offset = 4;
        }

        public void SetActorID(uint ActorID)
        {
            this.PutUInt(ActorID, 4);
        }

        public void SetLocation(float x, float y, float z)
        {
            this.PutFloat(x, 8);
            this.PutFloat(y, 12);
            this.PutFloat(z, 16);
        }
    }
}
