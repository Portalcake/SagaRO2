using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class NPCDropListResult : Packet
    {
        public enum Result
        {
            OK,
            NO_RIGHT,
            INVENTORY_FULL
        }
        public NPCDropListResult()
        {
            this.data = new byte[9];
            this.ID = 0x0518;
            this.offset = 4;            
        }

        public void SetResult(Result result)
        {
            this.PutByte((byte)result, 8);
        }
    }
}
