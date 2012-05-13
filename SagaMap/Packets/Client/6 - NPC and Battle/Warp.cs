using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class Warp : Packet
    {
        public Warp()
        {
            this.size = 6;
            this.offset = 4;
        }

        public uint GetWarpLocation()
        {
            return this.GetUShort(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.Warp();
        }

        public override void Parse(SagaLib.Client client)
        {
            
            MapClient cli = (MapClient)client;
            cli.OnWarp(this);
        }
    }
}
