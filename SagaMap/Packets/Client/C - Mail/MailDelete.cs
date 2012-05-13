using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MailDelete : Packet
    {
        public MailDelete() // 0x0E01
        {
            this.size = 8;
        }

        public uint GetMailID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MailDelete();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMailDelete(this);
        }
    }
}
