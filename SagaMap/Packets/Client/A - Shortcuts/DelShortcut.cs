using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class DelShortcut : Packet
    {
        public DelShortcut()
        {
            this.size = 5;
        }

        public byte GetSlotNumber()
        {
            return this.GetByte(4);
        }
     
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.DelShortcut();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDelShortcut(this);
        }
    }
}
