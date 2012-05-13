using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SetSpecialityConfirm : Packet
    {
        public SetSpecialityConfirm()
        {
            this.data = new byte[5];
            this.ID = 0x091D;
            this.offset = 4;
        }
       

    }
}
