using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketRegister: Packet
    {
        public MarketRegister() // 0x0F04
        {
            this.size = 11;
        }

        public byte ItemIndex()
        {
            return this.GetByte(4);
        }

        public byte StackCount()
        {
            return this.GetByte(5) ;
        }

        public uint Zeny()
        {
            return this.GetUInt(6);
        }

        public byte NumberOfDays()
        {
            return this.GetByte(10);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketRegister();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketRegister(this);
        }
    }
}
