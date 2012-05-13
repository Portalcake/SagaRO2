using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib.Packets.Server
{
    /// <summary>
    /// A packet requesting the GUID from the client.
    /// </summary>
    public class AskGUID : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AskGUID()
        {
            this.data = new byte[24];
            this.offset = 4;
            this.ID = 0x0202;

            for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

    }
}
