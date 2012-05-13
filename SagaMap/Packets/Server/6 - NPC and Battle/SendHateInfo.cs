using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendHateInfo : Packet
    {
        public SendHateInfo()
        {
            this.data = new byte[10];
            this.ID = 0x0609;
            this.offset = 4;
        }

        public void SetActor(uint actorID)
        {
            this.PutUInt(actorID, 4);
        }

        public void SetHateInfo(ushort hateinfo)
        {
            this.PutUShort(hateinfo, 8);
        }
    }
}
