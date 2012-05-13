using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendZeny : Packet
    {
        public SendZeny()
        {
            this.data = new byte[8];
            this.ID = 0x0515;
            this.offset = 4;            

        }

        public void SetZeny(uint zeny)
        {
            this.PutUInt(zeny,4);  // GameStringTable 2000-2051
        }
    }
}
