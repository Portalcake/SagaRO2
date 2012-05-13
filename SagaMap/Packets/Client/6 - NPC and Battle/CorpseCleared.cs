using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class CorpseCleared : Packet
    {
        public CorpseCleared()
        {
            this.size = 4;
            this.offset = 4;
        }

       

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CorpseCleared();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnCorpseCleared(this);
        }
    }
}
