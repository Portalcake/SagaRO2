using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class AddItem : Packet
    {
        public AddItem()
        {
            this.data = new byte[73];
            this.ID = 0x0506;
            this.offset = 4;
        }

        public void SetContainer(CONTAINER_TYPE type)
        {
            this.PutByte((byte)type, 4);
        }
        
        //only used for equipment?
        public void SetEquipSlot(byte slot)
        {
            this.PutByte(slot, 5);
        }

        public void SetItem(Item item)
        {
            this.PutInt(item.id, 6);
            this.PutUInt(0); // unknown
            this.PutUInt(0); //unknown
            this.PutString(Global.SetStringLength(item.creatorName, 16));
            this.PutUShort(0, (ushort)(4 + 2 + 4 + 4+ 4 + (17*2) )); // unknown
            this.PutByte(0);//unknown
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
        }
    }

}

