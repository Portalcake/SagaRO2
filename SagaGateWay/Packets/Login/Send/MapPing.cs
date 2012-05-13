using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Send
{
    /// <summary>
    /// A packet requesting the GUID from the client.
    /// </summary>
    public class MapPing : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MapPing()
        {
            this.data = new byte[4];
            this.offset = 4;            
            this.ID = 0xFE02;
            //for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

    }
}
