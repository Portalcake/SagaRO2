using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;

namespace SagaMap.Packets.Server
{

    public class CharExtStatus : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CharExtStatus()
        {
            this.data = new byte[45];
            this.ID = 0x030F;
            this.offset = 4;

        }

        public void SetStatsBase1(ushort str, ushort dex, ushort intel, ushort con, ushort luk)
        {
            this.PutUShort(str, 4);
            this.PutUShort(dex, 6);
            this.PutUShort(intel, 8);
            this.PutUShort(con, 10);
            this.PutUShort(luk, 12);
        }

        public void SetStatsBase2(ushort str, ushort dex, ushort intel, ushort con, ushort luk)
        {
            this.PutUShort(str, 14);
            this.PutUShort(dex, 16);
            this.PutUShort(intel, 18);
            this.PutUShort(con, 20);
            this.PutUShort(luk, 22);
        }

        public void SetStatsJob(ushort str, ushort dex, ushort intel, ushort con, ushort luk)
        {
            this.PutUShort(str, 24);
            this.PutUShort(dex, 26);
            this.PutUShort(intel, 28);
            this.PutUShort(con, 30);
            this.PutUShort(luk, 32);
        }

        public void SetStatsBonus(ushort str, ushort dex, ushort intel, ushort con, ushort luk)
        {
            this.PutUShort(str, 34);
            this.PutUShort(dex, 36);
            this.PutUShort(intel, 38);
            this.PutUShort(con, 40);
            this.PutUShort(luk, 42);
        }

        public void SetStatPoints(byte points)
        {
            this.PutByte(points, 44);
        }

    }
}