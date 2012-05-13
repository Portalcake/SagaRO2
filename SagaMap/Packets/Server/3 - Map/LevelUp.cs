using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class LevelUp : Packet
    {
        public LevelUp()
        {
            this.data = new byte[10];
            this.ID = 0x030C;
            this.offset = 4;
        }

        public void SetLevelType(byte type) // 1=base 2=job
        {
            this.PutByte(type, 4);
        }

        public void SetActorID(uint id)
        {
            this.PutUInt(id, 5);
        }

        public void SetLevels(byte levels)
        {
            this.PutByte(levels, 9);
        }
    }
}
