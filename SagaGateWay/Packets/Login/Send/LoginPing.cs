using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Send
{
    /// <summary>
    /// A packet requesting the GUID from the client.
    /// </summary>
    public class LoginPing : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginPing()
        {
            this.data = new byte[4];
            this.offset = 4;            
            this.ID = 0xFE01;
            //for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

    }
}
