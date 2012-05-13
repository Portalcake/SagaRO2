using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendJump : Packet
    {
        public SendJump()
        {
            this.size = 20;
            this.offset = 4;
        }

        public float GetX()
        {
            return this.GetFloat(4);
        }

        public float GetY()
        {
            return this.GetFloat(8);
        }

        public float GetZ()
        {
            return this.GetFloat(12);
        }

        public uint GetYaw()
        {
            return this.GetUInt(16);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendJump();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendJump(this);
        }
    }
}
