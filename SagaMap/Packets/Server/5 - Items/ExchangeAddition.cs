using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class ExchangeAddition : Packet
    {
        public ExchangeAddition()
        {
            this.data = new byte[16];
            this.ID = 0x051C;
            this.offset = 4;
        }

        public void SetID(uint id)
        {
            this.PutUInt(id, 4);
        }

        public void SetStatusID(uint u)
        {
            this.PutUInt(u, 8);
        }

        public void SetTime(uint u)
        {
            this.PutUInt(u, 12);
        }
        
    }

}

