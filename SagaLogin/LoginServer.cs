using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaLogin
{
    public static class LoginServer
    {
        public static UserDB userDB;
        public static Dictionary<int, CharServer> charServerList = new Dictionary<int, CharServer>();
        public static LoginConfig lcfg;
        public static WorldConfig wcfg;


        public static bool StartWorldDatabase()
        {
            wcfg = new WorldConfig();

            foreach( WorldConfig.World world in wcfg.Worlds.Values )
            {
                if( !LoginServer.charServerList.ContainsKey( world.ID ) )
                {
                    ActorDB newCharDB = null; ;
                    switch( world.ifSQL )
                    {
                        case 0:
                            newCharDB = new db4oCharacterDB( world.DBHost, world.DBPort, world.DBUser, world.DBPass );
                            break;
                        case 1:
                            newCharDB = new MySQLCharacterDB( world.DBHost, world.DBPort, world.DBName, world.DBUser, world.DBPass );
                            break;
                        case 2:
                            newCharDB = new DatCharacterDB( world.Name, world.DBHost );
                            break;                     
                        case 3:
                            newCharDB = new MSSQLCharacterDB(world.DBHost, world.DBPort, world.DBName, world.DBUser, world.DBPass);
                            break;
                    }

                    try
                    {
                        newCharDB.Connect();
                    }
                    catch( Exception ex )
                    {
                        switch( world.ifSQL )
                        {
                            case 0:
                                Console.WriteLine( "Error: Cannot connect to world db server " + world.DBHost + ":" + world.DBPort + " with username: " + world.DBUser + " and pass: " + world.DBPass );
                                Logger.ShowError( ex, null );
                                break;
                            case 1:
                                Logger.ShowError( "Error: Cannot connect to world db server " + world.DBHost + ":" + world.DBPort + " with username: " + world.DBUser + " and pass: " + world.DBPass, null );
                                Logger.ShowError( ex, null );
                                break;
                        }
                        return false;
                    }
                    charServerList.Add( world.ID, new CharServer( world.Name, 0, 0, (byte)world.ID, newCharDB ) );
                }
                else
                {
                    Console.WriteLine( "ERROR, cannot add world: " + world.Name + " with ID: " + world.ID + " reason: worldID is already taken" );
                    return false;
                }
            }

            return true;
        }

        public static bool StartUserDatabase()
        {
            lcfg = new LoginConfig();
            switch( lcfg.ifSQL )
            {
                case 0:
                    userDB = new db4oUserDB( lcfg.DBHost, lcfg.DBPort, lcfg.DBUser, lcfg.DBPass );
                    break;
                case 1:
                    userDB = new MYSQLUserDB( lcfg.DBHost, lcfg.DBPort, lcfg.DBName, lcfg.DBUser, lcfg.DBPass );
                    break;
                case 2:
                    userDB = new DatUserDB( lcfg.DBHost );
                    break;
                case 3:
                    userDB = new MSSQLUserDB(lcfg.DBHost, lcfg.DBPort, lcfg.DBName, lcfg.DBUser, lcfg.DBPass);
                    break;
            }
            if( !userDB.Connect() )
            {
                switch( lcfg.ifSQL )
                {
                    case 0:
                        Console.WriteLine( "Error: Cannot connect to db server " + lcfg.DBHost + ":" + lcfg.DBPort + " with username: " + lcfg.DBUser + " and pass: " + lcfg.DBPass );

                        break;
                    case 1:
                        Console.WriteLine( "Error: Cannot connect to db server " + lcfg.DBHost + ":" + lcfg.DBPort + " with username: " + lcfg.DBUser + " and pass: " + lcfg.DBPass );
                        break;
                }
                return false;
            }

            return true;
        }

        public static void EnsureUserDB()
        {
            bool notConnected = false;

            if( !userDB.isConnected() )
            {
                Console.WriteLine( "WARNING: LOST CONNECTION TO USER DB SERVER!" );
                notConnected = true;
            }
            if( notConnected )
            {
                Console.WriteLine( "Trying to reconnect to user db server .." );
                userDB.Connect();
                if( !userDB.isConnected() )
                {
                    Console.WriteLine( "Failed.. Trying again in 10sec" );
                    System.Threading.Thread.Sleep( 10000 );
                    notConnected = true;
                }
                else
                {
                    Console.WriteLine( "SUCCESSFULLY RE-CONNECTED to user db server..." );
                    Console.WriteLine( "Clients can now connect again" );
                    notConnected = false;
                }
            }
        }

        public static void EnsureCharDBs()
        {
            foreach( CharServer cServer in charServerList.Values )
            {
                bool notConnected = false;

                if( !cServer.charDB.isConnected() )
                {
                    Console.WriteLine( "WARNING: LOST CONNECTION TO CHAR DB SERVER OF WOLRD " + cServer.worldname + " !" );
                    notConnected = true;
                }
                if( notConnected )
                {
                    Console.WriteLine( "Trying to reconnect to char db server of world " + cServer.worldname );
                    cServer.charDB.Connect();
                    if( !cServer.charDB.isConnected() )
                    {
                        Console.WriteLine( "Failed.. Trying again in 10sec" );
                        notConnected = true;
                        System.Threading.Thread.Sleep( 10000 );

                    }
                    else
                    {
                        Console.WriteLine( "SUCCESSFULLY RE-CONNECTED to char db server of world " + cServer.worldname );
                        Console.WriteLine( "Clients can now connect again" );
                        notConnected = false;
                    }
                }
            }
        }


        static void Main( string[] args )
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Logger Log = new Logger("SagaLogin.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("             Saga Login Server - Internal Beta Version                ");
            Console.WriteLine("             (C)2007 The Saga Project Development Team                ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ResetColor();
            Logger.ShowInfo("Starting Initialization...", null);
            Config.Instance.ReadConfig("Config/CharConfig.xml");
            
            AdditionFactory.Start("DB/additionDB.xml");
            Item.LoadItems( "DB/itemDB.xml" );
            LoginClientManager.Instance.Start();
            Global.clientMananger = (ClientManager)LoginClientManager.Instance;

            if( !StartUserDatabase() || !StartWorldDatabase() )
            {
                Console.WriteLine( "Shutting down in 20sec." );
                System.Threading.Thread.Sleep( 20000 );
                return;
            }
            Logger.defaultlogger.LogLevel = (Logger.LogContent)lcfg.LogLevel;
            Console.WriteLine("Successfully connected to the dbservers.");

            if( !LoginClientManager.Instance.StartNetwork( lcfg.Port ) )
            {
                Console.WriteLine( "Error: cannot listen on port: " + lcfg.Port );
                Console.WriteLine( "Shutting down in 20sec." );
                System.Threading.Thread.Sleep( 20000 );
                return;
            }


            Console.WriteLine( "Accepting clients." );

            while( true )
            {
                // keep the connections to the database servers alive
                EnsureUserDB();
                EnsureCharDBs();

                // let new clients (max 10) connect
                LoginClientManager.Instance.NetworkLoop( 10 );

                // sleep 1ms
                System.Threading.Thread.Sleep( 1 );
            }

        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...", null);
            Logger.ShowError("Error Message:" + ex.Message, null);
            Logger.ShowError("Call Stack:" + ex.StackTrace, null);
        }

    }
}
