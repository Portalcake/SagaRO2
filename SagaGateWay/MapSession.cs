using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

using SagaLib;

namespace SagaGateway
{
    public class MapSession : Client
    {
        public enum SESSION_STATE
        {
            MAP,DISCONNECTED
        }
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED.
        /// </summary>
        public SESSION_STATE state;
        public GatewayClient Client;

        private Socket sock;
        private string host;
        private int port;
        Dictionary<ushort, Packet> commandTable;
        List<GatewayClient> waitingQueue;

        public MapSession(string host, int port,uint SessionID)
        {
            this.commandTable = new Dictionary<ushort, Packet>();

            this.commandTable.Add(0x0201, new Packets.Map.Get.SendKey());
            this.commandTable.Add(0x0202, new Packets.Map.Get.SendGUID());
            //this.commandTable.Add(0x010B, new Packets.Login.Get.SendIdentify());
            
            this.commandTable.Add(0xFD02, new Packets.Map.Get.ClientKick());
            this.commandTable.Add(0xFFFF, new Packets.Map.Get.SendUniversal());
            
            this.SessionID = SessionID;
            
            this.waitingQueue =new List<GatewayClient>();

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
                    Logger.ShowError("Cannot connect to the Mapserver,please check the configuration!", null);
                    Gateway.ShutingDown(null, null);
                    //Gateway.Init();
                    return;
                }
                try
                {
                    //Logger.ShowInfo("Trying to connect to Mapserver " + host + ":" + port, null);
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

            Logger.ShowInfo("Successfully connected to Map server", null);
            this.state = SESSION_STATE.MAP;
            this.netIO = new NetIO(sock, this.commandTable, this, GatewayClientManager.Instance);
        }


        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {
            this.state = SESSION_STATE.DISCONNECTED;
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect();
            if (this.state == SESSION_STATE.DISCONNECTED)
            {
                //TODO:
                //Disconnect all clients
            }
        }

        public void Logout(GatewayClient client)
        {
            Packets.Login.Send.Logout p = new SagaGateway.Packets.Login.Send.Logout();
            p.SetSessionID(client.SessionID);
            this.netIO.SendPacket(p, client.SessionID);
        }

        public void OnKick(Packets.Map.Get.ClientKick p)
        {
            uint id = p.SessionID;
            GatewayClient client = GatewayClientManager.Instance.clients[id];
            client.onKickMap = true;
            client.netIO.Disconnect();
        }

        public void OnSendKey(SagaGateway.Packets.Map.Get.SendKey p)
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

        public void SendToMap(byte[] data, uint SessionID)
        {
            Packet send = new Packet(data);
            send.data = data;
            this.netIO.SendPacket(send, SessionID);
        }

        
        public void OnSendUniversal(Packets.Map.Get.SendUniversal p)
        {
            uint id = p.SessionID;
            if (!GatewayClientManager.Instance.clients.ContainsKey(id)) return;
            GatewayClient client = GatewayClientManager.Instance.clients[id];
            client.SendToClient(p.GetData(), 0x0601);
        }

        public void OnSendGUID(Packets.Map.Get.SendGUID p)
        {
            this.SetHeader(true);
            //Logger.ShowInfo("Got Send GUID packet from map server",null);
        }

        public void NewSession(GatewayClient client, uint charID, uint Validation)
        {
            Packets.Map.Send.SendIdentify send = new SagaGateway.Packets.Map.Send.SendIdentify();
            send.SetCharID(charID);
            send.SetValidationKey(Validation);
            send.SetSessionID(client.SessionID);
            this.netIO.SendPacket(send, client.SessionID);
            this.netIO.fullHeader = true;
            client.ConnectedtoMap();            
        }

        public void OnSendIdentify(Packets.Map.Get.SendIdentify p)
        {
            //Logger.ShowInfo("Got Send Identify packet from map server", null);
            
        }
       
    }
}
