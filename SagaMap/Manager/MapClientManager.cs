//Comment this out to deactivate the dead lock check!
#define DeadLockCheck
//#define Preview_Version

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaLib;

namespace SagaMap.Manager
{
    sealed class MapClientManager : ClientManager
    {
        private Dictionary<uint, Client> clients;

        public Thread check;
        MapClientManager()
        {
            this.clients = new Dictionary<uint, Client>();
            this.commandTable = new Dictionary<ushort, Packet>();

            // general packets - all servers
            commandTable.Add(0x0201, new Packets.Client.SendKey());
            commandTable.Add(0x0202, new Packets.Client.GwLogout());

            // Map server packets
            commandTable.Add(0x010C, new Packets.Client.SendIdentity());

            //cb2
            /*
            commandTable.Add(0x0301, new Packets.Client.SendDemandMapID());
            commandTable.Add(0x0302, new Packets.Client.SendMapLoaded());
            commandTable.Add(0x0303, new Packets.Client.SendMoveStart());
            commandTable.Add(0x0304, new Packets.Client.SendMoveStop());
            commandTable.Add(0x0305, new Packets.Client.SendYaw());
            commandTable.Add(0x0306, new Packets.Client.SendChangeState());
            commandTable.Add(0x0307, new Packets.Client.SendUsePortal());
            // 0308
            commandTable.Add(0x0309, new Packets.Client.SendHomePoint());
            commandTable.Add(0x030A, new Packets.Client.GetStatUpdate());
            commandTable.Add(0x030C, new Packets.Client.SendJump());
            */
            commandTable.Add(0x0301, new Packets.Client.SendMapLoaded());
            commandTable.Add(0x0302, new Packets.Client.SendMoveStart());
            commandTable.Add(0x0303, new Packets.Client.SendMoveStop());
            commandTable.Add(0x0304, new Packets.Client.SendYaw());
            commandTable.Add(0x0305, new Packets.Client.SendChangeState());
            commandTable.Add(0x0306, new Packets.Client.SendUsePortal());
            commandTable.Add(0x0307, new Packets.Client.JobChange());
            // 0308
            commandTable.Add(0x0308, new Packets.Client.SendHomePoint());
            commandTable.Add(0x0309, new Packets.Client.GetStatUpdate());
            commandTable.Add(0x030B, new Packets.Client.SendJump());
            commandTable.Add(0x030E, new Packets.Client.SendFall());
            commandTable.Add(0x030F, new Packets.Client.DiveDown());
            commandTable.Add(0x0310, new Packets.Client.DiveUp());
            
            commandTable.Add(0x0401, new Packets.Client.GetChat());
            commandTable.Add(0x0402, new Packets.Client.GetWhisper());

            commandTable.Add(0x0501, new Packets.Client.MoveItem());
            //{	0x05,	0x02,	CGameSocket::p_ITEM_IVENLIST		},	// Character item demand
	        //{	0x05,	0x03,	CGameSocket::p_ITEM-WARELIST		},	// Warehouse item demand
            commandTable.Add(0x0504, new Packets.Client.SortInvList());
            // 	{	0x05,	0x05,	CGameSocket::p_ITEM_WARESORT		},	// Item alignment
            //	{	0x05,	0x06,	CGameSocket::p_ITEM_			},	// X
            commandTable.Add(0x0507, new Packets.Client.DeleteItem());
            commandTable.Add(0x0508, new Packets.Client.RepaireEquip());

            commandTable.Add(0x050B, new Packets.Client.SendWeaponMove());
            commandTable.Add(0x050D, new Packets.Client.SendWeaponSwitch());
            commandTable.Add(0x050F, new Packets.Client.WeaponUpgrade());
            commandTable.Add(0x0512, new Packets.Client.WeaponAuge());
            commandTable.Add(0x0517, new Packets.Client.UseMap());
            commandTable.Add(0x0519, new Packets.Client.GetUseDye());
            
            commandTable.Add(0x0601, new Packets.Client.NPCChat());
            commandTable.Add(0x0602, new Packets.Client.GetSelectButton());
            commandTable.Add(0x0603, new Packets.Client.LeaveNPC());
            commandTable.Add(0x0604, new Packets.Client.NPCMenu());
            commandTable.Add(0x0605, new Packets.Client.NPCPersonalRequest());
            commandTable.Add(0x060B, new Packets.Client.SupplySelect());
            commandTable.Add(0x060D, new Packets.Client.SupplyExchange());
            commandTable.Add(0x060F, new Packets.Client.CorpseCleared());
            
            commandTable.Add(0x0610, new Packets.Client.NPCDropList());
            commandTable.Add(0x0611, new Packets.Client.NPCShopSell());
            commandTable.Add(0x0612, new Packets.Client.NPCShopBuy());
            commandTable.Add(0x0614, new Packets.Client.DropSelect());
            
            commandTable.Add(0x0607, new Packets.Client.GetHateInfo());
            commandTable.Add(0x0608, new Packets.Client.GetTargetAttribute());
            commandTable.Add(0x0609, new Packets.Client.GetTargetCancel());

            commandTable.Add(0x0701, new Packets.Client.WantQuestGroupList());
            commandTable.Add(0x0704, new Packets.Client.QuestConfirmCancel());
            commandTable.Add(0x0705, new Packets.Client.QuestConfirm());
            commandTable.Add(0x0706, new Packets.Client.QuestCompleted());
            commandTable.Add(0x0707, new Packets.Client.QuestRewardChoice());


            commandTable.Add(0x0801, new Packets.Client.GetTrade());
            commandTable.Add(0x0802, new Packets.Client.GetTradeOther());
            commandTable.Add(0x0803, new Packets.Client.GetTradeItem());
            commandTable.Add(0x0804, new Packets.Client.GetTradeMoney());
            commandTable.Add(0x0805, new Packets.Client.GetTradeListConfirm());
            commandTable.Add(0x0806, new Packets.Client.GetTradeConfirm());
            commandTable.Add(0x0807, new Packets.Client.GetTradeCancel());

            commandTable.Add(0x0903, new Packets.Client.SkillCast());
            commandTable.Add(0x0904, new Packets.Client.SkillCastCancel());
            commandTable.Add(0x0905, new Packets.Client.UseOffensiveSkill());
            commandTable.Add(0x0906, new Packets.Client.SkillToggle());
            commandTable.Add(0x090A, new Packets.Client.ItemToggle());
            commandTable.Add(0x090C, new Packets.Client.SkillAddSpecial());
            commandTable.Add(0x090D, new Packets.Client.SetSpecialSkill());
            commandTable.Add(0x090E, new Packets.Client.RemoveSpecialSkill());
            commandTable.Add(0x0911, new Packets.Client.WantSetSpeciality());
            

            commandTable.Add(0x0A01, new Packets.Client.AddShortcut());
            commandTable.Add(0x0A02, new Packets.Client.DelShortcut());

            commandTable.Add(0x0C01, new Packets.Client.GetInbox());
            commandTable.Add(0x0C02, new Packets.Client.MailSend());
            commandTable.Add(0x0C03, new Packets.Client.GetMail());
            commandTable.Add(0x0C04, new Packets.Client.GetMailItem());
            commandTable.Add(0x0C05, new Packets.Client.GetMailZeny());
            commandTable.Add(0x0C06, new Packets.Client.MailDelete());
            commandTable.Add(0x0C07, new Packets.Client.GetOutbox());
            commandTable.Add(0x0C08, new Packets.Client.MailCancel());
            commandTable.Add(0x0C09, new Packets.Client.GetOutMail());
            
            commandTable.Add(0x0E02, new Packets.Client.PartyInvite());
            commandTable.Add(0x0E03, new Packets.Client.PartyAccept());
            commandTable.Add(0x0E04, new Packets.Client.PartyQuit());
            commandTable.Add(0x0E05, new Packets.Client.PartyKick());
            commandTable.Add(0x0E06, new Packets.Client.PartyMode());
            commandTable.Add(0x0E08, new Packets.Client.InviteCancel());

            commandTable.Add(0x0F01, new Packets.Client.MarketSearch());
            commandTable.Add(0x0F02, new Packets.Client.MarketBuyItem());
            commandTable.Add(0x0F03, new Packets.Client.MarketOwnItem());
            commandTable.Add(0x0F04, new Packets.Client.MarketRegister());
            commandTable.Add(0x0F05, new Packets.Client.MarketDeleteItem());
            commandTable.Add(0x0F06, new Packets.Client.MarketUpdateComment());
            commandTable.Add(0x0F07, new Packets.Client.MarketGetComment());
            commandTable.Add(0x0F08, new Packets.Client.MarketGetMessage());

            commandTable.Add(0xFD02, new Packets.Client.GwLogout());
            commandTable.Add(0xFE00, new Packets.Client.Heartbeat());
            commandTable.Add(0xFE01, new Packets.Login.Get.MapPing());


            this.waitressQueue = new AutoResetEvent(false);
            this.waitressHasFinished = new ManualResetEvent(false);
            this.waitingWaitressesCount = 0;
            this.waitressCountLock = new Object();

            this.packetCoordinator = new Thread(new ThreadStart(this.packetCoordinationLoop));
            this.packetCoordinator.Start();

            //deadlock check
            check = new Thread(new ThreadStart(this.checkCriticalArea));
#if DeadLockCheck
            check.Start();
#endif        
        }

        public static MapClientManager Instance
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

            internal static readonly MapClientManager instance = new MapClientManager();
        }

        /// <summary>
        /// Connects new clients
        /// </summary>
        public override void NetworkLoop(int maxNewConnections)
        {
            for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
            {
                Socket sock = listener.AcceptSocket();

                Logger.ShowInfo("New client from: " + sock.RemoteEndPoint.ToString(), null);
                uint sessionid = (uint)((uint)0xFFFFFFFF - clients.Count);
                MapClient client = new MapClient(sock, this.commandTable, sessionid);
                ClientManager.EnterCriticalArea();
                while (clients.ContainsKey(sessionid))
                {
                    if (sessionid > 0x40000000)
                        sessionid--;
                    else
                        sessionid++;
                }
                clients.Add(sessionid, client);
                ClientManager.LeaveCriticalArea();
            }
        }
        

        public override void OnClientDisconnect(Client client)
        {
            MapClient client_ = (MapClient)client;
            if (this.clients.ContainsKey(client_.SessionID)) this.clients.Remove(client_.SessionID);
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

        public MapClient GetClient(string name)
        {
            foreach (Client i in this.clients.Values)
            {
                MapClient client;
                if (i.GetType() != typeof(MapClient)) continue;
                client = (MapClient)i;
                if (client.Char == null) continue;
                if (client.Char.name == name)
                    return client;
            }
            return null;
        }


        public void SendToAllClients(Packet p)
        {
            p.doNotEncryptBuffer = true;

            foreach (MapClient client in this.clients.Values)
            {
                if (client.state == MapClient.SESSION_STATE.IDENTIFIED) client.netIO.SendPacket(p, client.SessionID);
            }
        }

        public void SendToOneClient(Packet p, string name)
        {
            foreach (MapClient client in this.clients.Values)
            {
                if (client.state == MapClient.SESSION_STATE.IDENTIFIED && name == client.Char.name) client.netIO.SendPacket(p, client.SessionID);
            }
        }

        public Dictionary<uint, Client> Clients()
        {            
            return this.clients;
        }

        public List<MapClient> Players
        {
            get
            {
                List<MapClient> list = new List<MapClient>();
                foreach (Client client_ in this.clients.Values)
                {
                    MapClient client;
                    try
                    {
                        client = (MapClient)client_;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    if (client.Char != null) list.Add(client);
                }
#if Preview_Version
                if (list.Count > 10)
                {
                    MapServer.LoginServerSession.netIO.Disconnect();
                    MapServer.LoginServerSession = null;
                }
#endif
                return list;
            }
        }

        public void ListClientsAndInfo(MapClient sclient)
        {
            foreach (Client i in this.clients.Values)
            {
                if (i.GetType() != typeof(MapClient)) continue;
                MapClient client = (MapClient)i;
                if (client.Char == null) continue;
                sclient.SendMessage("Saga", "Char: " + client.Char.name + " Map: " + client.Char.mapID + " x:" + client.Char.x + " y:" + client.Char.y + " z:" + client.Char.z);
            }
        }

        public void ListClients(MapClient sclient)
        {
            string message = "";
            int i = 0;
            foreach (Client j in this.clients.Values)
            {
                if (j.GetType() != typeof(MapClient)) continue;
                MapClient temp = (MapClient)j;
                if (temp.Char == null) continue;
                message += (temp.Char.name + " ");
                if ((i % 4) == 0 && i != 0)
                {
                    sclient.SendMessage("Saga", message);
                    message = "";
                }
                i++;
            }
            if(message != "")
                sclient.SendMessage("Saga", message);
        }       
    }
}
