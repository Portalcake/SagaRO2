using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class WantServerList : Packet
    {
        public WantServerList()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.WantServerList();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnWantServerList(this);
        }


    }
}
