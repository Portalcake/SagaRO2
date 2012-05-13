using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ChangeJobCosts : Packet
    {
        public ChangeJobCosts()
        {
            this.data = new byte[8];
            this.ID = 0x0323;
            this.offset = 4;
        }

        public void SetZeny(uint zeny) 
        {
            this.PutUInt(zeny , 4);
        }
    }
}
