using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Packet indicating the client that he can start loading the given map.
    /// </summary>
    public class BattleStats : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BattleStats()
        {
            this.data = new byte[44];
            this.offset = 4;
            this.ID = 0x0310;
        }
        public void SetHolyResist(ushort hr)
        {
            this.PutUShort(hr, 4);
        }
        
        public void SetDarkResist(ushort dr)
        {
            this.PutUShort(dr, 6);
        }

        public void SetFireResist(ushort fr)
        {
            this.PutUShort(fr, 8);
        }

        public void SetIceResist(ushort ir)
        {
            this.PutUShort(ir, 10);
        }

        public void SetWindResist(ushort wr)
        {
            this.PutUShort(wr, 12);
        }

        public void SetCurseResist(ushort cr)
        {
            this.PutUShort(cr, 14);
        }

        public void SetSpiritResist(ushort sr)
        {
            this.PutUShort(sr, 16);
        }

        public void SetGhostResist(ushort gr)
        {
            this.PutUShort(gr, 18);
        }

        public void SetMagicalFlee(ushort mf)
        {
            this.PutUShort(mf, 24);
        }
        public void SetPhysicalFlee(ushort pf)
        {
            this.PutUShort(pf, 20);
        }
        public void SetRangedFlee(ushort rf)
        {
            this.PutUShort(rf, 22);
        }
        public void SetDef(ushort def)
        {
            this.PutUShort(def, 26);
        }
        public void SetPhysicalAtk(ushort patk)
        {
            this.PutUShort(patk, 32);
        }
        public void SetPhysicalRangeAtk(ushort rpatk)
        {
            this.PutUShort(rpatk, 34);
        }
        public void SetMagicAtk(ushort matk)
        {
            this.PutUShort(matk, 36);
        }
        public void SetPhysicalBlue(ushort pb)
        {
            this.PutUShort(pb, 38);
        }
        public void SetRangeBlue(ushort rb)
        {
            this.PutUShort(rb, 40);
        }
        public void SetMagicBlue(ushort mb)
        {
            this.PutUShort(mb, 42);
        }        
        
    }
}

