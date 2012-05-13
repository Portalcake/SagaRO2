using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendQuestList : Packet
    {
        public SendQuestList()
        {
            this.data = new byte[9];
            this.ID = 0x0702;
            this.offset = 4;
        }

        public void SetActor(uint actorID)
        {
            this.PutUInt(actorID, 5);
        }

        public void SetQuestList(List<uint> IDs)
        {
            byte[] tmp = new byte[this.data.Length + (IDs.Count * 4)];
            this.data.CopyTo(tmp, 0);
            this.data = tmp;
            this.PutByte((byte)IDs.Count, 4);
            for (int i = 0; i < IDs.Count; i++)
            {
                this.PutUInt(IDs[i], (ushort)(9 + i * 4));
            }
        }
    }
}
