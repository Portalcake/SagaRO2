using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class QuestRewardChoice : Packet
    {
        public QuestRewardChoice()
        {
            this.size = 5;
            this.offset = 4;
        }

        public byte GetChoice()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.QuestRewardChoice();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnQuestRewardChoice(this);
        }
    }
}
