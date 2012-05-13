using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Client
{
    public class SendLoginPing : Packet
    {
        public SendLoginPing()
        {
            this.size = 4;
            this.offset = 4;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.SendLoginPing();
        }

        public override void Parse(SagaLib.Client client)
        {
            LoginClient client_ = (LoginClient)client;
            Packets.Server.LoginPong p = new SagaLogin.Packets.Server.LoginPong();
            client.netIO.SendPacket(p, client_.SessionID);
        }


    }
}