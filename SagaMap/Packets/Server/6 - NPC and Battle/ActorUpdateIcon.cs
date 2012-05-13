using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ActorUpdateIcon : Packet
    {
        public ActorUpdateIcon()
        {
            this.data = new byte[9];
            this.ID = 0x060E;
            this.offset = 4;
        }

        public void SetActor(uint actorID)
        {
            this.PutUInt(actorID, 4);
        }

        public void SetIcon(byte icon)
        {
            this.PutByte(icon, 8);
        }
    }
}
