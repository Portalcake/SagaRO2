using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Remove a Actor
    /// </summary>
    public class ActorDelete : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary> 
        public ActorDelete()
        {
            this.data = new byte[8];
            this.offset = 4;
            this.ID = 0x0304;
        }

        public void SetActorID(uint pID) 
        {
            this.PutUInt(pID,4);
        }


    }
}
