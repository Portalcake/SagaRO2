using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class RemoveNavPoint : Packet
    {
        public RemoveNavPoint()
        {
            this.data = new byte[8];
            this.ID = 0x070C;
            this.offset = 4;
            this.data[4] = 1;//ammount of nav points, not implement yet
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 4);
        }

    }
}
