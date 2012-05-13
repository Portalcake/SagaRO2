using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaLib;


namespace SagaGateway 
{
    sealed class ControlPanelClientManager : ClientManager 
    {
        private List<ControlPanelClient> clients;

        private uint count = 1;

        ControlPanelClientManager()
        {
            this.clients = new List<ControlPanelClient>();
            this.commandTable = new Dictionary<ushort, Packet>();

            //here for packets
            this.commandTable.Add(0x0101, new Packets.Client.SendKey());
            this.commandTable.Add(0x0102, new Packets.Client.SendGUID());
            this.commandTable.Add(0x0104, new Packets.Client.SendIdentify());
            this.commandTable.Add(0xFFFE, new Packets.Client.CP.CPCommand());
            this.commandTable.Add(0xFFFF, new Packets.Client.SendUniversal());

            
            this.waitressQueue = new AutoResetEvent(false);
            this.waitressHasFinished = new ManualResetEvent(false);
            this.waitingWaitressesCount = 0;
            this.waitressCountLock = new Object();

            this.packetCoordinator = new Thread(new ThreadStart(this.packetCoordinationLoop));
            this.packetCoordinator.Start();
        }

        public uint GetNextSessionID()
        {
            return count++;
        }

        public static ControlPanelClientManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly ControlPanelClientManager instance = new ControlPanelClientManager();
        }


        /// <summary>
        /// Connects new clients
        /// </summary>
        public override void NetworkLoop(int maxNewConnections)
        {
            for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
            {
                Socket sock = listener.AcceptSocket();

                Logger.ShowInfo("New client from: " + sock.RemoteEndPoint.ToString(),null);
                clients.Add(new ControlPanelClient(sock, this.commandTable));
            }
        }

        public override void OnClientDisconnect(Client client_t)
        {
            ControlPanelClient client = (ControlPanelClient)client_t;

            /*if (client.isMapServer)
            {
                Logger.ShowWarning("A map server just disconnected.",null);
                LoginServer.charServerList[client.mapServer.worldID].DeleteMapServer(client.mapServer);
            }*/
            this.clients.Remove(client);
        }

    }
}
