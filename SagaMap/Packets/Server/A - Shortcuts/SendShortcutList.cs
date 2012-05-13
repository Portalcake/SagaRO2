using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actors;

namespace SagaMap.Packets.Server
{
    public class SendShortcutList : Packet
    {
        public SendShortcutList()
        {
            this.data = new byte[7];
            this.ID = 0x0A03;
            this.offset = 11;
        }

        public void SetQuickPageType(byte one, byte two)
        {
            this.PutByte(one, 4);
            this.PutByte(two, 5);
        }

        public void SetEntries(Dictionary<byte,ActorPC.Shortcut> sc)
        {
            this.PutByte((byte)sc.Count, 6);
            this.offset = 11;
            byte[] temp = new byte[(sc.Count * 10) + 11];
            this.data.CopyTo(temp, 0);
            this.data = temp;
            foreach (byte i in sc.Keys)
            {
                this.PutByte(sc[i].type);
                this.PutByte(i);
                this.PutUInt(sc[i].ID);
                this.PutUInt(0);
            }
           
        }

    }
}
