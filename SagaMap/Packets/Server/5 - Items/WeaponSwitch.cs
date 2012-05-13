using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class WeaponSwitch : Packet
    {
        public WeaponSwitch()
        {
            this.data = new byte[5];
            this.ID = 0x050A;
        }

        public void SetID(byte id)
        {
            this.PutByte(id, 4);
        }
    }
}
