using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Sends health and skill point values of a selected actor to be displayed in a box. (Needed to end battle.)
    /// </summary>
    public class Dive : Packet
    {
        //There are more
        public enum Direction { DOWN, UP }
        /// <summary>
        /// Constructor.
        /// </summary> 
        public Dive()
        {
            this.data = new byte[9];
            this.offset = 4;
            this.ID = 0x0320;
        }

        public void SetDirection(Direction dir)
        {
            this.PutByte((byte)dir, 4);
        }

        public void SetOxygen(uint Oxygen)
        {
            this.PutUInt(Oxygen, 5);
        }        
    }
}