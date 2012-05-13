using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ActorAnimation : Packet
    {
        public ActorAnimation()
        {
            this.data = new byte[13];
            this.ID = 0x0613;
            this.offset = 4;
        }

        public void SetActor(uint actorID)
        {
            this.PutUInt(actorID, 4);
        }

        public void SetAnimation(uint ani)
        {
            this.PutUInt(ani, 8);
        }
    }
}
