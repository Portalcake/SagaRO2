using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyNewMember : Packet
    {
        public PartyNewMember()
        {
            this.data = new byte[44];
            this.ID = 0x0D05;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 9);
        }

        public void SetName(string name)
        {
            Global.SetStringLength(name, 16);
            this.PutString(name, 10);
        }
    }
}
