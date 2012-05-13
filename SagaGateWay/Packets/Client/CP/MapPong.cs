using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaGateway.Packets.Client.CP
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class MapPong : Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public MapPong()
        {
            this.size = 5;
            this.offset = 4;
        }

        public byte GetResult()
        {
            return this.GetByte(4);                    
        }
    
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaGateway.Packets.Client.CP.MapPong();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ControlPanelLoginSession)(client)).OnMapPong(this);
        }

    }
}
