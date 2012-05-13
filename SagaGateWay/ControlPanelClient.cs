using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using SagaLib;

namespace SagaGateway
{
   public class ControlPanelClient : SagaLib.Client 
   {
       public ControlPanelLoginSession LogingSession;
       public static ControlPanelClient currentCP;
                
        public enum SESSION_STATE
        {
            LOGIN,MAP,REDIRECTING,DISCONNECTED
        }
        public SESSION_STATE state;



       public ControlPanelClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, ControlPanelClientManager.Instance);
            if(this.netIO.sock.Connected) this.OnConnect();
        }

       public void OnLoginSendKey()
       {
           
       }

       public void OnLoginSendIdentify()
       {
          
       }

       public void OnLoginPong()
       {
           Packets.Server.CPReturn p = new SagaGateway.Packets.Server.CPReturn();
           p.SetValue(1);
           this.netIO.SendPacket(p, true);
           //this.netIO.Disconnect();
       }

       public void OnMapPong(byte result)
       {
           Packets.Server.CPReturn p1 = new SagaGateway.Packets.Server.CPReturn();
           p1.SetValue(result);
           this.netIO.SendPacket(p1, true);
           //this.netIO.Disconnect();
       }

       public void OnCPCommand(Packets.Client.CP.CPCommand p)
       {
           string command = p.GetCommand();
           Logger.ShowInfo("CP Command received:" + command, null);
           int times = 5;
           switch (command)
           {
                   
               case  "GET_LOGIN_INFO":
                   while (this.LogingSession == null && times > 0)
                   {
                       System.Threading.Thread.Sleep(1000);
                       times--;
                   }
                   if (this.LogingSession == null)
                   {
                       Packets.Server.CPReturn p2 = new SagaGateway.Packets.Server.CPReturn();
                       p2.SetValue(0);
                       try
                       {
                           this.netIO.SendPacket(p2, true);
                       }
                       catch (Exception)
                       { }
                       //this.netIO.Disconnect();
                       return;
                   }
                   if (this.LogingSession.state == ControlPanelLoginSession.SESSION_STATE.LOGIN)
                       this.LogingSession.LoginPing();
                   else
                   {
                       Packets.Server.CPReturn p2 = new SagaGateway.Packets.Server.CPReturn();
                       p2.SetValue(0);
                       this.netIO.SendPacket(p2, true);
                       //this.netIO.Disconnect();
                   }
                   break;
               case "GET_MAP_INFO":
                   while (this.LogingSession == null && times > 0)
                   {
                       System.Threading.Thread.Sleep(1000);
                       times--;
                   }
                   if (this.LogingSession == null)
                   {
                       Packets.Server.CPReturn p2 = new SagaGateway.Packets.Server.CPReturn();
                       p2.SetValue(0);
                       try
                       {
                           this.netIO.SendPacket(p2, true);
                       }
                       catch (Exception)
                       { }
                       //this.netIO.Disconnect();
                       return;
                   }
                   if (this.LogingSession.state == ControlPanelLoginSession.SESSION_STATE.LOGIN)
                       this.LogingSession.MapPing();
                   else
                   {
                       Packets.Server.CPReturn p2 = new SagaGateway.Packets.Server.CPReturn();
                       p2.SetValue(0);
                       this.netIO.SendPacket(p2, true);
                       //this.netIO.Disconnect();
                   }
                   break;
           }
           
       }

       public override void OnConnect()
       {
           this.LogingSession = new ControlPanelLoginSession(Gateway.lcfg.LoginServer, Gateway.lcfg.LoginPort, this);
       }

        public override void OnDisconnect()
        {
            try
            {
                if (this.state == SESSION_STATE.LOGIN)
                {
                    this.state = SESSION_STATE.DISCONNECTED;
                    if (this.LogingSession.state == ControlPanelLoginSession.SESSION_STATE.LOGIN)
                    {
                        this.LogingSession.netIO.Disconnect();
                    }
                }
                
                this.state = SESSION_STATE.DISCONNECTED;
                ControlPanelClientManager.Instance.OnClientDisconnect(this);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }


        
   }
}
