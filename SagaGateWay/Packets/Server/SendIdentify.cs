using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Server
{
    /// <summary>
    /// A packet requesting the GUID from the client.
    /// </summary>
    public class SendIdentify : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendIdentify()
        {
            this.data = new byte[10];
            this.offset = 10;
            this.isGateway = true;
            this.ID = 0x0204;

            //for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

    }
}
