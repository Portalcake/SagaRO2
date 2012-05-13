using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Map.Get
{
    public class MapPong : Packet
    {
        public MapPong()
        {
            this.size = 4;
            this.offset = 4;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Map.Get.MapPong();
        }

        public override void Parse(SagaLib.Client client)
        {
            LoginClient client_ = (LoginClient)client;
            if (client_.pinging)
            {
                client_.mapServer.lastPong = DateTime.Now;
                client_.pinging = false;
                TimeSpan span = client_.mapServer.lastPong - client_.mapServer.lastPing;
                Logger.ShowInfo("Last Map ping value:" + span.TotalMilliseconds.ToString() + "ms");
            }
            else
            {
                Packets.Server.MapPong p = new SagaLogin.Packets.Server.MapPong();
                p.SetResult(1);
                LoginClient.CPGateway.netIO.SendPacket(p, LoginClient.CPGateway.SessionID);
            }
        }

        



    }
}

