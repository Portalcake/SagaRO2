using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class InviteCancel : Packet
    {
        public InviteCancel() //0x0E08
        {
            this.size = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.InviteCancel();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyInviteCancel(this);
        }
    }
}
