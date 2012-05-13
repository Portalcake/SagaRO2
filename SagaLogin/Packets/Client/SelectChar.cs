using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SelectChar : Packet
    {
        public SelectChar()
        {
            this.size = 9;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SelectChar();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSelectChar(this);
        }

        public int GetSelChar()
        {
            return this.GetInt(4);
        }

        public byte GetChanel()
        {
            return this.GetByte(8);
        }
    }
}

