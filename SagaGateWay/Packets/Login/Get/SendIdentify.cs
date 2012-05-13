using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Get
{
    /// <summary>
    /// Client sending a GUID to the server.
    /// </summary>
    public class SendIdentify : Packet
    {
        /// <summary>
        /// Create a SendGUID packet.
        /// </summary>
        public SendIdentify()
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
            return (SagaLib.Packet)new SagaGateway.Packets.Login.Get.SendIdentify();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnSendIdentify(this);
        }


    }
}