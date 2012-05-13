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

    public class WideMoveStart : Packet
    {
        public WideMoveStart(byte count)
        {
            this.data = new byte[12 + (count+1) * 16];
            this.offset = 4;
            this.ID = 0x0319;

            this.data[10] = 4;
            this.data[11] = count;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetSpeed(ushort speed)
        {
            this.PutUShort(speed, 8);
            
        }
        
        public void SetWaypoints(List<float[]> wpt, List<int> yaw)
        {
            for (int j = 0; j < wpt.Count; j++)
            {
                for (int i = 0; i < wpt[j].Length; i++)
                {
                    this.PutFloat(wpt[j][i], (ushort)(12 + i * 4 + j * 16));
                }
                this.PutInt(yaw[j], (ushort)(24 + j * 16));
            }
            
        }
    }
}

