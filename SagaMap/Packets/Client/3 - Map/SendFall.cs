using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendFall : Packet
    {
        public SendFall()
        {
            this.size = 8;
            this.offset = 4;
        }

        public uint GetValue()
        {
            return this.GetUInt(4);
        }        

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendFall();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendFall(this);
        }
    }
}
