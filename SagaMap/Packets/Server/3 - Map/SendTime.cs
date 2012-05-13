using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    /// <summary>
    /// Set the background music and the game time.
    /// </summary>
    public class SendTime : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendTime()
        {
            this.data = new byte[8];
            this.offset = 4;
            this.ID = 0x0313;
        }

        public void SetTime(byte day, byte hour, byte min)
        {
            this.PutByte(day, 4);
            this.PutByte(hour, 5);
            this.PutByte(min, 6);
        }

        public void SetWeather(Global.WEATHER_TYPE weather)
        {
            this.PutByte((byte)weather, 7);
        }

        public void SetWeather(byte weather)
        {
            this.PutByte(weather, 7);
        }
    }
}
