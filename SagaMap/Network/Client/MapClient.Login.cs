//#define Preview_Version
using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;
using SagaDB.Mail;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skills;

namespace SagaMap
{
    public partial class MapClient
    {

        #region "0x01 and 0x02"
        // 0x01 and 0x02 Packets =========================================

        public void OnSendKey(SagaMap.Packets.Client.SendKey p)
        {
            if (this.state != SESSION_STATE.NOT_IDENTIFIED) return;

            this.netIO.ClientKey = p.GetKey();

            SagaLib.Packets.Server.AskGUID sendPacket = new SagaLib.Packets.Server.AskGUID();
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }


        public void OnSendGUID(SagaMap.Packets.Client.SendGUID p)
        {
            if (this.state != SESSION_STATE.NOT_IDENTIFIED) return;

            Packets.Server.Identify sendPacket = new Packets.Server.Identify();
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        /// <summary>
        /// Receive the identity of the client.
        /// </summary>
        public void OnSendIdentity(SagaMap.Packets.Client.SendIdentity p)
        {
            MapClient client = new MapClient(p.GetSessionID());
            ActorPC newChar;
            client.netIO = this.netIO;
            MapClientManager.Instance.Clients().Add(client.SessionID, client);
            if (client.state != SESSION_STATE.NOT_IDENTIFIED) return;

            uint charId = p.GetCharID();
            Logger.ShowInfo("Got identity (0x010C) from client: " + charId, null);
            try
            {
                newChar = MapServer.charDB.GetChar(charId);
                if (newChar == null) throw new Exception("cannot load char");
            }
            catch (Exception e)
            {
                Logger.ShowError(e, null);
                client.Disconnect();
                return;
            }
            while ((MapClientManager.Instance.GetClient(newChar.Name) != null))
            {
                Logger.ShowInfo("Character:" + newChar.Name + " already online, kicking....");
                MapClientManager.Instance.GetClient(newChar.Name).Disconnect();
            }
            client.Char = newChar;
            if (client.Char.validationKey != p.GetValidationKey())
            {
                Logger.ShowError("Client " + this.netIO.sock.RemoteEndPoint.ToString() + " sent wrong validation key and got kicked.", null);
                this.Disconnect();
                return;
            }

#if Preview_Version
            if (MapClientManager.Instance.Players.Count > 10)
            {
                this.Disconnect();
                return;
            }
#endif
            if (client.Char.mapID == 0)
            {
                if (client.Char.save_map == 0)
                {
                    client.Char.save_map = 3;
                    client.Char.save_x = -4811.951f;
                    client.Char.save_y = 15936.05f;
                    client.Char.save_z = 3894f;
                }
                client.Char.mapID = client.Char.save_map;
                client.Char.x = client.Char.save_x;
                client.Char.y = client.Char.save_y;
                client.Char.z = client.Char.save_z;
            }

            if (!MapManager.Instance.GetMap(client.Char.mapID, out client.map))
            {
                Logger.ShowError("Could not obtain map for client " + this.netIO.sock.RemoteEndPoint.ToString() + ".", null);
                this.Disconnect();
                return;
            }
            client.CheckWeaponEXP();
            client.Char.e = new ActorEventHandlers.PC_EventHandler(client.Char, client);

            client.Char.Tasks = new Dictionary<string, MultiRunTask>();
            client.state = SESSION_STATE.IDENTIFIED;
            client.Char.invisble = true;
            client.map.RegisterActor(client.Char, p.GetSessionID());
            client.Char.Online = 1;
            //client.taskHeartbeat.Activate();  //heartbeat service deactivated
            client.Pc = new PC(client);
            Logger.ShowInfo("Player:" + client.Char.name + " logged in");
            Logger.ShowInfo("Total Player count:" + MapClientManager.Instance.Players.Count.ToString());
        }

        public void OnLogout(SagaMap.Packets.Client.GwLogout p)
        {
            uint session = p.GetSessionId();
            MapClient client = (MapClient)MapClientManager.Instance.Clients()[session];
            client.OnDisconnect();
        }

        public void Disconnect()
        {
            this.OnDisconnect();
            Packets.Server.ClientKick p = new SagaMap.Packets.Server.ClientKick();
            p.SetSessionID(this.SessionID);
            this.netIO.SendPacket(p, this.SessionID);
        }
        #endregion

    }
}
