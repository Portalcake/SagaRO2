using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendWarpList : Packet
    {
        public SendWarpList()
        {
            this.data = new byte[9];
            this.ID = 0x0620;
            this.offset = 4;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void AddItem(ushort id, uint price)
        {
            int position = this.data.Length;
            Array.Resize<byte>(ref this.data, this.data.Length + 337);
            this.PutUShort( id, (ushort) position );
            this.PutUInt( price, (ushort)( position + 2) );
            this.data[8]++;
        }
    }
}
