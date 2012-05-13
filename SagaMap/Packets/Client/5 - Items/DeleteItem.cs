using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class DeleteItem : Packet
    {
        public DeleteItem()
        {
            this.size = 11;
            this.offset = 4;
        }

        public CONTAINER_TYPE GetContainter()
        {
            byte container = this.GetByte(4);
            if (container == (byte)CONTAINER_TYPE.EQUIP) return CONTAINER_TYPE.EQUIP;
            else if(container == (byte)CONTAINER_TYPE.INVENTORY) return CONTAINER_TYPE.INVENTORY;
            else if (container == (byte)CONTAINER_TYPE.STORAGE) return CONTAINER_TYPE.STORAGE;

            return CONTAINER_TYPE.INVENTORY;
        }

        public byte GetItemIndex()
        {
            return this.GetByte(5);
        }

        public int GetItemID()
        {
            return this.GetInt(6);
        }

        public byte GetAmount()
        {
            return this.GetByte(10);
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.DeleteItem();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDeleteItem(this);
        }

    }
}
