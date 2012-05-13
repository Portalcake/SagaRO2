using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class ItemActive : Packet
    {
        public ItemActive()
        {
            this.data = new byte[26];
            this.ID = 0x0913;
            this.offset = 4;
            this.PutByte(6, 5);//unknown
        }

        public void SetSkillType(byte type)
        {
            this.PutByte(type, 4);
        }

        public void SetContainer(byte container)
        {
            this.PutByte(container, 6);
        }

        public void SetIndex(byte index)
        {
            this.PutByte(index, 7);
        }

        public void SetSkillID(uint id)
        {
            this.PutUInt(id, 8);
        }

        public void SetSActor(uint id)
        {
            this.PutUInt(id, 12);
        }
        public void SetDActor(uint id)
        {
            this.PutUInt(id, 16);
        }

        public void SetValue(uint value)
        {
            this.PutUInt(value, 20);
        }

    }
}
