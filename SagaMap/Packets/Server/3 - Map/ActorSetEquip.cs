using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{

    public class ActorSetEquip : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary> 
        public ActorSetEquip()
        {
            this.data = new byte[13];
            this.offset = 4;
            this.ID = 0x030D;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetEquipSlot(EQUIP_SLOT slot)
        {
            this.PutByte((byte)slot, 8);
        }

        public void SetEquipItemID(int eID)
        {
            this.PutInt(eID, 9);
        }

    }
}
