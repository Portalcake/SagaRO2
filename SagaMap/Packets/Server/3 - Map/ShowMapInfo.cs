using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Sends health and skill point values of a selected actor to be displayed in a box. (Needed to end battle.)
    /// </summary>
    public class ShowMapInfo : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary> 
        public ShowMapInfo()
        {
            this.data = new byte[260];
            this.offset = 4;
            this.ID = 0x0317;
        }

        public void SetMapInfo(Dictionary<byte, byte> info)
        {
            foreach (byte i in info.Keys)
            {
                this.PutByte(info[i], (ushort)(4 + i));
            }
        }
    }
}