using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyMemberLoot : Packet
    {
        public PartyMemberLoot()
        {
            this.data = new byte[13];
            this.ID = 0x0D17;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetItemID(uint id)
        {
            this.PutUInt(id, 9);
        }
       
    }
}
