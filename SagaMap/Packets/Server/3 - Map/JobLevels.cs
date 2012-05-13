using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class JobLevels : Packet
    {
        public JobLevels()
        {
            this.data = new byte[15];
            this.ID = 0x0321;
            this.offset = 4;
        }

        public void SetJobLevels(Dictionary<SagaDB.Actors.JobType, byte> jlvs)
        {
            for (byte i = 1; i < 7; i++)
            {
                if (jlvs.ContainsKey((SagaDB.Actors.JobType)i))
                    this.PutByte(jlvs[(SagaDB.Actors.JobType)i], (ushort)(3 + i));
            }
        }
    }
}
