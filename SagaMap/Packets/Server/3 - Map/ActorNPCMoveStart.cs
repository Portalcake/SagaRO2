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

    public class ActorNPCMoveStart : Packet
    {
        public ActorNPCMoveStart(byte count)
        {
            this.data = new byte[12 + (count+1) * 12];
            this.offset = 4;
            this.ID = 0x0305;

            this.data[10] = 3;
            this.data[11] = 2;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetSpeed(ushort speed)
        {
            this.PutUShort(speed, 8);
            
        }
        
        public void SetWaypoints(float[] wpt)
        {
            for (int i = 0; i < wpt.Length; i++)
            {
                this.PutFloat(wpt[i], (ushort)(12 + i * 4));
            }
            for (int i = 0; i < wpt.Length; i++)
            {
                this.PutFloat(wpt[i], (ushort)(24 + i * 4));
            }
        }
    }
}

