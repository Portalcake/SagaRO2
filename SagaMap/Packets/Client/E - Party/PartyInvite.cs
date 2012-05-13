using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class PartyInvite : Packet
    {
        public PartyInvite() // 0x0E01
        {
            this.size = 38;
        }

        public string GetName()
        {
            return this.GetString(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.PartyInvite();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyInvite(this);
        }
    }
}
