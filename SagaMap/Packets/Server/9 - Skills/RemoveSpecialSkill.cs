using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class RemoveSpecialSkill : Packet
    {
        public RemoveSpecialSkill()
        {
            this.data = new byte[9];
            this.ID = 0x0919;
            this.offset = 4;
        }
                
        public void SetSkill(uint id)
        {
            this.PutUInt(id, 4);
        }


    }
}
