using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class SendNpcInventory : Packet
    {
        public SendNpcInventory()
        {
            this.data = new byte[9];
            this.ID = 0x0601;
        }

        public void SetActorID(uint actor)
        {
            this.PutUInt(actor, 4);
        }

        public void SetItems(List<Item> items)
        {
            int i = 0;
            int num = items.Count;
            byte count = (byte)num;
            this.PutByte(count, 8);

            byte[] tempdata = new byte[9 + (num*67)];
            this.data.CopyTo(tempdata, 0);
            this.data = tempdata;

            foreach (Item item in items)
            {
                Debug.Assert(item != null, "Invalid Loot Info", "Please check your loot table for invalid item id!");
                this.PutStandardItem(item.id, 0, 0, item.name, 0, (byte)item.req_clvl, item.tradeAble, item.durability, item.stack, item.addition1, item.addition2, item.addition3, (byte)i, (ushort)(9 + (67 * i)));                
                i++;
            }
        }

    }
}
