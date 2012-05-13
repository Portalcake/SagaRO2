using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class GwRequestNew : Packet
    {
        public GwRequestNew()
        {
            this.size = 4;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.GwRequestNew();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnRequestNew(this);
        }


    }
}