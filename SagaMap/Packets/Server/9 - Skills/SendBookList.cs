using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendBookList : Packet
    {
        public SendBookList()
        {
            this.data = new byte[1034];
            this.ID = 0x091B;
            this.offset = 4;
        }

        public void SetMoney(uint money)
        {
            this.PutUInt(money, 6);
        }

        public void SetActorID(uint actor)
        {
            this.PutUInt(actor, 10);
        }

        public void SetBooks(List<SagaDB.Items.Item> books)
        {
            int i = 0;
            this.PutUShort((ushort)books.Count, 4);
            foreach (SagaDB.Items.Item item in books)
            {
                if (item == null) continue;
                this.PutUInt((uint)item.id, (ushort)(14 + 4 * i));
                i++;
            }
        }
    }
}
