using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class QuestCompleted : Packet
    {
        public QuestCompleted()
        {
            this.size = 8;
            this.offset = 4;
        }

        public uint GetQuestID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.QuestCompleted();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnQuestCompleted(this);
        }
    }
}
