using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class QuestNote : Packet
    {
        public QuestNote()
        {
            this.data = new byte[9];
            this.ID = 0x070D;
            this.offset = 4;
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 4);
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 8);
        }
    }
}
