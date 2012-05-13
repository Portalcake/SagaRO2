using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ScenarioStepComplete : Packet
    {
        public ScenarioStepComplete()
        {
            this.data = new byte[12];
            this.ID = 0x1003;
            this.offset = 4;            
        }


        public void SetStep(uint currentquest)
        {
            this.PutUInt(currentquest, 4);            
        }

        public void SetNextStep(uint id)
        {
            this.PutUInt(id, 8);
        }
       
    }
}
