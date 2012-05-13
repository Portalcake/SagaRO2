using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Server
{
    /// <summary>
    /// A packet requesting the GUID from the client.
    /// </summary>
    public class SendPacket : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendPacket(int length)
        {
            this.data = new byte[10 + length];
            this.offset = 10;
            this.isGateway = true;
            this.isFullheader = true;
            
            //for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

        public void SetData(byte[] data)
        {
            Array.Copy(data, 0, this.data, 10, data.Length);
        }

    }
}
