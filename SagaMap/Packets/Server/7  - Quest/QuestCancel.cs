using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class QuestCancel : Packet
    {
        public QuestCancel()
        {
            this.data = new byte[8];
            this.ID = 0x0704;
            this.offset = 4;
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 4);
        }       
    }
}
