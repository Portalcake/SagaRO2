using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyMemberQuit : Packet
    {
        public PartyMemberQuit()
        {
            this.data = new byte[10];
            this.ID = 0x0D03;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 6);
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 5);
        }
       
    }
}
