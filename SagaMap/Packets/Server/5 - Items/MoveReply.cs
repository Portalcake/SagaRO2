using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MoveReply : Packet
    {
        public MoveReply()
        {
            this.data = new byte[9];
            this.ID = 0x0501;
            this.offset = 4;

            this.data[4] = 0; // Unknown
            this.data[5] = 0; // Unknown

        }

        public void SetMessage(byte MessID)
        {
            this.PutByte(MessID, 6);  // GameStringTable 2000-2051
        }
    }
}
