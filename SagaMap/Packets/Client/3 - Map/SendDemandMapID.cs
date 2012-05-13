using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    /// <summary>
    /// Client packet demanding to get the id of the mapserver it's trying to connect to.
    /// </summary>
    public class SendDemandMapID : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendDemandMapID()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendDemandMapID();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendDemandMapID(this);
        }
    }
}
