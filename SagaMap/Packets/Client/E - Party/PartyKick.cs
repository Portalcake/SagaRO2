using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class PartyKick : Packet
    {
        public PartyKick() //0x0E05
        {
            this.size = 5;
        }

        public byte GetIndex()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.PartyKick();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyKick(this);
        }
    }
}
