using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

/*
23 00 
03 07 

7C 00 00 80 ActorID
20 7A 86 FE x
D8 CD 07 00 y
50 F4 33 00 z
5C 78 00 00 yaw (rotation)
58 02       speed
00          ?
00 00 00 00 ?
00 00 00 00 delaytime
*/

namespace SagaMap.Packets.Server
{

    public class ActorMoveStop : Packet
    {
        public ActorMoveStop()
        {
            this.data = new byte[35];
            this.offset = 4;
            this.ID = 0x0306;
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

        public void SetYaw(int yaw)
        {
            this.PutInt(yaw, 20);
        }

        public void SetSpeed(ushort speed)
        {
            this.PutUShort(speed, 24);
        }

        public void SetDelayTime(uint delay)
        {
            this.PutUInt(delay, 31);
        }

    }
}
