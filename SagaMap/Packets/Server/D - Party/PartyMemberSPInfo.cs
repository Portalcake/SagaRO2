using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyMemberSPInfo : Packet
    {
        public PartyMemberSPInfo()
        {
            this.data = new byte[13];
            this.ID = 0x0D10;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetMaxSP(ushort sp)
        {
            this.PutUShort(sp, 9);
        }

        public void SetSP(ushort sp)
        {
            this.PutUShort(sp, 11);
        }
       
    }
}
