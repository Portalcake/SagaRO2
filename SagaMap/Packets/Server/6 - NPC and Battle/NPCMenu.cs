using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class NPCMenu : Packet
    {
        public NPCMenu()
        {
            this.data = new byte[6];
            this.ID = 0x0603;
            this.offset = 4;
        }

        public void SetButtonID(byte id)
        {
            this.PutByte(id, 4);
        }

        public void SetMenuID(byte id)
        {
            this.PutByte(id, 5);
        }
    }
}
