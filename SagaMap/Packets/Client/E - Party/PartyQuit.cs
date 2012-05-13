using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class PartyQuit : Packet
    {
        public PartyQuit() //0x0E04
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.PartyQuit();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyQuit(this);
        }
    }
}
