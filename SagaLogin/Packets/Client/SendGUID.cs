using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SendGUID : Packet
    {
        public SendGUID()
        {
            this.size = 24;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SendGUID();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSendGUID(this);
        }


    }
}