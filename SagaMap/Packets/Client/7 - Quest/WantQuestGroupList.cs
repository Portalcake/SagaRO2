using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class WantQuestGroupList : Packet
    {
        public WantQuestGroupList()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.WantQuestGroupList();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnWantQuestGroupList(this);
        }
    }
}
