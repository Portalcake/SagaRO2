using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class WantCharList : Packet
    {
        public WantCharList()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.WantCharList();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnWantCharList(this);
        }
     }
}

