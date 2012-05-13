using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class LeaveNPC : Packet
    {
        public LeaveNPC()
        {
            this.size = 4;
            this.offset = 4;
        }
       

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.LeaveNPC();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnLeaveNPC(this);
        }
    }
}
