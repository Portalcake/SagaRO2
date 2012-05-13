//Comment this out to deactivate the dead lock check!
#define DeadLockCheck

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaLib;

namespace SagaLogin
{
    sealed class LoginClientManager : ClientManager
    {
        public Dictionary<uint,LoginClient> clients;
        private uint count = 1;

        LoginClientManager()
        {
            this.clients = new Dictionary<uint, LoginClient>();
            this.commandTable = new Dictionary<ushort, Packet>();

            // general packets - all servers
            commandTable.Add(0x0201, new Packets.Client.SendKey());
            commandTable.Add(0x0202, new Packets.Client.SendGUID());
            commandTable.Add(0x0203, new Packets.Client.SendCRC());

            
            // packets from map servers
            commandTable.Add(0xFF01, new Packets.Map.Get.Identify());

            // login server packets
            //cb2
            /*commandTable.Add(0x0101, new Packets.Client.SendVersion());
            commandTable.Add(0x0102, new Packets.Client.SendLogin());
            commandTable.Add(0x0103, new Packets.Client.WantServerList());
            commandTable.Add(0x0104, new Packets.Client.SelectServer());
            commandTable.Add(0x0105, new Packets.Client.SelectChar());
            commandTable.Add(0x0106, new Packets.Client.CreateChar());
            commandTable.Add(0x0107, new Packets.Client.WantCharList());
            commandTable.Add(0x0108, new Packets.Client.GetCharData());
            commandTable.Add(0x0109, new Packets.Client.DeleteChar());
            */
            //cb3
            commandTable.Add(0x0101, new Packets.Client.SendLogin());
            commandTable.Add(0x0102, new Packets.Client.WantServerList());
            commandTable.Add(0x0103, new Packets.Client.SelectServer());
            commandTable.Add(0x0104, new Packets.Client.SelectChar());
            commandTable.Add(0x0105, new Packets.Client.CreateChar());
            commandTable.Add(0x0106, new Packets.Client.WantCharList());
            commandTable.Add(0x0107, new Packets.Client.GetCharData());
            commandTable.Add(0x0108, new Packets.Client.DeleteChar());
            
            //Gateway internal packets
            commandTable.Add(0xFD01, new Packets.Client.GwRequestNew());
            commandTable.Add(0xFD02, new Packets.Client.GwLogout());
            commandTable.Add(0xFE01, new Packets.Client.SendLoginPing());
            commandTable.Add(0xFE02, new Packets.Client.SendMapPing());
            commandTable.Add(0xFE03, new Packets.Map.Get.MapPong());


            this.waitressQueue = new AutoResetEvent(false);
            this.waitressHasFinished = new ManualResetEvent(false);
            this.waitingWaitressesCount = 0;
            this.waitressCountLock = new Object();

            this.packetCoordinator = new Thread(new ThreadStart(this.packetCoordinationLoop));
            this.packetCoordinator.Start();

            //deadlock check
            Thread check = new Thread(new ThreadStart(this.checkCriticalArea));
#if DeadLockCheck
            check.Start();
#endif
        }

        public uint GetNextSessionID()
        {
            return count++;
        }

        public static LoginClientManager Instance
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

            internal static readonly LoginClientManager instance = new LoginClientManager();
        }


        /// <summary>
        /// Connects new clients
        /// </summary>
        public override void NetworkLoop(int maxNewConnections)
        {
            try
            {
                for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
                {
                    Socket sock = listener.AcceptSocket();

                    Logger.ShowInfo("New client from: " + sock.RemoteEndPoint.ToString(), null);
                    uint sessionid = (uint)((uint)0xFFFFFFFF - clients.Count);
                    clients.Add(sessionid, new LoginClient(sock, this.commandTable, sessionid));
                }
            }
            catch (Exception)
            {
                //Logger.ShowError(ex);
            }
            
        }

        public bool CheckOnline(string ID, LoginClient my)
        {
            uint session = 0;
            List<LoginClient> list = new List<LoginClient>();
            foreach (LoginClient i in this.clients.Values)
            {
                if (i.User != null)
                {
                    if (i.User.Name.ToLower() == ID.ToLower())
                    {
                        if (my != i)
                            list.Add(i);
                    }
                }
                if (session != 0) break;
            }
            if (list.Count == 0)
                return false;
            else
            {
                foreach (LoginClient i in list)
                {
                    i.Disconnect();
                }
                return true;
            }
        }

        public override Client GetClient(uint SessionID)
        {
            if (clients.ContainsKey(SessionID))
            {
                return clients[SessionID];
            }
            else
            {
                return null;
            }
        }

        public override void OnClientDisconnect(Client client_t)
        {
            LoginClient client = (LoginClient)client_t;

            if (client.isMapServer)
            {
                Logger.ShowWarning("A map server just disconnected.",null);
                LoginServer.charServerList[client.mapServer.worldID].DeleteMapServer(client.mapServer);
            }
            this.clients.Remove(client.SessionID);
        }


    }
}
