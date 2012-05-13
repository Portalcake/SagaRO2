using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class DeleteItem : Packet
    {
        public DeleteItem()
        {
            this.data = new byte[7];
            this.ID = 0x0507;
            this.offset = 4;
        }


        public void SetContainer(CONTAINER_TYPE container)
        {
            this.PutByte((byte)container, 4);
        }

        public void SetAmount(byte amount)
        {
            this.PutByte(amount, 5);
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 6);
        }
    }
}
