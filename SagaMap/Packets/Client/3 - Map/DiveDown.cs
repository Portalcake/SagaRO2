using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    /// <summary>
    /// Client packet demanding to get the id of the mapserver it's trying to connect to.
    /// </summary>
    public class DiveDown : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DiveDown()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.DiveDown();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDiveDown();
        }
    }
}
