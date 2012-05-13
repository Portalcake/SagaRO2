using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetMailZeny : Packet
    {
        public GetMailZeny() 
        {
            this.size = 12;
        }

        public uint GetMailID()
        {
            return this.GetUInt(4);
        }

        public uint GetZeny()
        {
            return this.GetUInt(8);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetMailZeny();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetMailZeny(this);
        }
    }
}
