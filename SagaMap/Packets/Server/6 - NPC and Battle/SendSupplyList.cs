using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendSupplyList : Packet
    {
        public class SupplyItem
        {
            public uint itemID;
            public byte amount;
        }

        public SendSupplyList()
        {
            this.data = new byte[3423];
            this.ID = 0x0615;
            this.offset = 4;
        }

        public void SetActor(uint actorID)
        {
            this.PutUInt(actorID, 4);
        }

        public void SetSupplyID(uint ID)
        {
            this.PutUInt(ID, 8);
        }
       
        public void SetProducts(List<SupplyItem> list)
        {
            this.PutByte((byte)list.Count, 21);
            int j = 0;
            foreach (SupplyItem i in list)
            {
                this.PutUInt(i.itemID, (ushort)(123 + j * 66));
                this.PutByte(2, (ushort)(169 + j * 66));
                this.PutByte(i.amount, (ushort)(176 + j * 66));
                j++;
            }
        }

        public void SetMatrial(List<SupplyItem> list)
        {
            this.PutByte((byte)list.Count, 22);
            int j = 0;
            foreach (SupplyItem i in list)
            {
                this.PutUInt(i.itemID, (ushort)(1773 + j * 66));
                this.PutByte(1, (ushort)(1822 + j * 66));
                this.PutByte(i.amount, (ushort)(1826 + j * 66));
                j++;
            }
        }

    }
}
