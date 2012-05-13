using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetOutbox : Packet
    {
        public GetOutbox() 
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetOutbox();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetOutbox(this);
        }
    }
}
