using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyMemberHPInfo : Packet
    {
        public PartyMemberHPInfo()
        {
            this.data = new byte[13];
            this.ID = 0x0D09;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetMaxHP(ushort hp)
        {
            this.PutUShort(hp, 9);
        }

        public void SetHP(ushort hp)
        {
            this.PutUShort(hp, 11);
        }
       
    }
}
