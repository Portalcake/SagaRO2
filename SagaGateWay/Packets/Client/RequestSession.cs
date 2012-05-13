using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class RequestSession : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public RequestSession()
        {
            this.size = 10;
            this.offset = 4;
        }

        public override bool isStaticSize()
        {
            return false;
        }
        

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.RequestSession();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((GatewayClient)(client)).OnRequestSession(this);
        }

    }
}
