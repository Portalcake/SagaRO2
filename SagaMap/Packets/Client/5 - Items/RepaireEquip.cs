using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public struct RepaireInfo
    {
        public byte Container;
        public byte Slot;
    }

    public class RepaireEquip : Packet
    {
        public RepaireEquip()
        {
            this.size = 25;
            this.offset = 4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 only observed</returns>
        public byte GetAmount()
        {
           return this.GetByte(4);
        }

        public RepaireInfo[] GetEquipts(byte amount)
        {
            RepaireInfo[] tmp = new RepaireInfo[amount];
            for (int i = 0; i < amount; i++)
            {
                tmp[i].Container = this.GetByte((ushort)(5 + (i * 2)));
                tmp[i].Slot = this.GetByte((ushort)(6 + (i * 2)));
            }
            return tmp;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.RepaireEquip();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRepaireEquip(this);
        }

    }
}
