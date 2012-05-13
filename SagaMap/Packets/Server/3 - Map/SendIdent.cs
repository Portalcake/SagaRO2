using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Send the identity of the mapserver to the client.
    /// </summary>
    public class SendIdent : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendIdent()
        {
            this.data = new byte[8];
            this.offset = 4;
            this.ID = 0x0301;
        }

        public void SetActorID(uint actorID)
        {
            this.PutUInt(actorID, 4);
        }

    }
}
