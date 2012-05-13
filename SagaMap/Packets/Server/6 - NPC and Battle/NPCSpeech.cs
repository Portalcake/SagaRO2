using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class NPCSpeech : Packet
    {
        public NPCSpeech()
        {
            this.data = new byte[12];
            this.ID = 0x0605;
            this.offset = 4;            
        }
        
        public void SetScript(uint script) 
        {
            this.PutUInt(script, 4);
        }

        public void SetActor(uint actor)
        {
            this.PutUInt(actor, 8);
        }        
    }
}
