using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class NpcShopList : Packet
    {
        public NpcShopList()
        {
            this.data = new byte[13];
            this.ID = 0x0511;
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 9);
        }

        public void SetMoney(uint money)
        {
            this.PutUInt(money, 5);
        }

        public void SetItems(List<Item> items)
        {
            int i = 0;
            int num = items.Count;
            byte count = (byte)num;
            this.PutByte(count, 4);

            byte[] tempdata = new byte[15 + (num*68)];
            this.data.CopyTo(tempdata, 0);
            this.data = tempdata;
            foreach (Item item in items)
            {
                if (item == null) continue;
                this.PutStandardItem(item.id, 0, 0, item.name, 0, (byte)item.req_clvl,item.tradeAble,item.durability, 0, item.addition1, item.addition2, item.addition3,(byte)i, (ushort)(15 +(68 * i)));
                this.PutByte(1, (ushort)(82 + (68 * i)));//unknown
                i++;
            }
        }

    }
}
