using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SkillEffect : Packet
    {
        public SkillEffect()
        {
            this.data = new byte[19];
            this.ID = 0x091F;
            this.offset = 4;
        }

        public void SetActorID(uint actor)
        {
            this.PutUInt(actor, 4);
        }

        public void SetU1(byte u1)
        {
            this.PutByte(u1, 8);
        }       

        public void SetU2(uint u2)
        {
            this.PutUInt(u2, 9);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func">1 for HP, 2 for SP, 3 for LP</param>
        public void SetFunction(byte func)
        {
            this.PutByte(func, 13);
        }

        public void SetAmount(uint amount)
        {
            this.PutUInt(amount, 14);
        }

        public void SetU3(byte u3)
        {
            this.PutByte(u3, 18);
        }
    }
}
