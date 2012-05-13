using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

/*
[22:14:45] [  DATA  ] Client => Server
[22:14:45] -- Packet Command:03-0A Length:9
[  DATA  ] (0x0000)  02 02 00 00 00   
 *** Add to base stats ***
 * str
 * dex
 * int
 * con
 * pointsleft

*/

namespace SagaMap.Packets.Client
{
    public class GetStatUpdate : Packet
    {
        public GetStatUpdate()
        {
            this.size = 14;
            this.offset = 4;
        }

        public ushort GetStr()
        {
            return this.GetUShort(4);
        }

        public ushort GetDex()
        {
            return this.GetUShort(6);
        }

        public ushort GetIntel()
        {
            return this.GetUShort(8);
        }

        public ushort GetCon()
        {
            return this.GetUShort(10);
        }

        public byte PointsLeft()
        {
            return this.GetByte(12);
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetStatUpdate();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnStatUpdate(this);
        }



    }
}
