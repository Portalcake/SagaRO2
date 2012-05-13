using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ShowMap : Packet
    {
        public ShowMap()
        {
            this.data = new byte[7];
            this.ID = 0x0522;
        }

        public void SetMap(byte map)
        {
            this.PutByte(map, 5);
        }

        public void SetZone(byte zone)
        {
            this.PutByte(zone, 6);
        }
    }
}
