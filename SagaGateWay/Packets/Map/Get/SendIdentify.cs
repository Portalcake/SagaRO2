using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Map.Get
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
            this.size = 4;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Map.Get.SendIdentify();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapSession)(client)).OnSendIdentify(this);
        }


    }
}