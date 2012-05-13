using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{

    public class RegisterBlacklistChar : Packet
    {

        public RegisterBlacklistChar()
        {
            this.data = new byte[41];
            this.ID = 0x1304;
            this.offset = 4;
        }

        public void SetName(string name)
        {
            PutString(name, 4);
        }

        public void SetReasonForBlacklist(byte reason)
        {
            PutByte(reason, 39);
        }

        public void SetReason(byte reason)
        {
            PutByte(reason, 40);
        }

    }
}

