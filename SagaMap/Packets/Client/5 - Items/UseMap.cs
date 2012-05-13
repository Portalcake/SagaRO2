using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class UseMap : Packet
    {
        public UseMap()
        {
            this.size = 5;
            this.offset = 4;

            //data unknown
        }

        public byte GetIndex()
        {
            return this.GetByte(4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 only observed</returns>
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.UseMap();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnUseMap(this);
        }

    }
}
