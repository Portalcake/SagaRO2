using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ChangeJob : Packet
    {
        public ChangeJob()
        {
            this.data = new byte[16];
            this.ID = 0x0312;
            this.offset = 4;
        }

        public void SetJobCounts(byte count) 
        {
            this.PutByte(count , 4);
        }

        public void SetPossibleJobs(List<byte> jobs)
        {
            int j = 0;
            foreach (byte i in jobs)
            {
                this.PutByte(i, (ushort)(5 + j));
                j++;
            }
        }
    }
}
