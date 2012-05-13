using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendSupplyMenu : Packet
    {
        public SendSupplyMenu()
        {
            this.data = new byte[12];
            this.ID = 0x0616;
            this.offset = 4;
        }
        
        public void SetMenuID(uint id)
        {
            this.PutUInt(id, 8);
        }
    }
}
