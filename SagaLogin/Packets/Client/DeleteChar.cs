using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class DeleteChar : Packet
    {
        public DeleteChar()
        {
            this.size = 39;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.DeleteChar();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnDeleteChar(this);
        }

        public byte GetCharIndex()
        {
            return this.GetByte(4);
        }
        public string GetCharName()
        {
            return this.GetString(5);
        }
    }
}
