using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login.Get
{
    /// <summary>
    /// Client packet that contains the key user by the client.
    /// </summary>
    public class MapPing: Packet
    {
        /// <summary>
        /// Create an empty send key packet
        /// </summary>
        public MapPing()
        {
            this.size = 4;
            this.offset = 4;
        }

        

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Login.Get.MapPing();
        }

        public override void Parse(SagaLib.Client client)
        {
            try
            {
                LoginSession client_ = (LoginSession)client;
                Packets.Login.Send.MapPong p = new SagaMap.Packets.Login.Send.MapPong();
                client.netIO.SendPacket(p, client_.SessionID);
            }
            catch
            {
                MapClient client_ = (MapClient)client;
                Packets.Login.Send.MapPong p = new SagaMap.Packets.Login.Send.MapPong();
                client.netIO.SendPacket(p, client_.SessionID);
            }
        }

    }
}
