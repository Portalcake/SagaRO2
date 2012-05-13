using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class PartyMemberPosition : Packet
    {
        public PartyMemberPosition()
        {
            this.data = new byte[17];
            this.ID = 0x0D15;
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetX(float x)
        {
            this.PutFloat(x, 9);
        }

        public void SetY(float y)
        {
            this.PutFloat(y, 13);
        }
       
    }
}
