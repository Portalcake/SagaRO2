using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MailCancel : Packet
    {
        public MailCancel() // 0x0E01
        {
            this.size = 8;
        }

        public uint GetMailID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MailCancel();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMailCancel(this);
        }
    }
}
