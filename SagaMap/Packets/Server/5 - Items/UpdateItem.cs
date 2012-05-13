using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{

    public enum ITEM_UPDATE_TYPE { AMOUNT = 4 }; // 1,2,3,5 ???
    
    public class UpdateItem : Packet
    {

        public UpdateItem()
        {
            this.data = new byte[10];
            this.ID = 0x0513;
            this.offset = 4;
        }

        public void SetContainer(CONTAINER_TYPE container)
        {
            this.PutByte((byte)container, 4);
        }

        public void SetUpdateType(ITEM_UPDATE_TYPE type)
        {
            this.PutByte((byte)type, 5);
        }

        public void SetUpdateReason(SagaDB.Items.ITEM_UPDATE_REASON reason)
        {
            this.PutByte((byte)reason, 6);
        }

        public void SetItemIndex(byte index)
        {
            this.PutByte(index, 7);
        }

        public void SetAmount(byte amount)
        {
            this.PutByte(amount, 8);
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 9);
        }
    }
}