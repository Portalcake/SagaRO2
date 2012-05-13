using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Map.Get
{
    /// <summary>
    /// Client sending a GUID to the server.
    /// </summary>
    public class ClientKick : Packet
    {
        /// <summary>
        /// Create a SendGUID packet.
        /// </summary>
        public ClientKick()
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
            return (SagaLib.Packet)new SagaGateway.Packets.Map.Get.ClientKick();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapSession)(client)).OnKick(this);
        }


    }
}