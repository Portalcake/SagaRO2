using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Packet indicating the client that he can start loading the given map.
    /// </summary>
    public class SendStart : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendStart()
        {
            this.data = new byte[22];
            this.offset = 4;
            //cb2
            //this.ID = 0x0302;
            //cb3
            this.ID = 0x0301;
            this.SetUnknown(0x0D);
            this.SetChannel(1);//temporary info
        }

        public void SetUnknown(uint u)
        {
            this.PutUInt(u, 4);
        }

        // byte 4,5 are UNKNOWN!

        public void SetMapID(byte mapID)
        {
            this.PutByte(mapID, 8);
        }

        public void SetChannel(byte channel)
        {
            this.PutByte(channel, 9);
        }

        public void SetLocation(float locX, float locY, float locZ)
        {
            this.PutFloat(locX, 10);
            this.PutFloat(locY, 14);
            this.PutFloat(locZ, 18);
        }


    }
}

