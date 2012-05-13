using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class DropSelect : Packet
    {
        public DropSelect()
        {
            this.size = 9;
            this.offset = 4;
        }

        public uint GetActorID()
        {
            return this.GetUInt(4);
        }

        public byte GetIndex()//???
        {
           return this.GetByte(8);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.DropSelect();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDropSelect(this);
        }
    }
}
