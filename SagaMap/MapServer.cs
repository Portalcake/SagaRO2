using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Diagnostics;

using SagaLib;
using SagaDB;
using SagaDB.Items;
using SagaMap.Manager;

namespace SagaMap
{
    /// <summary>
    /// The MapServer is the biggest component in Saga and keeps track (and responds) of (to) all users that 
    /// are currently logged into the world and all of their actions. The mapserver can be distributed
    /// amonst several systems.
    /// </summary>
    public static class MapServer
    {
        /// <summary>
        /// The characterdatabase associated to this mapserver.
        /// </summary>
        public static ActorDB charDB;

        /// <summary>
        /// Holds the settings for this mapserver.
        /// </summary>
        public static MapConfig lcfg;

        public static LoginSession LoginServerSession;

        public static ScriptManager ScriptManager;
        /// <summary>
        /// Connect to the interserver to access the remote database objects.
        /// </summary>
        public static bool StartDatabase()
        {
            switch (lcfg.ifSQL)
            {
                case 0:
                    charDB = new db4oCharacterDB(lcfg.DBHost, lcfg.DBPort, lcfg.DBUser, lcfg.DBPass);
                    break;
                case 1:
                    charDB = new MySQLCharacterDB(lcfg.DBHost, lcfg.DBPort, lcfg.DBName, lcfg.DBUser, lcfg.DBPass);
                    break;
                case 2:
                    charDB = new DatCharacterDB(lcfg.WorldName, lcfg.DBHost);
                    break;
                case 3:
                    charDB = new MSSQLCharacterDB(lcfg.DBHost, lcfg.DBPort, lcfg.DBName, lcfg.DBUser, lcfg.DBPass);
                    break;
            }
            try
            {
                charDB.Connect();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void EnsureCharDB()
        {
            bool notConnected = false;

            if (!charDB.isConnected())
            {
               Logger.ShowWarning ("LOST CONNECTION TO CHAR DB SERVER!",null);
                notConnected = true;
            }
            while (notConnected)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..",null);
                charDB.Connect();
                if (!charDB.isConnected())
                {
                    Logger.ShowError ("Failed.. Trying again in 10sec",null);
                    System.Threading.Thread.Sleep(10000);
                    notConnected = true;
                }
                else
                {
                    Logger.ShowInfo("SUCCESSFULLY RE-CONNECTED to char db server...",null);
                    Logger.ShowInfo("Clients can now connect again",null);
                    notConnected = false;
                }
            }
        }


        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Logger Log = new Logger("SagaMap.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            AppDomain.CurrentDomain.UnhandledException +=new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);            
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("              Saga Map Server - Internal Beta Version                 ");
            Console.WriteLine("             (C)2007 The Saga Project Development Team                ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ResetColor();
            Logger.ShowInfo("Starting Initialization...",null);
            lcfg = new MapConfig();
            Config.Instance.ReadConfig("Config/MapConfig.xml");
            AdditionFactory.Start("DB/additionDB.xml");
            Item.LoadItems("DB/itemDB.xml");
            WeaponFactory.Start("DB/weaponInfo.xml");
            PortalManager.Start("DB/portalDB.xml");

            Skills.SkillHandler.Initialize();

            MapClientManager.Instance.Start();
            Global.clientMananger = (ClientManager)MapClientManager.Instance;

            if (!StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver",null );
                Logger.ShowError("Shutting down in 20sec.",null);
                System.Threading.Thread.Sleep(20000);
                return;
            }

            MapManager.Instance.LoadMaps(lcfg.HostedMaps);
            MobFactory.Start("DB/MobDB.xml");
            ScriptManager = new ScriptManager();

            LoginServerSession = new LoginSession(lcfg.LoginServerHost,lcfg.LoginServerPort);
            Logger.defaultlogger.LogLevel = (Logger.LogContent)lcfg.LogLevel;
            
            while (LoginServerSession.state == LoginSession.SESSION_STATE.NOT_IDENTIFIED) 
                System.Threading.Thread.Sleep(10);

            if (LoginServerSession.state == LoginSession.SESSION_STATE.REJECTED)
            {
                Logger.ShowWarning("Shutting down in 5sec.",null);
                MapClientManager.Instance.Stop();
                System.Threading.Thread.Sleep(5000);                
                Process.GetCurrentProcess().Close();
                Environment.Exit(0);
                return;
            }

            if (!MapClientManager.Instance.StartNetwork(lcfg.Port))
            {
                Logger.ShowError("cannot listen on port: " + lcfg.Port,null);
                MapClientManager.Instance.Stop();
                Logger.ShowError("Shutting down in 5sec.", null);
                System.Threading.Thread.Sleep(5000);
                Process.GetCurrentProcess().Close();
                Environment.Exit(0);
                return;
            }

            Logger.ShowInfo("Accepting clients, MAP SERVER IS ONLINE!",null);

            // General poonism by will. Temp solution to test 
            ExperienceManager.Instance.LoadTable("DB/exp.xml");

 
            while (true)
            {
                if (!LoginServerSession.netIO.sock.Connected)
                {
                    LoginServerSession.netIO.sock.Close();
                    LoginServerSession.netIO.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    LoginServerSession.Connect(LoginServerSession.netIO.sock, lcfg.LoginServerHost, lcfg.LoginServerPort);
                }
                EnsureCharDB();

                MapClientManager.Instance.NetworkLoop(10);
                

                System.Threading.Thread.Sleep(1);
            }

        }

        private static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            Logger.ShowInfo("Closing.....", null);
            uint[] sessions;            
            sessions = new uint[MapClientManager.Instance.Clients().Count];
            MapClientManager.Instance.Clients().Keys.CopyTo(sessions, 0);
            foreach (uint i in sessions)
            {
                try
                {
                    MapClient client = (MapClient)MapClientManager.Instance.Clients()[i];
                    if (client.Char == null) continue;
                    client.Disconnect();
                }
                catch (Exception) { }
            }                   
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MapClientManager.Instance.check.Abort();
            Exception ex = e.ExceptionObject as Exception;
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...");
            Logger.ShowError("Error Message:" + ex.Message);
            Logger.ShowError("Call Stack:" + ex.StackTrace);
            Logger.ShowError("Trying to save all player's data");
            uint[] sessions;
            sessions = new uint[MapClientManager.Instance.Clients().Count];
            MapClientManager.Instance.Clients().Keys.CopyTo(sessions, 0);
            foreach (uint i in sessions)
            {
                try
                {
                    MapClient client = (MapClient)MapClientManager.Instance.Clients()[i];
                    if (client.Char == null) continue;
                    client.Disconnect();
                }
                catch (Exception) { }
            }                  
        }

    }
}
