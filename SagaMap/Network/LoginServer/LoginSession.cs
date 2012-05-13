using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

using SagaMap.Manager;
using SagaLib;

namespace SagaMap
{
    /// <summary>
    /// Maybe we'll remove this.
    /// </summary>
    public class LoginSession : SagaLib.Client
    {

        public enum SESSION_STATE
        {
            NOT_IDENTIFIED, IDENTIFIED, REJECTED
        }
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED.
        /// </summary>
        public SESSION_STATE state;

        Dictionary<ushort, Packet> commandTable;

        public LoginSession(string host, int port)
        {
            this.commandTable = new Dictionary<ushort,Packet>();

            this.commandTable.Add(0x0201, new Packets.Login.Get.SendKey());
            this.commandTable.Add(0x0202, new Packets.Login.Get.SendGUID());
            this.commandTable.Add(0xFE01, new Packets.Login.Get.MapPing());

            this.commandTable.Add(0xFF01, new Packets.Login.Get.IdentAnswer());

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect(newSock, host, port);
        }

        public void Connect(Socket sock, string host, int port)
        {
            bool Connected = false;
            do
            {
                try
                {
                   Logger.ShowInfo("Trying to connect to loginserver " + host + ":" + port,null);
                   sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(host), port));
                   Connected = true;
                }
                catch (Exception e)
                {
                    Logger.ShowError ("Failed... Trying again in 5sec",null );
                    Logger.ShowError(e.ToString(),null);
                    System.Threading.Thread.Sleep(5000);
                    Connected = false;
                }

            } while (!Connected);

            Logger.ShowInfo("Successfully connected to login server, trying to login...",null);
            this.netIO = new NetIO(sock, this.commandTable, this, MapClientManager.Instance);
        }


        public override void OnConnect()
        {
          
        }

        public void OnSendKey(SagaMap.Packets.Login.Get.SendKey p)
        {
            this.netIO.ClientKey = p.GetKey();

            byte[] tempServerKey = Encryption.GenerateKey();
            byte[] expandedServerKey = Encryption.GenerateDecExpKey(tempServerKey);

            SagaLib.Packets.Server.SendKey sendPacket = new SagaLib.Packets.Server.SendKey();
            this.SessionID = p.SessionID;
            sendPacket.SetKey(expandedServerKey);
            sendPacket.SetCollumns(4);
            sendPacket.SetRounds(10);
            sendPacket.SetDirection(2);
            this.netIO.SendPacket(sendPacket, this.SessionID);

            this.netIO.ServerKey = tempServerKey;
        }

  
        public void OnSendGUID(Packets.Login.Get.SendGUID p)
        {
            Packets.Login.Send.Identify sendPacket = new Packets.Login.Send.Identify();
            sendPacket.SetLoginPassword(MapServer.lcfg.LoginServerPass);
            sendPacket.SetWorldName(MapServer.lcfg.WorldName);
            sendPacket.SetHostedMaps(MapServer.lcfg.HostedMaps);
            sendPacket.SetIP(MapServer.lcfg.Host);
            sendPacket.SetPort(MapServer.lcfg.Port);

            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnIdentAnswer(Packets.Login.Get.IdentAnswer p)
        {
            this.SessionID = p.SessionID;
            MapClientManager.Instance.Clients().Add(this.SessionID, this);
            if (p.GetError() == Packets.Login.Get.IdentError.NO_ERROR)
            {
                Logger.ShowInfo ("Succesfully logged into the login server!",null);
                this.state = SESSION_STATE.IDENTIFIED;
            }
            else if(p.GetError() == Packets.Login.Get.IdentError.MAP_ALREADY_HOSTED)
            {
                Logger.ShowError(" one of my hosted maps is already hosted by another map-server",null);
                this.state = SESSION_STATE.REJECTED;
            }
            else {
                Logger.ShowError("Fatal Error: login server rejected login request",null);
                this.state = SESSION_STATE.REJECTED;
            }
        }
    }
}