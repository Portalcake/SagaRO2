using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class QuestConfirmCancel : Packet
    {
        public QuestConfirmCancel()
        {
            this.size = 16;
            this.offset = 4;
        }

        public uint GetQuestID()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.QuestConfirmCancel();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnQuestConfirmCancel(this);
        }
    }
}
