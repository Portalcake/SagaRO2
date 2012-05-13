using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login.Send
{
    public class Identify : Packet
    {
        public Identify()
        {
            this.data = new byte[2+ 2 + 25*2 + 25*2 + (30*4) + 15*2 + 2];
            this.offset = 4;
            this.ID = 0xff01;
        }

        public void SetLoginPassword(string pass)
        {
            pass = Global.SetStringLength(pass, 24);
            this.PutString(pass, 4);
        }

        public void SetWorldName(string worldName)
        {
            worldName = Global.SetStringLength(worldName, 24);
            this.PutString(worldName, 2+2+25*2);
        }

        public void SetHostedMaps(List<int> hostedMaps)
        {
            for (int i = 0; i < hostedMaps.Count; i++) 
               this.PutInt(hostedMaps[i], (ushort)(2 + 2 + 25*2 + 25*2 + (4*i) ));
        }

        public void SetIP(string sIP)
        {
            sIP = Global.SetStringLength(sIP, 14);
            this.PutString(sIP, (ushort)(2 + 2 + 25*2 + 25*2 + (4*30)));
        }

        public void SetPort(short port)
        {
            this.PutShort(port, (ushort)(2 + 2 + 25*2 + 25*2 + (4 * 30) + 15*2));
        }

    }

}
