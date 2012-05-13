using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Server
{
    public class SendKey : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendKey()
        {
            this.data = new byte[560];
            this.isGateway = true;
            this.offset = 10;
            this.ID = 0x0201;
        }

        /// <summary>
        /// Set the key that will be send to the client.
        /// </summary>
        /// <param name="key">Key to send.</param>
        public void SetKey(byte[] key)
        {
            this.PutBytes(key, 266);
        }

        /// <summary>
        /// Set the number of collumns to use for the algorithm.
        /// </summary>
        public void SetCollumns(byte nColumns)
        {

            this.PutByte(nColumns, 522);

        }

        /// <summary>
        /// Set the number of rounds to use for the algorithm.
        /// </summary>
        public void SetRounds(byte nRounds)
        {

            this.PutByte(nRounds, 526);

        }

        /// <summary>
        /// Set the direction to use for the algorithm.
        /// </summary>
        public void SetDirection(byte direction)
        {

            this.PutByte(direction, 530);

        }


    }
}
