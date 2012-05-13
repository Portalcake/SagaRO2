using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class SendUniversal : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public SendUniversal()
        {
            this.size = 14;
            this.isGateway = true;
            this.isFullheader = true;
            this.offset = 10;
        }

        public override bool isStaticSize()
        {
            return false;
        }

        public byte[] GetData()
        {
            byte[] data = new byte[this.data.Length - 10];
            Array.Copy(this.data, 10, data, 0, this.data.Length - 10);
            return data;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.SendUniversal();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((GatewayClient)(client)).OnSendUniversal(this);
        }

    }
}
