using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class PartyAccept : Packet
    {
        public PartyAccept() //0x0E03
        {
            this.size = 5;
        }

        public byte GetStatus()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.PartyAccept();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyAccept(this);
        }
    }
}