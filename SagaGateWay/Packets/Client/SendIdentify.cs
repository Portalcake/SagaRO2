using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class SendIdentify : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public SendIdentify()
        {
            this.size = 11;
            this.offset = 10;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.SendIdentify();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((GatewayClient)(client)).OnSendIdentify(this);
        }

    }
}
