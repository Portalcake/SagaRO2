using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    /// <summary>
    /// Client sending a GUID to the server.
    /// </summary>
    public class GwLogout : Packet
    {
        /// <summary>
        /// Create a SendGUID packet.
        /// </summary>
        public GwLogout()
        {
            this.size = 8;
            this.offset = 4;
        }

        public uint GetSessionId()
        {
            return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GwLogout();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnLogout(this);
        }


    }
}