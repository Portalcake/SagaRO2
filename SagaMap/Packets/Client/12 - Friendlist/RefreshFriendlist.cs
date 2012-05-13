using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class RefreshFriendlist : Packet
    {
        public RefreshFriendlist()
        {
            //0x1203
            this.size = 4;
            this.offset = 4;
        }
    }
}
