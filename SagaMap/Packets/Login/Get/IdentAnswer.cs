using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login.Get
{
    /// <summary>
    /// Possible identification errors.
    /// </summary>
    public enum IdentError
    {
        NO_ERROR, ERROR, MAP_ALREADY_HOSTED
    }

    /// <summary>
    /// Server's answer to a identification.
    /// </summary>
    public class IdentAnswer : Packet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public IdentAnswer()
        {
            this.size = 5;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Login.Get.IdentAnswer();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)(client)).OnIdentAnswer(this);
        }


        public IdentError GetError()
        {
            return (IdentError)this.GetByte(4);

        }


    }
}
