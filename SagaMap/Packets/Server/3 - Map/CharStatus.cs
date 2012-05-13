using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
/*
                0x1D, 0x00,
                0x03, 0x09, 
                0xCA, 0x08, 0x00, 0x00, // CExp
                0x01, // Job
                0xCA, 0x08, 0x00, 0x00, //JExp
                0xF2, 0x00, // Hp
                0x38, 0x01, // MaxHp
                0x64, 0x00, // Sp
                0x67, 0x00, // MaxSp
                0x00, // Lc
                0x1E, // MaxLc
                0x00, // Lp
                0x05, // MaxLp
                0x00, 0x00, 0x00, 0x00
            */


namespace SagaMap.Packets.Server
{

    public class CharStatus : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CharStatus()
        {
            this.data = new byte[31];
            this.ID = 0x0308;
            this.offset = 4;            
        }

        public void SetJob(JobType job)
        {
            this.PutByte((byte)job, 8);
        }

        public void SetExp(uint cExp, uint jExp)
        {
            this.PutUInt(cExp, 4);
            this.PutUInt(jExp,9);
        }

        public void SetHPSP(ushort HP, ushort maxHP, ushort SP, ushort maxSP)
        {
            this.PutUShort(HP, 13);
            this.PutUShort(maxHP, 15);
            this.PutUShort(SP, 17);
            this.PutUShort(maxSP, 19);
        }

        public void SetLCLP(byte LC, byte maxLC, byte LP, byte maxLP)
        {
            this.PutByte(LC, 21);
            this.PutByte(maxLC, 22);
            this.PutByte(LP, 23);
            this.PutByte(maxLP, 24);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">4 for JEXP,32 for CEXP, 36 for both</param>
        public void SetVisibleField(ushort u)
        {
            this.PutUShort(u, 29);
        }

    }
}

