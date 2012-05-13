using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

using SagaLib;

namespace SagaGateway
{
    public static class Gateway
    {
        //public static Dictionary<int, CharServer> charServerList = new Dictionary<int, CharServer>();
        public static GatewayConfig lcfg;

        private static int rotator = 0;
        private static int rotatormap = 0;
        private static int loginSessions;
        private static int mapSessions;

        private static LoginSession[] loginServers;
        private static Dictionary<string, MapSession>[] mapServers;

        public static LoginSession Login
        {
            get
            {
                rotator++;
                if (rotator == loginSessions)
                    rotator = 0;
                return loginServers[rotator];
            }
        }

        public static Dictionary<string, MapSession> Maps
        {
            get
            {
                rotatormap++;
                if (rotatormap == mapSessions)
                    rotatormap = 0;
                return mapServers[rotatormap];
            }
        }

        public static void Init()
        {
            Logger.ShowInfo("Initializing workers......");
            loginServers = new LoginSession[lcfg.Conncetions];
            loginSessions = lcfg.Conncetions;
            mapServers = new Dictionary<string, MapSession>[lcfg.Conncetions * 2];
            mapSessions = loginSessions * 2;
            for (int i = 0; i < lcfg.Conncetions; i++)
            {
                loginServers[i] = new LoginSession(lcfg.LoginServer, lcfg.LoginPort);               
            }
            for (int i = 0; i < lcfg.Conncetions * 2; i++)
            {
                mapServers[i] = new Dictionary<string, MapSession>();
            }
            Logger.ShowInfo("Finished..");
        }

        static void Main(string[] args)
        {
            try
            {
                Logger.CurrentLogger = null;
                Logger.defaultlogger = new Logger("SagaGateway.log");
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);
                bool cpvalid = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======================================================================");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("            Saga Gateway Server - Internal Beta Version               ");
                Console.WriteLine("             (C)2007 The Saga Project Development Team                ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======================================================================");
                Console.ResetColor();
                Logger.ShowInfo("Starting Initialization...", null);
                lcfg = new GatewayConfig();
                Logger.defaultlogger.LogLevel = (Logger.LogContent)lcfg.LogLevel;            
                GatewayClientManager.Instance.Start();
                ControlPanelClientManager.Instance.Start();
                if (!ControlPanelClientManager.Instance.StartNetwork(50000))
                {
                    Logger.ShowWarning("Cannot listen on port 50000 for CP!", null);
                    cpvalid = false;
                }
                Global.clientMananger = (ClientManager)GatewayClientManager.Instance;
                Init();
               
                if (!GatewayClientManager.Instance.StartNetwork(lcfg.Port))
                {
                    Logger.ShowError("Cannot listen on port: " + lcfg.Port, null);
                    Logger.ShowWarning("Shutting down in 20sec.", null);
                    System.Threading.Thread.Sleep(20000);
                    return;
                }


                Logger.ShowInfo("Accepting clients.", null);

                while (true)
                {
                    // keep the connections to the database servers alive
                    // let new clients (max 10) connect
                    GatewayClientManager.Instance.NetworkLoop(10);
                    if (cpvalid) ControlPanelClientManager.Instance.NetworkLoop(10);
                    // sleep 1ms
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);               
            }
        }

        public static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            uint[] sessions;
            Logger.ShowInfo("Closing.....", null);
            sessions = new uint[GatewayClientManager.Instance.clients.Count];
            GatewayClientManager.Instance.clients.Keys.CopyTo(sessions, 0);
            foreach (uint i in sessions)
            {
                GatewayClientManager.Instance.clients[i].netIO.Disconnect();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GatewayClientManager.Instance.check.Abort();
            uint[] sessions;
            Exception ex = e.ExceptionObject as Exception;
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...");
            Logger.ShowError("Error Message:" + ex.Message);
            Logger.ShowError("Call Stack:" + ex.StackTrace);
            Logger.ShowError("Trying to save all player's data");
            sessions = new uint[GatewayClientManager.Instance.clients.Count];
            GatewayClientManager.Instance.clients.Keys.CopyTo(sessions, 0);
            foreach (uint i in sessions)
            {
                GatewayClientManager.Instance.clients[i].netIO.Disconnect();
            }
        }

    }
}
