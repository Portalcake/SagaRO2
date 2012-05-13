using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SendMapPing : Packet
    {
        public SendMapPing()
        {
            this.size = 4;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SendMapPing();
        }

        public override void Parse(SagaLib.Client client)
        {
            try
            {
                foreach (CharServer i in LoginServer.charServerList.Values)
                {
                    MapServer map = i.mapServers[0];
                    Packets.Map.Send.MapPing p = new SagaLogin.Packets.Map.Send.MapPing();
                    map.sClient.netIO.SendPacket(p, map.sClient.SessionID);
                    LoginClient.CPGateway = (LoginClient)client;
                    return;
                }
            }
            catch(Exception)
            {
                Packets.Server.MapPong p = new SagaLogin.Packets.Server.MapPong();
                p.SetResult(0);
                client.netIO.SendPacket(p, 0xFFFFFFFF);
            }
        }


    }
}