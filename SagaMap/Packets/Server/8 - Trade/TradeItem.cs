using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class TradeItem : Packet
    {
        public TradeItem()
        {
            this.data = new byte[8];
            this.ID = 0x0803;
        }

        public void SetTradeSlot(byte slot)
        {
            this.PutByte(slot, 4);
        }

        public void SetItemIndex(byte ind)
        {
            this.PutByte(ind, 5);
        }

        public void SetQuantity(byte quant)
        {
            this.PutByte(quant, 6);
        }

        public void SetStatus(byte stat)
        {
            this.PutByte(stat, 7);
        }
    }
}
