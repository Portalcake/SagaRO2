using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

using SagaLib;

namespace SagaGateway
{
    public class ControlPanelLoginSession : Client
    {
        public enum SESSION_STATE
        {
            CONNECTING,LOGIN,DISCONNECTED,NOTVALID
        }
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED.
        /// </summary>
        public SESSION_STATE state = SESSION_STATE.CONNECTING;
        public ControlPanelClient Client;

        Dictionary<ushort, Packet> commandTable;

        public ControlPanelLoginSession(string host, int port, ControlPanelClient client)
        {
            this.commandTable = new Dictionary<ushort, Packet>();

            
            this.Client = client;
            this.commandTable.Add(0xFE01, new Packets.Login.Get.SendLoginPong());
            this.commandTable.Add(0xFE02, new Packets.Client.CP.MapPong());
            
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect(newSock, host, port);
        }

        public void Connect(Socket sock, string host, int port)
        {
            try
                {
                    Logger.ShowInfo("Trying to connect to loginserver " + host + ":" + port, null);
                    sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(host), port));
                }
                catch (Exception)
                {
                    Logger.ShowWarning("Cannot connect to loginserver!", null);
                    return;
                }             

            Logger.ShowInfo("Successfully connected to login server", null);
            this.state = SESSION_STATE.LOGIN;
            try
            {
                this.netIO = new NetIO(sock, this.commandTable, this, ControlPanelClientManager.Instance);
            }
            catch (Exception ex)
            {
                Logger.ShowWarning(ex.StackTrace, null);
            }
        }


        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {
            this.state = SESSION_STATE.DISCONNECTED;
            if (this.Client.state == ControlPanelClient.SESSION_STATE.LOGIN || this.Client.state == ControlPanelClient.SESSION_STATE.MAP)
            {
                this.Client.netIO.Disconnect();
            }
        }

        public void LoginPing()
        {
            Packets.Login.Send.LoginPing p = new SagaGateway.Packets.Login.Send.LoginPing();
            this.netIO.SendPacket(p, 0);
        }

        public void MapPing()
        {
            Packets.Login.Send.MapPing p = new SagaGateway.Packets.Login.Send.MapPing();
            this.netIO.SendPacket(p, 0);
        }        
        
        public void OnMapPong(Packets.Client.CP.MapPong p)
        {
            this.Client.OnMapPong(p.GetResult());
        }

        public void OnLoginPong()
        {
            this.Client.OnLoginPong();
        }

        public void OnClientSendKey()
        {
            byte[] tempServerKey = Encryption.GenerateKey();
            byte[] expandedServerKey = Encryption.GenerateDecExpKey(tempServerKey);

            SagaLib.Packets.Server.SendKey sendPacket = new SagaLib.Packets.Server.SendKey();
            sendPacket.SetKey(expandedServerKey);
            sendPacket.SetCollumns(4);
            sendPacket.SetRounds(10);
            sendPacket.SetDirection(2);
            this.netIO.SendPacket(sendPacket, 0);

            this.netIO.ServerKey = tempServerKey;
            
        }

        public void OnClientSendGUID()
        {
            SagaLib.Packet send = new Packet(24);
            send.ID = 0x0202;
            this.netIO.SendPacket(send, 0);
        }

        public void OnSendKey(SagaGateway.Packets.Login.Get.SendKey p)
        {
            this.netIO.ClientKey = p.GetKey();
            this.Client.OnLoginSendKey();
        }

        public void SetHeader(bool value)
        {
            this.netIO.fullHeader = value;
        }

        public void SendToLogin(byte[] data)
        {
            Packet send = new Packet(data);
            send.data = data;
            this.netIO.SendPacket(send, 0);
        }

       

        public void OnSendGUID(Packets.Login.Get.SendGUID p)
        {
            Logger.ShowInfo("Got Send GUID packet from login server",null);
        }

        public void OnSendIdentify(Packets.Login.Get.SendIdentify p)
        {
            Logger.ShowInfo("Got Send Identify packet from login server", null);
            this.Client.OnLoginSendIdentify();
        }
       
    }
}
