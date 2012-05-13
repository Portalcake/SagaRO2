using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class QuestRewardChoice : Packet
    {
        public QuestRewardChoice()
        {
            this.data = new byte[4];
            this.ID = 0x0708;
            this.offset = 4;
        }
    }
}
