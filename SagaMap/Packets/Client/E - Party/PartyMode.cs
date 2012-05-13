using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class PartyMode : Packet
    {
        public PartyMode() //0x0E05
        {
            this.size = 11;
        }

        public byte GetLootShare()
        {
            return this.GetByte(4);
        }

        public byte GetExpShare()
        {
            return this.GetByte(5);
        }

        public uint GetID()
        {
            return this.GetUInt(6);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.PartyMode();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyMode(this);
        }
    }
}
