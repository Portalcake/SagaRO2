using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class UpdateQuest : Packet
    {
        public UpdateQuest()
        {
            this.data = new byte[18];
            this.ID = 0x0706;
            this.offset = 4;
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 4);
        }

        public void SetStep(SagaDB.Quest.Step s)
        {
            this.PutUInt(s.ID, 8);
            this.PutByte(2, 12);
            this.PutUInt(s.nextStep, 13);
            this.PutByte(s.Status, 17);
        }
    }
}
