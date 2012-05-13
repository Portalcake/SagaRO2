using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class NPCNote : Packet
    {
        public NPCNote()
        {
            this.data = new byte[12];
            this.ID = 0x070E;
            this.offset = 4;
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 4);
        }

        public void SetSetpID(uint s)
        {
            this.PutUInt(s, 8);
        }
    }
}
