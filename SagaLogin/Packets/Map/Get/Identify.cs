using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Map.Get
{
    public class Identify : Packet
    {
        public Identify()
        {
            this.size = 2 + 25*2 + 25*2 + (30 * 4) + 2 + 15*2 + 2;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Map.Get.Identify();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnMapIdentify(this);
        }

        public string GetPass()
        {
            return this.GetString(4);
        }

        public string GetWorldName()
        {
            return this.GetString(4+25*2);
        }

        public int[] GetHostedMaps()
        {
            int[] hostedMaps = new int[30];
            for (int i = 0; i < 30; i++) hostedMaps[i] = this.GetInt((ushort)(4 + 25*2 + 25*2 + (4 * i)));

            return hostedMaps;
        }


        public string GetIP()
        {
            return this.GetString(4 + 25*2 + 25*2 + (4*30));
        }

        public short GetPort()
        {
            return this.GetShort(4 + 25*2 + 25*2 + (4 * 30) + 15*2 );
        }



    }
}

