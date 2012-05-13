using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SendUsePortal : Packet
    {
        public SendUsePortal()
        {
            this.size = 8;
            this.offset = 4;
        }

        public byte GetPortalID()
        {
            return (byte)(this.GetByte(4) - 1);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendUsePortal();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendUsePortal(this);
        }
    }
}
