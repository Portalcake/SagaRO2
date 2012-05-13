using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class NPCChat : Packet
    {
        public NPCChat()
        {
            this.data = new byte[14];
            this.ID = 0x0602;
            this.offset = 4;
            this.data[4] = 0; // ?
        }

        public void SetU(byte u)
        {
            this.PutByte(u, 4);
        }

        // Npc_Script.xml : 1 - 2221
        public void SetScript(uint script) 
        {
            this.PutUInt(script, 5);
        }

        public void SetActor(uint actor)
        {
            this.PutUInt(actor, 9);
        }

        //Icon ids from NpcChat_IconTable.xml 
        public void SetIcons(byte ncons, byte[] icons)
        {
            this.PutByte(ncons, 13);

            byte[] temp = new byte[15 + ncons];
            this.data.CopyTo(temp, 0);
            this.data = temp;
            
            for (int i = 0; i < ncons; i++)
            {
                int count = i + 15;
                ushort index = (ushort)count;

                this.PutByte(icons[i], index);
            }
        }

        public void SetUnknown(byte unk) // 1 or 2
        {
            this.PutByte(unk, 14);
        }
    }
}
