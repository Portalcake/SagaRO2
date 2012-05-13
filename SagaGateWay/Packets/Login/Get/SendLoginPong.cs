using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Get
{
    /// <summary>
    /// Client sending a GUID to the server.
    /// </summary>
    public class SendLoginPong : Packet
    {
        /// <summary>
        /// Create a SendGUID packet.
        /// </summary>
        public SendLoginPong()
        {
            this.size = 4;
            this.offset = 4;
        }
       

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Login.Get.SendLoginPong();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ControlPanelLoginSession)(client)).OnLoginPong();
        }


    }
}