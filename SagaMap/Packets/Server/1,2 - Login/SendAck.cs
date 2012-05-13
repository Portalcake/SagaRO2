using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Acknowledgement packet sent by the server to the client as a response to....
    /// </summary>
    public class SendAck : Packet
    {
        /// <summary>
        /// Create a new acknowledgement packet. (which acknowledgement set to true as default)
        /// </summary>
        public SendAck()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0x0101;
        }

        /// <summary>
        /// Set acknowledgement to true or false.
        /// </summary>
        /// <param name="noerror">true or false</param>
        public void SetAck(bool noerror)
        {
            if (noerror) this.PutByte(0, 4); //no error
            else this.PutByte(1, 4); // error
        }

    }

}