using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class MarketSearch : Packet
    {
        public enum Class { Noraml = 1, Rare }
        public enum Mode { Name = 1 }
        public MarketSearch() // 0x0F01
        {
            this.size = 46;
        }

        public ushort GetSearchStartOffset()
        {
            return this.GetUShort(4);
        }

        public Class GetItemClass()
        {
            return (Class)this.GetByte(6);
        }

        public byte GetItemType()
        {
            return this.GetByte(7);
        }

        public byte GetSearchMode()
        {
            return this.GetByte(8);
        }

        public string GetSearchString()
        {
            return this.GetString(9);
        }

        public byte GetMinCLv()
        {
            return this.GetByte(43);
        }

        public byte GetMaxCLv()
        {
            return this.GetByte(44);
        }

        public byte SortBy()
        {
            return this.GetByte(45);
        }
       
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MarketSearch();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMarketSearch(this);
        }
    }
}
