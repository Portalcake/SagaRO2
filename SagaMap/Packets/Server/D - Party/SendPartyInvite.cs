using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendPartyInvite : Packet
    {
        public SendPartyInvite()
        {
            this.data = new byte[38];
            this.ID = 0x0D01;
        }

        public void SetName(string name)
        {
            Global.SetStringLength(name, 16);
            this.PutString(name, 4);
        }
    }
}
