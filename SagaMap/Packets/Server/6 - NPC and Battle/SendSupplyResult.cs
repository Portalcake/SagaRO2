using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendSupplyResult : Packet
    {
        public enum Results
        {
            OK,
            NOT_ENOUGH_SPACE = 2,
        }
        public SendSupplyResult()
        {
            this.data = new byte[6];
            this.ID = 0x0618;
            this.offset = 4;
        }
        
        public void SetResult(Results result)
        {
            this.PutByte((byte)result, 5);
        }
    }
}
