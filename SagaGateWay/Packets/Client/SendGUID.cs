using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class SendGUID : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public SendGUID()
        {
            this.size = 30;
            this.offset = 10;
        }

        public string GetKey()
        {
            byte[] tmp;
            tmp = this.GetBytes(20, 10);
            return Conversions.bytes2HexString(tmp);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.SendGUID();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((GatewayClient)(client)).OnSendGUID(this);
        }

    }
}
