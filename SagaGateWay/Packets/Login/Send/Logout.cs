using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Login.Send
{
    /// <summary>
    /// A packet requesting the GUID from the client.
    /// </summary>
    public class Logout : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Logout()
        {
            this.data = new byte[8];
            this.offset = 4;            
            this.ID = 0xFD02;
            //for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

        public void SetSessionID(uint id)
        {
            this.PutUInt(id, 4);
        }

    }
}
