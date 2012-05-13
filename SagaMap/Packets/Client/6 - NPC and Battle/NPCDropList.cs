using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    
    public class NPCDropList : Packet
    {
        public NPCDropList()
        {
            this.size = 8;
            this.offset = 4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 ActorID</returns>
        public uint GetActorID()
        {
           return this.GetUInt(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.NPCDropList();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCDropList(this);
        }

    }
}
