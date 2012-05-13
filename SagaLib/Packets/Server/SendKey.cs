using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib.Packets.Server
{
    /// <summary>
    /// Send the key that will be used by the server to the client.
    /// </summary>
    public class SendKey : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendKey()
        {
            this.data = new byte[554];
            this.offset = 4;
            this.ID = 0x0201;
        }

        /// <summary>
        /// Set the key that will be send to the client.
        /// </summary>
        /// <param name="key">Key to send.</param>
        public void SetKey(byte[] key)
        {
            this.PutBytes(key, 260);
        }

        /// <summary>
        /// Set the number of collumns to use for the algorithm.
        /// </summary>
        public void SetCollumns(byte nColumns) {

            this.PutByte(nColumns, 516);

        }

        /// <summary>
        /// Set the number of rounds to use for the algorithm.
        /// </summary>
        public void SetRounds(byte nRounds) {

            this.PutByte(nRounds, 520);

        }

        /// <summary>
        /// Set the direction to use for the algorithm.
        /// </summary>
        public void SetDirection(byte direction) {

            this.PutByte(direction, 524);

        }


    }
}
