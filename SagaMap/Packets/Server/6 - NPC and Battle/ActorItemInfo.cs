using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
        public class ActorItemInfo : Packet
        {
            
            public ActorItemInfo()
            {
                this.data = new byte[41];
                this.offset = 4;
                this.ID = 0x060C;
                SetU1(1);
                SetU2(1);
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
            
            public void SetU2(byte U1)
            {
                this.PutByte(U1, 39);
            }
            
            public void SetActive(byte U1)
            {
                this.PutByte(U1, 40);
            }

        }
    }
