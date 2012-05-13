using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

/*  
0x05, 0x12,
 
0x9E, 0x0D, 0x00, 0x00, // ItemId
0x4B, 0xC7, 0x93, 0x45, //  ?
0xDE, 0x00, 0x03, 0x01, // ?
0x41, 0x00, 0x42, 0x00, 0x43, 0x00, 0x44, 0x00, 0x45, 0x00, 0x46, 0x00, 0x47, 0x00, 0x48, 0x00, 
0x49, 0x00, 0x4a, 0x00, 0x4b, 0x00, 0x4c, 0x00, 0x4d, 0x00, 0x4e, 0x00, 0x4f, 0x00, 0x50, 0x00, 
0x00, 0x00,// wchar[17] Creator name
0x00, 0x00 0x00, 0x00,  // ?
0xF0, 0x01, // Current durability
0x01, // quantity
0xD9, 0x27, 0x00, 0x00, // Addition1Id
0x90, 0x1D, 0x01, 0x00, // Addition2Id 
0xE6, 0xCE, 0x01, 0x00, // Addition3Id
0x00, // ?
0x01, // ?
*/

namespace SagaMap.Packets.Server
{
    public class ListEquipment : Packet
    {
        public ListEquipment()
        {
            this.data = new byte[4 + 68 * 16];
            this.ID = 0x0512;
            this.offset = 4;
        }

        public void SetEquipment(Dictionary<EQUIP_SLOT, Item> equip)
        {
            for (byte slot = 0; slot < 16; slot++)
            {
                if(equip.ContainsKey((EQUIP_SLOT)slot)) {
                    Item eItem = equip[(EQUIP_SLOT)slot];
                    if (eItem.durability == 0) eItem.active = 0;
                    ushort offset = 0;
                    //if (slot >= 3) offset = 2 * 67;
                    this.PutInt(eItem.id, (ushort)(4 + offset + (slot * 68)));
                    this.PutUInt(0); // unknown
                    this.PutUInt(0); // unknown
                    this.PutString(Global.SetStringLength(eItem.creatorName, 16));
                    this.PutUInt(0, (ushort)(4 + offset + (slot * 68) + 45)); // unknown
                    this.PutByte((byte)eItem.req_clvl);
                    if (eItem.tradeAble == false)
                        this.PutByte(1);
                    else
                        this.PutByte(0); 
                    this.PutUShort(eItem.durability); 
                    this.PutByte(eItem.stack); 
                    this.PutUInt(eItem.addition1); 
                    this.PutUInt(eItem.addition2); 
                    this.PutUInt(eItem.addition3); 
                    this.PutByte(0);
                    // active equipment (if an item blocks another slot, the blocked slot should be filled with an "inactive" item
                    this.PutByte(eItem.active);          
                }
            }
        }
    }
}
