using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SendCRC : Packet
    {
        public SendCRC()
        {
            this.size = 60;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SendCRC();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSendCRC(this);
        }

    }
}
