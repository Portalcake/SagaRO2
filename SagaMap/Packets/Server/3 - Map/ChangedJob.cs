using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ChangedJob : Packet
    {
        public ChangedJob()
        {
            this.data = new byte[10];
            this.ID = 0x0311;
            this.offset = 4;            
        }

        public void SetJob(byte job) 
        {
            this.PutByte(job , 4);
        }

        public void SetUnknown(uint unknown)
        {
            this.PutUInt(unknown, 6);
        }
    }
}
