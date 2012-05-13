using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendChangeState : Packet
    {
        public SendChangeState()
        {
            this.size = 10;
            this.offset = 4;
        }

        public byte GetState()
        {
            return this.GetByte(4);
        }

        public byte GetStance()
        {
            return this.GetByte(5);
        }

        public uint GetTargetActor()
        {
            return this.GetUInt(6);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendChangeState();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendChangeState(this);
        }
    }
}
