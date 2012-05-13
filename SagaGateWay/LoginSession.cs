using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

using SagaLib;

namespace SagaGateway
{
    public class LoginSession : Client
    {
        public enum SESSION_STATE
        {
            LOGIN,DISCONNECTED
        }
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED.
        /// </summary>
        public SESSION_STATE state;
        //public GatewayClient Client;
        private Socket sock;
        private string host;
        private int port;
        
        Dictionary<ushort, Packet> commandTable;

        List<GatewayClient> waitingQueue;

        public LoginSession(string host, int port)
        {
            this.commandTable = new Dictionary<ushort, Packet>();

            this.commandTable.Add(0x0201, new Packets.Login.Get.SendKey());
            this.commandTable.Add(0x0202, new Packets.Login.Get.SendGUID());
            this.commandTable.Add(0x0106, new Packets.Login.Get.SendToMap());
            this.commandTable.Add(0x010A, new Packets.Login.Get.SendIdentify());
            this.commandTable.Add(0xFD01, new Packets.Login.Get.ResponseRequest());
            this.commandTable.Add(0xFD02, new Packets.Login.Get.ClientKick());
            this.commandTable.Add(0xFFFF, new Packets.Login.Get.SendUniversal());

            this.waitingQueue = new List<GatewayClient>();
            
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sock = newSock;
            this.host = host;
            this.port = port;            
            this.Connect();
        }

        public void Connect()
        {
            bool Connected = false;
            int times = 5;
            do
            {
                if (times < 0)
                {
                    Logger.ShowError("Cannot connect to the loginserver,please check the configuration!", null);
                    Gateway.ShutingDown(null, null);
                    //Gateway.Init();
                    return;
                }
                try
                {
                    //Logger.ShowInfo("Trying to connect to loginserver " + host + ":" + port, null);
                    sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(host), port));
                    Connected = true;
                }
                catch (Exception e)
                {
                    Logger.ShowError("Failed... Trying again in 5sec", null);
                    Logger.ShowError(e.ToString(), null);
                    System.Threading.Thread.Sleep(5000);
                    Connected = false;
                }
                times--;
            } while (!Connected);

            Logger.ShowInfo("Successfully connected to login server", null);
            this.state = SESSION_STATE.LOGIN;
            try
            {
                this.netIO = new NetIO(sock, this.commandTable, this, GatewayClientManager.Instance);
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
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect();
        }

        public void RequestNewSession(GatewayClient client)
        {
            Packets.Login.Send.NewClient p = new SagaGateway.Packets.Login.Send.NewClient();
            this.waitingQueue.Add(client); 
            this.netIO.SendPacket(p, this.SessionID);            
        }

        public void Logout(GatewayClient client)
        {
            Packets.Login.Send.Logout p = new SagaGateway.Packets.Login.Send.Logout();
            p.SetSessionID(client.SessionID);
            this.netIO.SendPacket(p, client.SessionID);
        }       

        public void OnSendKey(SagaGateway.Packets.Login.Get.SendKey p)
        {
            this.netIO.ClientKey = p.GetKey();
            byte[] tempServerKey = Encryption.GenerateKey();
            byte[] expandedServerKey = Encryption.GenerateDecExpKey(tempServerKey);

            SagaLib.Packets.Server.SendKey sendPacket = new SagaLib.Packets.Server.SendKey();
            sendPacket.SetKey(expandedServerKey);
            sendPacket.SetCollumns(4);
            sendPacket.SetRounds(10);
            sendPacket.SetDirection(2);
            this.netIO.SendPacket(sendPacket, this.SessionID);

            this.netIO.ServerKey = tempServerKey;            
        }

        public void SetHeader(bool value)
        {
            this.netIO.fullHeader = value;
        }

        public void SendToLogin(byte[] data, uint SessionID)
        {
            Packet send = new Packet(data);
            send.data = data;
            this.netIO.SendPacket(send, SessionID);
        }

        public void OnKick(Packets.Login.Get.ClientKick p)
        {
            uint id = p.SessionID;
            GatewayClient client = GatewayClientManager.Instance.clients[id];            
            client.onKick = true;
            client.netIO.Disconnect();
        }

        public void OnSendUniversal(Packets.Login.Get.SendUniversal p)
        {
            uint id = p.SessionID;
            GatewayClient client = GatewayClientManager.Instance.clients[id];
            client.SendToClient(p.GetData(), 0x0401);
        }

        public void OnSendToMap(Packets.Login.Get.SendToMap p)
        {
            uint id = p.SessionID;
            GatewayClient client = GatewayClientManager.Instance.clients[id];
            client.SendToMap(p);
            //this.Client.SendToMap(p);
        }

        public void OnSendGUID(Packets.Login.Get.SendGUID p)
        {
            this.SetHeader(true);           
        }

        public void OnSendIdentify(Packets.Login.Get.SendIdentify p)
        {
            Logger.ShowInfo("Got Send Identify packet from login server");
            this.SessionID = p.GetSessionID();
            this.SetHeader(true);
            //this.Client.OnLoginSendIdentify();
        }

        public void OnResponseRequest(Packets.Login.Get.ResponseRequest p)
        {
            Logger.ShowInfo("Got New SessionID:" + p.GetSessionID().ToString());           
            if (this.waitingQueue.Count > 0)
            {
                try
                {
                    GatewayClient client = this.waitingQueue[0];
                    client.OnGotSessionID(p.GetSessionID());
                    this.waitingQueue.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
       
    }
}
