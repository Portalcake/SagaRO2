using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class GetUseDye : Packet
    {
        public GetUseDye()
        {
            this.size = 11;
            this.offset = 4;
        }

        public byte GetDyeSlot()
        {
            return this.GetByte(4);
        }

        public CONTAINER_TYPE GetContainer()
        {
            return (CONTAINER_TYPE)this.GetByte(5);
        }

        public EQUIP_SLOT GetSlot()
        {
            return (EQUIP_SLOT)this.GetByte(6);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetUseDye();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnUseDye(this);
        }

    }
}
