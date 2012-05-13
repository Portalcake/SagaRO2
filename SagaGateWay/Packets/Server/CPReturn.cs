using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Server
{
    public class CPReturn : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CPReturn()
        {
            this.data = new byte[5];
            this.offset = 4;
            this.ID = 0xFFFE;
        }
                
        public void SetValue(byte value)
        {
            this.PutByte(value, 4);
        }


    }
}
