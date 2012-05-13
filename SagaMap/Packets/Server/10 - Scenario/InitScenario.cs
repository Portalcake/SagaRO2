using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class InitScenario : Packet
    {
        public InitScenario()
        {
            this.data = new byte[14];
            this.ID = 0x1001;
            this.offset = 4;
            this.SetUnknown(2);
            this.SetUnknown2(1);
        }


        public void SetScenario(uint currentquest)
        {
            this.PutUInt(currentquest, 8);
        }

        public void SetUnknown(uint u1)
        {
            this.PutUInt(u1, 4);
        }

        public void SetUnknown2(ushort u2)
        {
            this.PutUShort(u2, 12);
        }
    }
}
