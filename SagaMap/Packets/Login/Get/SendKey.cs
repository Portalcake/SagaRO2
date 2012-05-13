using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login.Get
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
            this.size = 554;
            this.offset = 4;
        }

        /// <summary>
        /// Get the AES key that the client is sending from the packet.
        /// </summary>
        /// <returns>AES key as byte array.</returns>
        public byte[] GetKey()
        {
            return this.GetBytes(16, 260);
        }

        /// <summary>
        /// Get the number of collumns used by the algorithm
        /// </summary>
        /// <returns>Number of collumns</returns>
        public byte GetCollumns()
        {
            return this.GetByte(516);
        }

        /// <summary>
        /// Get the number of bytes used by the algorithm
        /// </summary>
        /// <returns>number of rounds</returns>
        public byte GetRounds()
        {
            return this.GetByte(520);
        }

        /// <summary>
        /// Get the direction of rotation for the algorithm
        /// </summary>
        /// <returns></returns>
        public byte GetDirection()
        {
            return this.GetByte(524);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Login.Get.SendKey();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnSendKey(this);
        }

    }
}
