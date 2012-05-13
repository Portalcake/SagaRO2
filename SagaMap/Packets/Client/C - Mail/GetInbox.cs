using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetInbox : Packet
    {
        public GetInbox() 
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetInbox();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetInbox(this);
        }
    }
}
