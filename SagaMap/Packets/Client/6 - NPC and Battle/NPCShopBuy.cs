using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class NPCShopBuy : Packet
    {
        public NPCShopBuy()
        {
            this.size = 45;
            this.offset = 4;
        }

        public byte GetIndex()
        {
           return this.GetByte(5);
        }

        public byte GetAmount()
        {
           return this.GetByte(7);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.NPCShopBuy();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCShopBuy(this);
        }

    }
}
