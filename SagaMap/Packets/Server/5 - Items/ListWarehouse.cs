using System;
using System.Collections.Generic;
using System.Text;

using SagaDB;
using SagaDB.Items;
using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ListWarehouse : Packet
    {
        private byte numberOfItems;

        public ListWarehouse(byte numberOfItems)
        {
            this.data = new byte[10 + (numberOfItems*67)];
            this.ID = 0x0503;
            this.offset = 4;
            this.numberOfItems = numberOfItems;
            this.PutByte(numberOfItems, 5); // number of items
        }

        public void SetSortType(ITEM_TYPE type)
        {
            this.PutByte((byte)type, 4);
        }

        public void SetItems(List<Item> items)
        {
            // make sure we dont overflow
            if (items.Count > this.numberOfItems) return;

            int i = 0;
            foreach(Item item in items)
            {
                this.PutInt(item.id, (ushort)(10 + (67 * i)));
                this.PutUInt(0); // unknown
                this.PutUInt(0); // unknown
                this.PutString(Global.SetStringLength(item.creatorName, 16));
                this.PutUInt(0, (ushort)(10 + (67 * i ) + 45 )); // unknown
                this.PutByte((byte)item.req_clvl);
                if (item.tradeAble == false)
                    this.PutByte(1);
                else
                    this.PutByte(0);                
                this.PutUShort(item.durability);
                this.PutByte(item.stack);
                this.PutUInt(item.addition1);
                this.PutUInt(item.addition2);
                this.PutUInt(item.addition3); 
                this.PutByte(item.index);
                i++;
            }
        }


    }
}
