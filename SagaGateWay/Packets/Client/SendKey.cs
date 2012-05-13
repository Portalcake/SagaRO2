using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class SendKey : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public SendKey()
        {
            this.size = 560;
            this.offset = 10;
        }

        /// <summary>
        /// Get the AES key that the client is sending from the packet.
        /// </summary>
        /// <returns>AES key as byte array.</returns>
        public byte[] GetKey()
        {
            return this.GetBytes(16, 266);
        }

        /// <summary>
        /// Get the number of collumns used by the algorithm
        /// </summary>
        /// <returns>Number of collumns</returns>
        public byte GetCollumns()
        {
            return this.GetByte(522);
        }

        /// <summary>
        /// Get the number of bytes used by the algorithm
        /// </summary>
        /// <returns>number of rounds</returns>
        public byte GetRounds()
        {
            return this.GetByte(526);
        }

        /// <summary>
        /// Get the direction of rotation for the algorithm
        /// </summary>
        /// <returns></returns>
        public byte GetDirection()
        {
            return this.GetByte(530);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.SendKey();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((GatewayClient)(client)).OnSendKey(this);
        }

    }
}
