using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class DeleteExchangeAddition : Packet
    {
        public DeleteExchangeAddition()
        {
            this.data = new byte[12];
            this.ID = 0x051D;
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
    }

}

