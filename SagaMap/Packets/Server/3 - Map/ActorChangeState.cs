using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{

    /// <summary>
    /// Change the state of an actor
    /// </summary>
    /// 
    public class ActorChangeState : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary> 
        public ActorChangeState()
        {
            this.data = new byte[14];
            this.offset = 4;
            this.ID = 0x0309;
        }

        public void SetActorID(uint pID)
        {
            this.PutUInt(pID, 4);
        }

        public void SetBattleState(bool inBattle)
        {
            if (inBattle) this.PutByte(1, 8);
            else this.PutByte(0, 8);
        }

        public void SetStance(Global.STANCE s)
        {
            this.PutByte((byte)s, 9);
        }

        public void SetStance(byte s)
        {
            this.PutByte(s, 9);
        }

        public void SetTargetActor(uint id)
        {
            this.PutUInt(id);
        }

    }
}