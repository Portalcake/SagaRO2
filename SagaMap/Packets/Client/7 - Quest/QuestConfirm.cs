using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class QuestConfirm : Packet
    {
        public QuestConfirm()
        {
            this.size = 9;
            this.offset = 4;
        }

        public uint GetQuestID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.QuestConfirm();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnQuestConfirm(this);
        }
    }
}
