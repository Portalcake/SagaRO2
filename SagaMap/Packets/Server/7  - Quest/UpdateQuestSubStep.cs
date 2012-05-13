using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class UpdateQuestSubStep : Packet
    {
        public UpdateQuestSubStep()
        {
            this.data = new byte[15];
            this.ID = 0x0709;
            this.offset = 4;
            this.SetUnknown(2);
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 4);
        }

        public void SetStep(uint s)
        {
            this.PutUInt(s, 8);                
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 12);
        }

        public void SetSubStep(byte s)
        {
            this.PutByte(s, 13);
        }

        public void SetAmmount(byte a)
        {
            this.PutByte(a, 14);
        }
    }
}
