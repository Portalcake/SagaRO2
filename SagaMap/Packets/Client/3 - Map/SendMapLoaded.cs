using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    /// <summary>
    /// Client packet indicating that the client is done loading the map.
    /// </summary>
    public class SendMapLoaded : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendMapLoaded()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendMapLoaded();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendMapLoaded(this);
        }
    }
}
