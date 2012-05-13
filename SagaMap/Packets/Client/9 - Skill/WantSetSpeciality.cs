using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class WantSetSpeciality : Packet
    {
        public WantSetSpeciality()
        {
            this.size = 4;
            this.offset = 4;
        }

        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.WantSetSpeciality();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnWantSetSpeciality(this);
        }
    }
}


