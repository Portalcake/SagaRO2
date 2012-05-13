using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetMail : Packet
    {
        public GetMail() // 0x0E01
        {
            this.size = 8;
        }

        public uint GetMailID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetMail();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetMail(this);
        }
    }
}
