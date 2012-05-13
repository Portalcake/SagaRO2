using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MarketComment : Packet
    {
        public MarketComment()
        {
            this.data = new byte[255];
            this.ID = 0x1107;
            this.offset = 4;               
        }

        public void SetReason(byte reason)
        {
            this.PutByte(reason, 4);
        }

        public void SetComment(string comment)
        {
            int length = Encoding.Unicode.GetBytes(comment, 0, comment.Length, this.data, 6);
            Array.Resize<byte>(ref this.data, length + 6);
            this.data[5] = (byte)length;
        }
    }
}
