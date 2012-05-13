using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Sends health and skill point values of a selected actor to be displayed in a box. (Needed to end battle.)
    /// </summary>
    public class TakeDamage : Packet
    {
        //There are more
        public enum REASON { FALLING = 1, OXYGEN, FALLING_SURVIVE, FALLING_DEAD, OXYGEN_DEAD }
        /// <summary>
        /// Constructor.
        /// </summary> 
        public TakeDamage()
        {
            this.data = new byte[9];
            this.offset = 4;
            this.ID = 0x031F;
        }

        public void SetReason(REASON reason)
        {
            this.PutByte((byte)reason, 4);
        }

        public void SetDamage(uint damage)
        {
            this.PutUInt(damage, 5);
        }        
    }
}