using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Get
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
            this.size = 4;
            this.isGateway = true;
            this.isFullheader = true;
            this.offset = 4;
        }

        public override bool isStaticSize()
        {
            return false;
        }

        public byte[] GetData()
        {
            byte[] data = new byte[this.data.Length - 4];
            Array.Copy(this.data, 0, data, 0, 2);
            Array.Copy(this.data, 6, data, 2, this.data.Length - 6);
            return data;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Login.Get.SendUniversal();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnSendUniversal(this);
        }

    }
}
