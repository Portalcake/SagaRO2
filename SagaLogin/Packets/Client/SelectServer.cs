using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SelectServer : Packet
    {
        public SelectServer()
        {
            this.size = 5;
            this.offset = 4;
        }

        public byte GetSelServer()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SelectServer();
        }


        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSelectServer(this);
        }


    }
}
