using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendSpecialSkills : Packet
    {
        public SendSpecialSkills()
        {
            this.data = new byte[5];
            this.ID = 0x0917;
            this.offset = 4;
        }

        public void SetSkills(List<uint> skills)
        {
            byte[] tmp = new byte[5 + skills.Count * 4];
            this.data.CopyTo(tmp, 0);
            this.data = tmp;
            this.PutByte((byte)skills.Count, 4);
            for (int i = 0; i < skills.Count; i++)
            {
                this.PutUInt(skills[i], (ushort)(5 + i * 4));
            }
        }
    }
}
