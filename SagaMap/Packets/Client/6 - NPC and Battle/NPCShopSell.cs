using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class NPCShopSell : Packet
    {
        public NPCShopSell()
        {
            this.size = 35;
            this.offset = 4;
        }

        public byte GetContainer()
        {
            return this.GetByte(5);
        }

        public byte GetIndex()
        {
           return this.GetByte(6);
        }

        public byte GetAmount()
        {
           return this.GetByte(7);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.NPCShopSell();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCShopSell(this);
        }

    }
}
