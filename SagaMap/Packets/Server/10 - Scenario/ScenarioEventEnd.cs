using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ScenarioEventEnd : Packet
    {
        public ScenarioEventEnd()
        {
            this.data = new byte[8];
            this.ID = 0x1009;
            this.offset = 4;            
        }
        
        public void SetActor(uint id)
        {
            this.PutUInt(id, 4);
        }
       
    }
}
