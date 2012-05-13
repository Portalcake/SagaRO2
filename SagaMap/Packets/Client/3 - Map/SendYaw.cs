using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendYaw : Packet
    {
        public SendYaw()
        {
            this.size = 8;
            this.offset = 4;
        }

        public int GetYaw()
        {
            return this.GetInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendYaw();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendYaw(this);
        }
    }
}
