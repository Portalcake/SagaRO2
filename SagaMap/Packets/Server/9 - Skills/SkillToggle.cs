using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillToggle : Packet
    {
        public SkillToggle()
        {
            this.data = new byte[10];
            this.ID = 0x0909;
            this.offset = 4;
        }

        public void SetSkillType(byte type)
        {
            this.PutByte(type, 4);
        }

        public void SetSkillID(uint skillID)
        {
            this.PutUInt(skillID, 5);
        }

        public void SetToggle(bool toggle)
        {
            switch (toggle)
            {
                case true:
                    this.PutByte(12, 9);
                    break;
                case false:
                    this.PutByte(13, 9);
                    break;
            }
        }
    }
}
