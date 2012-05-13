using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class NPCMenu : Packet
    {
        public NPCMenu()
        {
            this.size = 6;
            this.offset = 4;
        }

        public byte GetButtonID()
        {
            return this.GetByte(4);
        }

        public byte GetMenuID()
        {
            return this.GetByte(5);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.NPCMenu();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCMenu(this);
        }
    }
}
