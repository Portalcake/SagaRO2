using System;
using System.Collections.Generic;
using System.Text;
/*
30 00 
03 06 

5B 01 00 00 ActorID

BC C8 19 FF x
40 10 3C FF y
00 6F 0C 00 z

06 5A EC FB ax
92 26 59 04 ay
00 00 00 00 az

AF 5E 00 00 yaw

F4 01       speed?

00 
C4 FD C6 01 delay1?
01
00 00 00 00 delay2?
*/


using SagaLib;

namespace SagaMap.Packets.Server
{

    public class ActorMoveStart : Packet
    {
        public ActorMoveStart()
        {
            this.data = new byte[48];
            this.offset = 4;
            this.ID = 0x0305;

            this.data[38] = 0;
            this.data[43] = 1;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetLocation(float x, float y, float z)
        {
            this.PutFloat(x, 8);
            this.PutFloat(y, 12);
            this.PutFloat(z, 16);
        }

        public void SetAcceleration(float ax, float ay, float az)
        {
            this.PutFloat(ax, 20);
            this.PutFloat(ay, 24);
            this.PutFloat(az, 28);
        }

        public void SetYaw(int yaw)
        {
            this.PutInt(yaw, 32);
        }

        public void SetSpeed(ushort speed)
        {
            this.PutUShort(speed, 36);
        }

        public void SetDelay0(uint delay)
        {
            this.PutUInt(delay, 39);
        }

        public void SetDelay1(uint delay)
        {
            this.PutUInt(delay, 44);
        }

    }
}

