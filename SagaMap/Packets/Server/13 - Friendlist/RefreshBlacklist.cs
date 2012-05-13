using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Packet indicating the client that he can start loading the given map.
    /// </summary>
    public class RefreshBlacklist : Packet
    {
        public RefreshBlacklist()
        {
            this.data = new byte[376];
            this.ID = 0x1306;
            this.offset = 4;
        }

        public void Add(string name, byte reason)
        {
            int index = 5 + (this.data[4] * 37);
            UnicodeEncoding.Unicode.GetBytes(name, 0, Math.Min(name.Length, 16), this.data, index);
            this.data[index + 36] = reason;
            this.data[4]++;
        }
    }
}

