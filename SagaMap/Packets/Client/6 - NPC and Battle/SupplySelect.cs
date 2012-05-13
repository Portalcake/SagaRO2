using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class SupplySelect : Packet
    {
        public SupplySelect()
        {
            this.size = 12;
            this.offset = 4;
        }

        public uint GetActorID()
        {
            return this.GetUInt(4);
        }

        public uint GetSupplyID()
        {
            return this.GetUInt(8);
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SupplySelect();
        }

        public override void Parse(SagaLib.Client client)
        {
            
            MapClient cli = (MapClient)client;
            cli.OnSupplySelect(this);
        }
    }
}
