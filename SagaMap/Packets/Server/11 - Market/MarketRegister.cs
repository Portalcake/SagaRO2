using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class MarketRegister : Packet
    {
        public MarketRegister()
        {
            this.data = new byte[80]; 
            this.ID = 0x1104;
            this.offset = 4;

        }

        public void SetItemID(uint itemId)
        {
            this.PutUInt(itemId, 5);
        }

        public void SetAuctionID(uint auctionId)
        {
            this.PutUInt(auctionId, 76);
        }

        public void SetZeny(uint zeny)
        {
            this.PutUInt(zeny, 71);
        }

        public void SetReqClvl(byte clvl)
        {
            this.PutByte(clvl, 54);
        }

        public void SetCount(byte count)
        {
            this.PutByte(count, 58);
        }
    }
}
