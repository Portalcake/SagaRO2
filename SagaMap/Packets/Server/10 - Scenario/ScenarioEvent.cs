using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ScenarioEvent : Packet
    {
        public ScenarioEvent()
        {
            this.data = new byte[12];
            this.ID = 0x1007;
            this.offset = 4;            
        }


        public void SetStep(uint currentquest)
        {
            this.PutUInt(currentquest, 4);            
        }

        public void SetActor(uint id)
        {
            this.PutUInt(id, 8);
        }
       
    }
}
