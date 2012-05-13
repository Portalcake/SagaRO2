using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public class MarketOwnItemResult : Packet
    {
        public MarketOwnItemResult()
        {
            this.data = new byte[6];
            this.ID = 0x1103;
            this.offset = 4;
            this.SetUnknown(0);            
        }

        public void SetUnknown(byte u)
        {
            this.PutByte(u, 4);
        }

        public void SetItems(List<MarketplaceItem> items)
        {
            byte[] tmp;
            this.PutByte((byte)items.Count, 5);
            tmp = new byte[6 + items.Count * 75];
            this.data.CopyTo(tmp, 0);
            this.data = tmp;
            for (int i = 0; i < items.Count; i++)
            {
                MarketplaceItem item = items[i];
                this.PutInt(item.item.id, (ushort)(6 + i * 75));
                this.PutString(Global.SetStringLength(item.item.creatorName, 21), (ushort)(10 + i * 75));
                this.PutUShort((ushort)item.item.req_clvl, (ushort)(55 + i * 75));
                this.PutUShort((ushort)item.item.durability, (ushort)(57 + i * 75));
                this.PutByte(item.item.stack, (ushort)(59 + i * 75));//unknown
                //unknown 12 bytes
                this.PutUInt(item.price, (ushort)(72 + i * 75));
                TimeSpan span = item.expire - DateTime.Now;
                if (span.TotalHours > 0)
                    this.PutByte((byte)span.TotalHours, (ushort)(76 + i * 75));
                else
                    this.PutByte(0, (ushort)(76 + i * 75));                
                this.PutUInt(item.id, (ushort)(77 + i * 75));
                
            }
        }
        
    }
}
