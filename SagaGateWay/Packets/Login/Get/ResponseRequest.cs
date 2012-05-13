using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Get
{
    /// <summary>
    /// Client sending a GUID to the server.
    /// </summary>
    public class ResponseRequest : Packet
    {
        /// <summary>
        /// Create a SendGUID packet.
        /// </summary>
        public ResponseRequest()
        {
            this.size = 8;
            this.offset = 4;
        }

        public uint GetSessionID()
        {
           return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Login.Get.ResponseRequest();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnResponseRequest(this);
        }


    }
}