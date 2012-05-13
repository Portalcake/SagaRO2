using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Map.Send
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
            this.data = new byte[16];
            this.offset = 4;
            this.ID = 0x010C;
        }

        public void SetCharID(uint id)
        {
            this.PutUInt(id, 4);
        }

        public void SetValidationKey(uint key)
        {
            this.PutUInt(key, 8);
        }

        public void SetSessionID(uint id)
        {
            this.PutUInt(id, 12);
        }

    }
}
