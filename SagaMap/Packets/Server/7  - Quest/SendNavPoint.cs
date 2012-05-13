using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendNavPoint : Packet
    {
        public SendNavPoint()
        {
            this.data = new byte[25];
            this.ID = 0x070B;
            this.offset = 4;
            this.data[4] = 1;//ammount of nav points, not implement yet
        }

        public void SetQuestID(uint ID)
        {
            this.PutUInt(ID, 5);
        }

        public void SetNPCType(uint type)
        {
            this.PutUInt(type, 9);
             
        }

        public void SetPosition(List<Quest.QuestsManager.WayPointInfo> list)
        {
            this.PutByte((byte)list.Count, 4);
            byte[] tmp = new byte[9 + list.Count * 16];
            this.data.CopyTo(tmp, 0);
            this.data = tmp;
            int j = 0;
            foreach (Quest.QuestsManager.WayPointInfo i in list)
            {
                this.PutUInt(i.npcType, (ushort)(9 + 16 * j));
                this.PutFloat(i.x / 1000, (ushort)(13 + 16 * j));
                this.PutFloat(i.y / 1000, (ushort)(17 + 16 * j));
                this.PutFloat(i.z / 1000, (ushort)(21 + 16 * j));
                j++;
            }
        }

        public void SetPosition(float x, float y, float z)
        {
            this.PutFloat(x / 1000, 13);
            this.PutFloat(y / 1000, 17);
            this.PutFloat(z / 1000, 21);

        }
    }
}
