using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class JobChangeCosts : Packet
    {
        public JobChangeCosts()
        {
            this.size = 5;
            this.offset = 4;
        }

        public byte GetJob()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.JobChangeCosts();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnJobChangeCosts(this);
        }
    }
}
