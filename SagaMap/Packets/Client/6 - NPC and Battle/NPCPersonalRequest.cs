using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class NPCPersonalRequest : Packet
    {
        public NPCPersonalRequest()
        {
            this.size = 9;
            this.offset = 4;
        }

        public uint GetUnknown()
        {
            return this.GetUInt(4);
        }

        public byte GetValue()
        {
            return this.GetByte(8);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.NPCPersonalRequest();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPersonalRequest(this);
        }
    }
}
