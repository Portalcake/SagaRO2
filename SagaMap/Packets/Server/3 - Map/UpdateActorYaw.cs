using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class UpdateActorYaw : Packet
    {
        public UpdateActorYaw()
        {
            this.data = new byte[12];
            this.ID = 0x0307;
            this.offset = 4;
        }

        public void SetActor(uint ActorID)
        {
            this.PutUInt(ActorID, 4);
        }

        public void SetYaw(int yaw)
        {
            this.PutInt(yaw, 8);
        }
    }
}
