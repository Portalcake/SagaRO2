using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class GwLogout : Packet
    {
        public GwLogout()
        {
            this.size = 8;
            this.offset = 4;
        }

        public uint GetSessionId()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.GwLogout();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnLogout(this);
        }


    }
}