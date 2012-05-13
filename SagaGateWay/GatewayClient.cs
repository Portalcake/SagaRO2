using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using SagaLib;

namespace SagaGateway
{
   public class GatewayClient : SagaLib.Client 
   {
       //public LoginSession LogingSession;
       public MapSession MapSession;
       private string serverIP;

       public bool onKick = false;
       public bool onKickMap = false;
       public static int totalInPacketsPerMin;
       public static int totalOutPacketsPerMin;
       public static DateTime timeStamp = DateTime.Now;
               
        public enum SESSION_STATE
        {
            LOGIN,MAP,REDIRECTING,DISCONNECTED
        }
        public SESSION_STATE state;

       
        
        public GatewayClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, true);
            if(this.netIO.sock.Connected) this.OnConnect();
        }

       public override string ToString()
       {
           try
           {
               if (this.netIO != null) return this.netIO.sock.RemoteEndPoint.ToString();
               else
                   return "GatewayClient";
           }
           catch (Exception)
           {
               return "GatewayClient";
           }
       }

       public void OnGotSessionID(uint SessionID)
       {
           this.SessionID = SessionID;
           GatewayClientManager.Instance.clients.Add(SessionID, this);
           byte[] tempServerKey = Encryption.GenerateKey();
           byte[] expandedServerKey = Encryption.GenerateDecExpKey(tempServerKey);
           Packets.Server.SendKey sendPacket = new SagaGateway.Packets.Server.SendKey();
           sendPacket.SetKey(expandedServerKey);
           sendPacket.SetCollumns(4);
           sendPacket.SetRounds(10);
           sendPacket.SetDirection(2);
           this.netIO.SendPacket(sendPacket, this.SessionID);
           this.netIO.ServerKey = tempServerKey;
       }      


       public void OnSendKey(SagaGateway.Packets.Client.SendKey p)
       {
           this.netIO.ClientKey = p.GetKey();           
           Packets.Server.AskGUID send = new SagaGateway.Packets.Server.AskGUID();
           send.PutKey(Gateway.lcfg.CRC_Key);
           this.netIO.SendPacket(send, this.SessionID);
       }

       public void OnSendGUID(SagaGateway.Packets.Client.SendGUID p)
       {
           try
           {
               string key = p.GetKey();
               if (!Gateway.lcfg.Allowed_CRC.Contains(key) && Gateway.lcfg.Allowed_CRC.Count != 0)
               {
                   Logger.ShowWarning("Invalid Client Version CRC!Kicking.... \r\n         Key:" + key);
                   this.netIO.Disconnect();
                   return;
               }
               Packets.Server.SendIdentify p1 = new SagaGateway.Packets.Server.SendIdentify();
               this.netIO.SendPacket(p1, this.SessionID);
           }
           catch (Exception ex)
           {
               Logger.ShowError(ex);
               this.netIO.Disconnect();
               return;
           }
           //this.LogingSession.OnClientSendGUID();
       }

       public void OnSendIdentify(SagaGateway.Packets.Client.SendIdentify p)
       {
           Packet send = new Packet(11);
           send.isGateway = true;
           send.ID = 0x0205;
           this.netIO.SendPacket(send, SessionID);
           //this.SessionID = GatewayClientManager.Instance.GetNextSessionID();
           send = new Packet(11);
           send.isGateway = true;
           send.ID = 0x0206;
           send.SessionID = this.SessionID;
           this.netIO.SendPacket(send, SessionID);
           this.netIO.fullHeader = true;
           Gateway.Login.netIO.fullHeader = true;
           //this.LogingSession.SetHeader(true);
       }

       public void OnSendUniversal(SagaGateway.Packets.Client.SendUniversal p)
       {
           //Logger.ShowInfo(string.Format("Redirecting Packet:{0:X4} of Session:{1} to ServerID:{2:X4}", p.ID, p.SessionID, p.ServerID),null);
           if (p.ServerID == 0x0301)
               Gateway.Login.SendToLogin(p.GetData(), this.SessionID);
           if (p.ServerID == 0x0501)
           {
               Dictionary<string, MapSession> list;
               list = Gateway.Maps;
               if(!list.ContainsKey(this.serverIP))
               {
                   string ip = this.serverIP.Substring(0, this.serverIP.IndexOf(":"));
                   string port = this.serverIP.Substring(this.serverIP.IndexOf(":") + 1);
                   MapSession map = new MapSession(ip, int.Parse(port), this.SessionID);
                   list.Add(this.serverIP, map);
               }
               this.MapSession = list[this.serverIP];
               this.MapSession.SendToMap(p.GetData(), this.SessionID);
           }
           
       }

       public void OnKick(SagaGateway.Packets.Login.Get.ClientKick p)
       {
           this.onKick = true;
           this.netIO.Disconnect();
       }

       public void SendToClient(byte[] data, ushort server)
       {
           Packets.Server.SendPacket p = new SagaGateway.Packets.Server.SendPacket(data.Length);
           p.ServerID = server;
           p.SessionID = this.SessionID;
           p.SetData(data);
           this.netIO.SendPacket(p, this.SessionID);           
       }

       public void SendToMap(Packets.Login.Get.SendToMap p)
       {
           string server = p.GetIPAddress() + ":" + p.GetPort().ToString();
           this.serverIP = server;
           MapSession map;
           this.state = SESSION_STATE.REDIRECTING;
           Dictionary<string, MapSession> maps = Gateway.Maps;
           if (maps.ContainsKey(server)) map = maps[server];
           else
           {
               map = new MapSession(p.GetIPAddress(), p.GetPort(), this.SessionID);               
               maps.Add(server, map);
           }
           this.MapSession = map;
           map.NewSession(this, p.GetCharID(), p.GetValidationKey());
           //this.MapSession = new MapSession(p.GetIPAddress(), p.GetPort(), p.GetCharID(), p.GetValidationKey(),this.SessionID,this);
           //this.MapSession.Connect();
           //this.LogingSession.netIO.Disconnect();
       }

       public void OnRequestSession(Packets.Client.RequestSession p)
       {
           Gateway.Login.RequestNewSession(this);
       }

       public void ConnectedtoMap()
       {
           Packet send = new Packet(11);
            send.isGateway = true;
            send.SessionID = this.SessionID;
            send.ID = 0x0207;
            this.netIO.SendPacket(send, this.SessionID);
            this.state = SESSION_STATE.MAP;
       }

       public override void OnConnect()
       {
           
       }

        public override void OnDisconnect()
        {
            try
            {
                if (this.state == SESSION_STATE.LOGIN)
                {
                    this.state = SESSION_STATE.DISCONNECTED;
                    if (!this.onKick) Gateway.Login.Logout(this);
                }
                if (this.state == SESSION_STATE.MAP)
                {
                    this.state = SESSION_STATE.DISCONNECTED;
                    Gateway.Login.Logout(this);
                    if (!this.onKickMap) this.MapSession.Logout(this);
                }
                this.state = SESSION_STATE.DISCONNECTED;
                GatewayClientManager.Instance.OnClientDisconnect(this);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }


        
   }
}
