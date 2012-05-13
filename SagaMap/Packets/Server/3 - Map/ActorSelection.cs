using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Sends health and skill point values of a selected actor to be displayed in a box. (Needed to end battle.)
    /// </summary>
    public class ActorSelection : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary> 
        public ActorSelection()
        {
            this.data = new byte[20];
            this.offset = 4;
            this.ID = 0x030B;
        }

        public void SetSourceActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetHP(ushort hp)
        {
            this.PutUShort(hp, 8);
        }

        public void SetMaxHP(ushort hp)
        {
            this.PutUShort(hp, 10);
        }

        public void SetSP(ushort sp)
        {
            this.PutUShort(sp, 12);
        }

        public void SetMaxSP(ushort hp)
        {
            this.PutUShort(hp, 14);
        }

        public void SetTargetActorID(uint pID)
        {
            this.PutUInt(pID, 16);
        }
    }
}