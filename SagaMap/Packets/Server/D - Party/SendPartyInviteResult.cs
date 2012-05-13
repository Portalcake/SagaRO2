using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SendPartyInviteResult : Packet
    {
        public enum Result { OK, DENIED, NOT_EXIST, ALREADY_IN_PARTY, OVER_6_LEVEL_DIFFERENCE, MAX_MEMBER }

        public SendPartyInviteResult()
        {
            this.data = new byte[5];
            this.ID = 0x0D02;
        }

        public void SetResult(Result result)
        {            
            this.PutByte((byte)result, 4);
        }
    }
}
