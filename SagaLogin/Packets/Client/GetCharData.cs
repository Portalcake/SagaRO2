using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class GetCharData : Packet
    {
        public GetCharData()
        {
            this.size = 5;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.GetCharData();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnGetCharData(this);
        }

        public byte GetSelChar()
        {
            return this.GetByte(4);
        }
    }
}

