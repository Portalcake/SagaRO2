using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SendVersion : Packet
    {
        public SendVersion()
        {
            this.size = 26;
            this.offset = 4;
        }

        public string GetVersionString()
        {
            return this.GetString(8);
        }

        public int GetIntVersion()
        {
            return this.GetInt(4);
        }

        public ushort GetUShortVersion()
        {
            return this.GetUShort(24);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SendVersion();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSendVersion(this);
        }

    }
}
