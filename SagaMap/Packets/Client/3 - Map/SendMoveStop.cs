using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

//native function SendActorStop(Vector Pos, int Yaw, float DelayTime);
//SendSTOP(float Pos.x, float Pos.y ,float Pos.z ,int Delaytime, float Yaw)
/*
1C 00 size
03 04 id

34 90 58 FF pos x
25 5D EE FE pos y
24 3C 0B 00 pos z
00 00 00 00 yaw (rotation)
A0 63 C8 01 delaytime
52 02       speed?
00 00       ?
*/

namespace SagaMap.Packets.Client
{

    public class SendMoveStop : Packet
    {

        public SendMoveStop()
        {
            this.size = 28;
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

        public int GetYaw()
        {
            return this.GetInt(16);
        }

        public uint GetDelayTime()
        {
            return this.GetUInt(20);
        }

        public ushort GetSpeed()
        {
            return this.GetUShort(24);
        }




        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendMoveStop();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendMoveStop(this);
        }

    }
}
