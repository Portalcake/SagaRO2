using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
        public class ActorNPCInfo : Packet
        {
            private ushort addStatsCount;

            public ActorNPCInfo(byte addStatsCount)
            {
                this.data = new byte[38 + 4 * addStatsCount];
                this.offset = 4;
                this.ID = 0x060B;
                this.addStatsCount = addStatsCount;
                this.SetU1(1);
            }

            public void SetActorID(uint pID)
            {
                this.PutUInt(pID, 4);
            }

            public void SetNPCID(uint npcID)
            {
                this.PutUInt(npcID, 8);
            }

            public void SetLocation(float x, float y, float z)
            {
                this.PutFloat(x, 12);
                this.PutFloat(y, 16);
                this.PutFloat(z, 20);
            }

            public void SetYaw(int yaw)
            {
                this.PutInt(yaw, 24);
            }

            public void SetU1(byte U1)
            {
                this.PutByte(U1, 28);
            }

            public void SetHPSP(ushort HP, ushort maxHP, ushort SP, ushort maxSP)
            {
                this.PutUShort(HP, 29);
                this.PutUShort(maxHP, 31);
                this.PutUShort(SP, 33);
                this.PutUShort(maxSP, 35);
            }

            public void SetAdditionalStatus(int[] aStats)
            {
                if (aStats.Length != this.addStatsCount) return;
                for (int i = 0; i < aStats.Length; i++) this.PutByte((byte)aStats[i], (ushort)(37 + (i * 4)));
            }

        }
    }
