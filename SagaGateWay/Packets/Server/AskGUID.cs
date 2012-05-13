using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Server
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
            this.data = new byte[30];
            this.offset = 10;
            this.isGateway = true;
            this.ID = 0x0202;

            //for (int i = 4; i < 24; i++) this.data[i] = 0;
        }

        public void PutKey(string key)
        {
            byte[] tmp = Conversions.HexStr2Bytes(key);
            System.Diagnostics.Debug.Assert(tmp.Length == 20, "Incorrect key length\r\nKey:" + key);
            this.PutBytes(tmp, 10);
        }

    }
}
