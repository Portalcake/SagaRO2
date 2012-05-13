using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{

    public class RefreshFriendlist : Packet
    {

        public RefreshFriendlist()
        {
            this.data = new byte[805];
            this.offset = 4;
            this.ID = 0x1303;            
        }

        public void Add(string name, byte job, byte clvl, byte jlvl, byte map)
        {
            int index = 5 + (this.data[4] * 40);
            UnicodeEncoding.Unicode.GetBytes(name, 0, Math.Min(name.Length, 16), this.data, index);
            this.data[index + 36] = job;
            this.data[index + 37] = clvl;
            this.data[index + 38] = jlvl;
            this.data[index + 39] = map;
            this.data[4]++;
        }
        
    }
}

