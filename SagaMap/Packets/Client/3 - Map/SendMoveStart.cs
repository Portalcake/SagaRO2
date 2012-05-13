using System;
using System.Collections.Generic;
using System.Text;

//native function SendActorMove(int Type, Vector Pos, Vector Accel, int Yaw, float DelayTime);
//SendMOVE(int, float, float, float, float, float, float, int, float)
/* CB1 packet
29 00 size
03 03 id

F1 C3 77 FF x
50 B2 F9 FE y
74 D6 10 00 z
04 DA 05 00 acceleration x
22 DE F5 05 acceleration y
00 00 00 00 acceleration z
D7 3F 00 00 yaw (rotation)
3F 81 68 01 delaytime
01          movetype (forwards = 01 / backwards = 02)
B6 04 00 00 ?, has something to do with the current location
*/
/* CB2 packet
2D 00 
03 03 

50 AE 12 01 
4D E1 20 FF 
AD F1 00 00

B8 0B F4 FA 
DC 1C D4 FC 
00 00 00 00 

FC 64 13 10 *new
DD 96 00 00 

73 BE F9 01 
01 
00 00 00 00
*/

using SagaLib;

namespace SagaMap.Packets.Client
{

    public class SendMoveStart: Packet
    {

        public SendMoveStart()
        {
            this.size = 45;
            this.offset = 4;
        }


        public float[] GetPos()
        {
            float[] pos = new float[3];

            pos[0] = this.GetFloat(4);
            pos[1] = this.GetFloat(8);
            pos[2] = this.GetFloat(12);

            return pos;
        }

        public float[] GetAcceleration()
        {
            float[] accel = new float[3];

            accel[0] = this.GetFloat(16);
            accel[1] = this.GetFloat(20);
            accel[2] = this.GetFloat(24);

            return accel;
        }

        public uint GetU1()
        {
            return this.GetUInt(28);
        }

        public int GetYaw()
        {
            return this.GetInt(32);
        }

        public uint GetDelayTime()
        {
            return this.GetUInt(36);
        }

        public byte GetMoveType()
        {
            return this.GetByte(40);
        }

        public int GetUnknown()
        {
            return this.GetInt(41);
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendMoveStart();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendMoveStart(this);
        }

    }
}
