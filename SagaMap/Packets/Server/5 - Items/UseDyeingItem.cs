using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class UseDyeingItem : Packet
    {
        public UseDyeingItem()
        {
            this.data = new byte[11];
            this.ID = 0x0525;
            this.offset = 4;
        }

        public void SetError(byte data)
        {
            this.PutByte(data, 4);
        }

        public void SetItemID(int data)
        {
            this.PutInt(data, 5);
        }

        public void SetContainer(CONTAINER_TYPE container)
        {
            this.PutByte((byte)container, 9);
        }

        public void SetEquipment(EQUIP_SLOT item)
        {
            this.PutByte((byte)item, 10);
        }
    }
}