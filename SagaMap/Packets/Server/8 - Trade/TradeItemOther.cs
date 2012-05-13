using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class TradeItemOther : Packet
    {
        public TradeItemOther()
        {
            this.data = new byte[71];
            this.ID = 0x0804;
        }

        public void SetSlot(byte slot)
        {
            this.PutByte(slot, 4);
        }

        public void SetItem(Item item)
        {
            this.PutTradeItem(item.id,0,0,item.creatorName,0,(byte)item.req_clvl,item.tradeAble,item.durability,item.stack,item.addition1,item.addition2,item.addition3,5);
        }
    }
}
