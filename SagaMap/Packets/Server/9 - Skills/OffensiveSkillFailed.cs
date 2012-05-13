using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class OffensiveSkillFailed : Packet
    {
        public OffensiveSkillFailed()
        {
            this.data = new byte[13];
            this.ID = 0x0907;
            this.offset = 4;
            this.PutByte(1, 4);
        }

        public void SetSkillID(uint skillID)
        {
            this.PutUInt(skillID, 5);
        }

        public void SetActor(uint sourceID)
        {
            this.PutUInt(sourceID, 9);
        }        
    }
}
