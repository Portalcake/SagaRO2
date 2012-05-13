using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;
using SagaDB.Actors;
using SagaDB.Items;
using SagaMap.Scripting;

namespace SagaMap
{
    public sealed class AtCommand
    {
        private delegate void ProcessCommandFunc( MapClient client, string args );

        private struct CommandInfo
        {
            public ProcessCommandFunc func;
            public uint level;

			public CommandInfo(ProcessCommandFunc func, uint lvl)
			{
				this.func = func;
				this.level = lvl;
			}
        }

        private Dictionary<string, CommandInfo> commandTable;
        private static string _MasterName = "Saga";

        AtCommand()
        {
            this.commandTable = new Dictionary<string, CommandInfo>();

            #region "Prefixes"
            string OpenCommandPrefix = "/";
            string GMCommandPrefix = "!";
            string RemoteCommandPrefix = "~";
            #endregion

            #region "Public Commands"
            // public commands
            this.commandTable.Add( OpenCommandPrefix + "yell", new CommandInfo( new ProcessCommandFunc( this.ProcessYell ), 1 ) );
            this.commandTable.Add(OpenCommandPrefix + "where", new CommandInfo( new ProcessCommandFunc( this.ProcessWhere ), 1 ) );
            this.commandTable.Add(OpenCommandPrefix + "getheight", new CommandInfo(new ProcessCommandFunc(this.ProcessGetHeight), 1));
            this.commandTable.Add(OpenCommandPrefix + "lie", new CommandInfo(new ProcessCommandFunc(this.ProcessLie), 1));
            this.commandTable.Add(OpenCommandPrefix + "who", new CommandInfo(new ProcessCommandFunc(this.ProcessWho2), 1));
            #endregion

            #region "GM Commands"
            // gm commands
            //this.commandTable.Add(GMCommandPrefix + "wlevel", new CommandInfo(new ProcessCommandFunc(this.ProcessWlevel), 2));
            this.commandTable.Add(GMCommandPrefix + "map", new CommandInfo( new ProcessCommandFunc( this.ProcessMap ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "broadcast", new CommandInfo( new ProcessCommandFunc( this.ProcessBroadcast ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "jump", new CommandInfo( new ProcessCommandFunc( this.ProcessJump ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "npc", new CommandInfo( new ProcessCommandFunc( this.ProcessNPC ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "item", new CommandInfo( new ProcessCommandFunc( this.ProcessItem ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "info", new CommandInfo( new ProcessCommandFunc( this.ProcessInfo ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "who", new CommandInfo( new ProcessCommandFunc( this.ProcessWho ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "whopp", new CommandInfo(new ProcessCommandFunc(this.ProcessWhopp), 60));
            this.commandTable.Add(GMCommandPrefix + "fsay", new CommandInfo( new ProcessCommandFunc( this.ProcessFsay ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "pjump", new CommandInfo( new ProcessCommandFunc( this.ProcessPJump ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "pcall", new CommandInfo( new ProcessCommandFunc( this.ProcessPCall ), 20 ) );
            this.commandTable.Add(GMCommandPrefix + "time", new CommandInfo( new ProcessCommandFunc( this.ProcessTime ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "weather", new CommandInfo( new ProcessCommandFunc( this.ProcessWeather ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "level", new CommandInfo( new ProcessCommandFunc( this.ProcessLevel ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "tinfo", new CommandInfo( new ProcessCommandFunc( this.ProcessTinfo ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "treset", new CommandInfo( new ProcessCommandFunc( this.ProcessTReset ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "cash", new CommandInfo( new ProcessCommandFunc( this.ProcessCash ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "commandlist", new CommandInfo( new ProcessCommandFunc( this.ProcessCommandList ), 60 ) );
            this.commandTable.Add(GMCommandPrefix + "speed", new CommandInfo(new ProcessCommandFunc(this.ProcessSpeed), 60));
            this.commandTable.Add(GMCommandPrefix + "reloadscript", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadScript), 60));
            this.commandTable.Add(GMCommandPrefix + "reloadconfig", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadConfig), 60));
            this.commandTable.Add(GMCommandPrefix + "mute", new CommandInfo(new ProcessCommandFunc(this.ProcessMute), 20));
            this.commandTable.Add(GMCommandPrefix + "gmc", new CommandInfo(new ProcessCommandFunc(this.ProcessGMChat), 60));
            this.commandTable.Add(GMCommandPrefix + "res", new CommandInfo(new ProcessCommandFunc(this.ProcessRes), 20));
            this.commandTable.Add(GMCommandPrefix + "die", new CommandInfo(new ProcessCommandFunc(this.ProcessDie), 60));
            this.commandTable.Add(GMCommandPrefix + "heal", new CommandInfo(new ProcessCommandFunc(this.ProcessHeal), 60));
            this.commandTable.Add(GMCommandPrefix + "spawn", new CommandInfo(new ProcessCommandFunc(this.ProcessSpawn), 99));
            this.commandTable.Add(GMCommandPrefix + "kick", new CommandInfo(new ProcessCommandFunc(this.ProcessKick), 20));
            this.commandTable.Add(GMCommandPrefix + "kickall", new CommandInfo(new ProcessCommandFunc(this.ProcessKickAll), 99));
            this.commandTable.Add(GMCommandPrefix + "callmap", new CommandInfo(new ProcessCommandFunc(this.ProcessCallMap), 99));
            this.commandTable.Add(GMCommandPrefix + "callall", new CommandInfo(new ProcessCommandFunc(this.ProcessCallAll), 99));
            this.commandTable.Add(GMCommandPrefix + "raw", new CommandInfo(new ProcessCommandFunc(this.ProcessRaw), 100));
            this.commandTable.Add(GMCommandPrefix + "test3", new CommandInfo(new ProcessCommandFunc(this.ProcessTest3), 100));
            this.commandTable.Add(GMCommandPrefix + "test", new CommandInfo(new ProcessCommandFunc(this.ProcessTest), 100));
            this.commandTable.Add(GMCommandPrefix + "test2", new CommandInfo(new ProcessCommandFunc(this.ProcessTest2), 100));
            this.commandTable.Add(GMCommandPrefix + "gc", new CommandInfo(new ProcessCommandFunc(this.ProcessGC), 100));


            #endregion

            #region "Remote Commands"
            // remote commands
            this.commandTable.Add( RemoteCommandPrefix + "jump", new CommandInfo( new ProcessCommandFunc( this.ProcessRJump ), 60 ) );
            this.commandTable.Add( RemoteCommandPrefix + "cash", new CommandInfo( new ProcessCommandFunc( this.ProcessRCash ), 60 ) );
            this.commandTable.Add( RemoteCommandPrefix + "info", new CommandInfo( new ProcessCommandFunc( this.ProcessRInfo ), 60 ) );
            this.commandTable.Add( RemoteCommandPrefix + "res", new CommandInfo( new ProcessCommandFunc(this.ProcessRRes), 60));
            this.commandTable.Add(RemoteCommandPrefix + "die", new CommandInfo(new ProcessCommandFunc(this.ProcessRDie), 60));
            this.commandTable.Add(RemoteCommandPrefix + "heal", new CommandInfo(new ProcessCommandFunc(this.ProcessRHeal), 60));
            #endregion


            #region "Aliases"
            // Aliases
            this.commandTable.Add(GMCommandPrefix + "kill", new CommandInfo(new ProcessCommandFunc(this.ProcessDie), 60));
            this.commandTable.Add(RemoteCommandPrefix + "kill", new CommandInfo(new ProcessCommandFunc(this.ProcessRDie), 60));
            this.commandTable.Add(GMCommandPrefix + "b", new CommandInfo(new ProcessCommandFunc(this.ProcessBroadcast), 60));
            this.commandTable.Add(GMCommandPrefix + "gm", new CommandInfo(new ProcessCommandFunc(this.ProcessGMChat), 60));
            #endregion
        }

        public bool ProcessCommand( MapClient client, string command )
        {
            try
            {
                string[] args = command.Split( " ".ToCharArray(), 2 );
                args[0] = args[0].ToLower();

                if( this.commandTable.ContainsKey( args[0] ) )
                {
                    CommandInfo cInfo = this.commandTable[args[0]];

                    if( client.Char.GMLevel >= cInfo.level )
                    {
                        if (Config.Instance.LogGMCommands && cInfo.level > 2)
                        {
                            string logstring = client.Char.name +" "+ command;
                            Logger.gmlogger.WriteLog(logstring);
                        }

                        if (args.Length == 2)
                            cInfo.func(client, args[1]);
                        else cInfo.func(client, "");
                    }
                    else
                        client.SendMessage( _MasterName, "You do not have access to that command" );

                    return true;
                }
            }
            catch( Exception e ) { Logger.ShowError( e, null ); }

            return false;
        }

        #region "Command Processing"


        #region "Public Commands"
        private void ProcessLie(MapClient client, string args)
        {
            client.Char.stance = Global.STANCE.LIE;
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, null, client.Char, true);
        }

        private void ProcessWho2(MapClient client, string args)
        {
            client.SendMessage(_MasterName, "Total Online Player:" + MapClientManager.Instance.Players.Count);
        }

        private void ProcessYell(MapClient client, string args)
        {
            client.map.SendEventToAllActors(Map.TOALL_EVENT_TYPE.CHAT, new Map.ChatArgs(Packets.Server.SendChat.MESSAGE_TYPE.YELL, args), client.Char, true);
        }

        private void ProcessWhere(MapClient client, string args)
        {
            Logger.ShowInfo("Location request from " + client.Char.name, null);
            client.SendMessage(_MasterName, "Map: " + client.map.Name + ", Region: " + client.Char.region + ". Player " + client.Char.name);
            client.SendMessage(_MasterName, "x: " + client.Char.x + " y: " + client.Char.y + " z: " + client.Char.z + "yaw:" + client.Char.yaw);
        }

        private void ProcessGetHeight(MapClient client, string args)
        {
            client.SendMessage(_MasterName, "x: " + client.Char.x + " y: " + client.Char.y + " z: " + client.Char.z);
            float myZ = client.map.GetHeight(client.Char.x, client.Char.y);
            client.SendMessage(_MasterName, "should-z: " + myZ);
        }
        #endregion

        public void ProcessCommandList(MapClient client, string args)
        {
            int CommandCounter = 0;
            foreach (KeyValuePair<string, CommandInfo> kvp in this.commandTable)
            {
                if (kvp.Value.level <= client.Char.GMLevel)
                {
                    CommandCounter++;
                    client.SendMessage(_MasterName, "Command " + CommandCounter + ": " + kvp.Key);
                }
            }
        }

        private void ProcessSpeed(MapClient client, string args)
        {
            try
            {                
                client.Char.speed = uint.Parse(args);
            }
            catch (Exception)
            {
                client.SendMessage(_MasterName, "Please enter the right parameter(1-999)");
            }
        }

        private void ProcessReloadConfig(MapClient client, string args)
        {
            Config.Instance.ReloadConfig();
            client.SendMessage(_MasterName, "Configuration reloaded!", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
        }

        private void ProcessReloadScript(MapClient client, string args)
        {
            ProcessBroadcast(null, "Saga is now reloading scripts");
            ProcessBroadcast(null, "You may feel laggy");
            MapServer.ScriptManager.ReloadScript(client);
            ProcessBroadcast(null, "Saga completed with reloading scripts");
        }

        private void ProcessMap( MapClient client, string args )
        {
            string[] mapinfo = args.Split( " ".ToCharArray() );
            byte mapid;
            float x, y, z;
            try
            {
                string smap = System.Convert.ToString( mapinfo[0] );
                if( MapManager.Instance.GetMapId( smap ) != -1 )
                    mapid = (byte)MapManager.Instance.GetMapId( smap );
                else
                    mapid = System.Convert.ToByte( mapinfo[0] );
                if( mapid < 1 || mapid > 33 ) throw new Exception( "wrong mapid" );
                if( mapinfo.Length == 4 )
                {
                    x = float.Parse( mapinfo[1] );
                    y = float.Parse( mapinfo[2] );
                    z = float.Parse( mapinfo[3] );
                }
                else
                {
                    x = 0f;
                    y = 0f;
                    z = 0f;
                }
            }
            catch( Exception ) { return; }

            client.map.SendActorToMap( client.Char, mapid, x, y, z );
        }

        private void ProcessInfo( MapClient client, string args )
        {
            client.SendMessage( _MasterName, "You are " + client.Char.name + " with ActorID: " + client.Char.id );
            client.SendMessage( _MasterName, "cLevel: " + ( client.Char.cLevel + 1 ) + ", cExp: " + client.Char.cExp + ", jLevel: " + ( client.Char.jLevel + 1 ) + ", jExp: " + client.Char.jExp );
            uint rufi = client.Char.zeny % 10000;
            uint zeny = client.Char.zeny / 10000;
            client.SendMessage( _MasterName, "Zeny: " + zeny + ", Rufi: " + rufi );
            client.SendMessage(_MasterName, "State: " + client.Char.state + ", stance: " + client.Char.stance);
        }

        private void ProcessRInfo( MapClient client, string args )
        {
            MapClient target = MapClientManager.Instance.GetClient(args);
            if( target == null )
                client.SendMessage( _MasterName, "Client does not exist. Useage: ~info <name>" );
            else
            {
                client.SendMessage(_MasterName, "Info for " + target.Char.name + " with ActorID: " + target.Char.id);
                client.SendMessage(_MasterName, "cLevel: " + (target.Char.cLevel + 1) + ", cExp: " + target.Char.cExp + ", jLevel: " + (target.Char.jLevel + 1) + ", jExp: " + target.Char.jExp);
                uint rufi = target.Char.zeny % 10000;
                uint zeny = target.Char.zeny / 10000;
                client.SendMessage( _MasterName, "Zeny: " + zeny + ", Rufi: " + rufi );
            }
        }

        private void ProcessJump( MapClient client, string args )
        {
            string[] ords = args.Split( " ".ToCharArray() );
            float x, y, z;
            try
            {
                if( ords.Length > 3 ) throw new Exception( "Too many args" );
                if( ords.Length == 3 )
                {
                    x = float.Parse( ords[0] );
                    y = float.Parse( ords[1] );
                    z = float.Parse( ords[2] );
                }
                else
                {
                    float[] newPos = client.map.GetRandomPos();
                    x = newPos[0];
                    y = newPos[1];
                    z = newPos[2];
                }
            }
            catch( Exception ) { return; }

            client.map.TeleportActor( client.Char, x, y, z );

            Logger.ShowInfo( "Sending " + client.Char.name + " to location " + x + " " + y + " " + z, null );
            client.SendMessage( _MasterName, "You Jump to " + x + " " + y + " " + z );
        }

        private void ProcessRJump( MapClient client, string args )
        {
            string[] ords = args.Split( " ".ToCharArray() );
            ActorPC remote = client.map.GetPC( ords[0] );
            if( remote == null )
                client.SendMessage( _MasterName, "Client does not exist. Useage: ~jump <name> [x,y,z]" );
            else
            {
                float x, y, z;
                try
                {
                    if( ords.Length > 4 ) throw new Exception( "Too many args" );
                    if( ords.Length == 4 )
                    {
                        x = float.Parse( ords[1] );
                        y = float.Parse( ords[2] );
                        z = float.Parse( ords[3] );
                    }
                    else
                    {
                        float[] newPos = client.map.GetRandomPos();
                        x = newPos[1];
                        y = newPos[2];
                        z = newPos[3];
                    }
                }
                catch( Exception ) { return; }

                client.map.TeleportActor( remote, x, y, z );

                Logger.ShowInfo( "Sending " + remote.name + " to location " + x + " " + y + " " + z, null );
                remote.e.OnSendMessage( _MasterName, "You have been warped to " + x + " " + y + " " + z );
            }
        }

        public void ProcessMute(MapClient client, string args)
        {
            string[] inputs = args.Split(" ".ToCharArray());
            if (inputs.Length != 1)
            {
                client.SendMessage("Saga", "Usage: !mute playername");
            }
            else
            {
                MapClient tomute = MapClientManager.Instance.GetClient(args);
                if (tomute == null)
                {
                    client.SendMessage("Saga", "Player does not exist");
                }
                else
                {
                    if (tomute.Char.muted == 1)
                        tomute.Char.muted = 0;
                    else
                        tomute.Char.muted = 1;
                }
            }
        }

        public void ProcessNPC( MapClient client, string args )
        {
            uint npcID;
            try { npcID = uint.Parse( args ); }
            catch( Exception ) { npcID = 1; }

            ActorNPC newNPC = new ActorNPC( npcID, 100, 100, 100, 100 );
            if( npcID < 10000 )
            {

                //newNPC.e = new ActorEventHandlers.NPC_EventHandler(newNPC, client.map);
                newNPC.e = new Npc( newNPC, client.map );
            }
            else//Activate AI for sommoned Mobs
            {
                Mob mob = MobFactory.GetMob( newNPC.npcType, ref newNPC );
                if (mob == null)
                {
                    client.SendMessage(_MasterName, "Error, Cannot find mob with ID:" + newNPC.npcType);
                    return;
                }
                mob.Map = client.map;
                mob.ai = new SagaMap.Tasks.MobAI( mob );
                mob.ai.Start();
                newNPC.e = mob;
            }
            newNPC.x = client.Char.x + Global.Random.Next( 100 ); ;
            newNPC.y = client.Char.y + Global.Random.Next( 100 );
            newNPC.z = client.Char.z;
            newNPC.sightRange = Global.MakeSightRange( 100000 );
            newNPC.name = "NPC" + Global.Random.Next().ToString();
            if( client.map.RegisterActor( newNPC ) )
                client.SendMessage( _MasterName, "Spawned NPC with ID " + npcID );
            else
                client.SendMessage( _MasterName, "Error, cannot spawn the npc" );
        }

        public void ProcessItem( MapClient client, string args )
        {
            int itemID;
            Item nItem;
            try { itemID = int.Parse( args ); }
            catch( Exception ) { itemID = 1; }

            try
            {
                nItem = new Item(itemID);
            }
            catch(Exception)
            {
                client.SendMessage( _MasterName, "cannot create item with ID " + itemID );
                return;
            }
            byte index, amount;
            AddItemResult res = client.Char.inv.AddItem( nItem, out index, out amount );
            if( res == AddItemResult.ERROR )
            {
                client.SendMessage( _MasterName, "cannot add item with ID " + itemID );
                return;
            }

            nItem.index = index;
            nItem.stack = amount;

            if( res == AddItemResult.NEW_INDEX )
            {
                Packets.Server.AddItem p1 = new SagaMap.Packets.Server.AddItem();
                p1.SetContainer( CONTAINER_TYPE.INVENTORY );
                p1.SetItem( nItem );
                client.netIO.SendPacket(p1, client.SessionID);
                MapServer.charDB.NewItem(client.Char, nItem);
            }

            Packets.Server.UpdateItem p2 = new SagaMap.Packets.Server.UpdateItem();
            p2.SetContainer( CONTAINER_TYPE.INVENTORY );
            p2.SetItemIndex( nItem.index );
            p2.SetAmount( nItem.stack );
            p2.SetUpdateType( SagaMap.Packets.Server.ITEM_UPDATE_TYPE.AMOUNT );
            p2.SetUpdateReason(ITEM_UPDATE_REASON.FOUND);
            client.netIO.SendPacket(p2, client.SessionID);
            MapServer.charDB.UpdateItem(client.Char, nItem);
            client.SendMessage( _MasterName, "Created item with ID " + itemID );
        }

        public void ProcessWho( MapClient client, string args )
        {
            MapClientManager.Instance.ListClients( client );
            client.SendMessage(_MasterName, "Total online Players:" + MapClientManager.Instance.Players.Count);
        }

        public void ProcessWhopp(MapClient client, string args)
        {
            MapClientManager.Instance.ListClientsAndInfo(client);
            client.SendMessage(_MasterName, "Total online Players:" + MapClientManager.Instance.Players.Count);
        }

        //Just for fun ^_-
        public void ProcessFsay( MapClient client, string args )
        {
            string[] inputs = args.Split( ":".ToCharArray() );
            try
            {
                if( inputs.Length != 2 ) { throw new Exception(); }

                Dictionary<uint, Client> clients = MapClientManager.Instance.Clients();
                foreach( Client i in clients.Values )
                {
                    MapClient target;
                    if (i.GetType() != typeof(MapClient)) continue;
                    target = (MapClient)i;
                        
                    if (target.Char == null) continue;
                    if( target.Char.name == inputs[0] )
                        target.map.SendEventToAllActorsWhoCanSeeActor( Map.EVENT_TYPE.CHAT, new Map.ChatArgs( SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.NORMAL, inputs[1] ), target.Char, true );
                }
            }
            catch( Exception )
            {
                client.SendMessage( _MasterName, "Usage: !fsay name:your message" );
            }
        }

        public void ProcessPJump( MapClient client, string args )
        {
            string[] inputs = args.Split( " ".ToCharArray() );
            Dictionary<uint, Client> clients = MapClientManager.Instance.Clients();
            foreach( Client i in clients.Values )
            {
                MapClient target;
                if (i.GetType() != typeof(MapClient)) continue;
                target = (MapClient)i;
                if (target.Char == null) continue;
                if( target.Char.name == inputs[0] && client.Char.name != inputs[0] )
                {
                    if( inputs.Length != 1 ) { client.SendMessage( _MasterName, "Usage: !pjump <name>" ); }
                    Map destmap;
                    float destx;
                    float desty;
                    float destz;

                    if( MapManager.Instance.GetMap( target.Char.mapID, out destmap ) )
                    {
                        destx = target.Char.x + 100;
                        desty = target.Char.y + 100;
                        destz = destmap.GetHeight( destx, desty );

                    }
                    else
                    {
                        destx = target.Char.x;
                        desty = target.Char.y;
                        destz = target.Char.z + 100;
                    }

                    if( client.Char.mapID == target.Char.mapID )
                        client.map.TeleportActor( client.Char, destx, desty, destz );
                    else
                        client.map.SendActorToMap( client.Char, target.Char.mapID, destx, desty, destz );
                }
            }
        }

        public void ProcessPCall( MapClient client, string args )
        {
            string[] inputs = args.Split( " ".ToCharArray() );
            Dictionary<uint, Client> clients = MapClientManager.Instance.Clients();
            foreach (Client i in clients.Values)
            {
                MapClient target;
                if (i.GetType() != typeof(MapClient)) continue;
                target = (MapClient)i;
                if (target.Char == null) continue;
                if( target.Char.name == inputs[0] )
                {
                    if( inputs.Length != 1 ) { throw new Exception(); }
                    Map destmap;
                    float destx;
                    float desty;
                    float destz;

                    if( MapManager.Instance.GetMap( client.Char.mapID, out destmap ) )
                    {
                        destx = client.Char.x + 100;
                        desty = client.Char.y + 100;
                        destz = destmap.GetHeight( destx, desty );

                    }
                    else
                    {
                        destx = client.Char.x;
                        desty = client.Char.y;
                        destz = client.Char.z + 100;
                    }

                    if( client.Char.mapID == target.Char.mapID )
                        client.map.TeleportActor( target.Char, destx, desty, destz );
                    else
                        client.map.SendActorToMap( target.Char, client.Char.mapID, destx, desty, destz );
                }
            }
        }

        public void ProcessTime( MapClient client, string args )
        {
            Packets.Server.SendTime sendPacket = new SagaMap.Packets.Server.SendTime();
            string[] inputs = args.Split( " ".ToCharArray() );
            int size = ( inputs.Length );
            if( size != 3 ) { client.SendMessage( _MasterName, "invalid time (syntax: day hour min" ); return; }
            client.map.UpdateTime( byte.Parse( inputs[0] ), byte.Parse( inputs[1] ), byte.Parse( inputs[2] ) );
        }

        public void ProcessWeather( MapClient client, string args )
        {
            Global.WEATHER_TYPE newWeather = Global.WEATHER_TYPE.NO_WEATHER;
            args = args.ToLower();
            if( args == "sunny" ) newWeather = Global.WEATHER_TYPE.SUNNY;
            else if( args == "partly cloudy" ) newWeather = Global.WEATHER_TYPE.PARTLY_CLOUDY;
            else if( args == "mostly cloudy" ) newWeather = Global.WEATHER_TYPE.MOSTLY_CLOUDY;
            else if( args == "cloudy" ) newWeather = Global.WEATHER_TYPE.CLOUDY;
            else if( args == "raining" ) newWeather = Global.WEATHER_TYPE.RAINING;
            else if( args == "shower" ) newWeather = Global.WEATHER_TYPE.SHOWER;
            else if( args == "snowing" ) newWeather = Global.WEATHER_TYPE.SNOWING;

            if( newWeather != Global.WEATHER_TYPE.NO_WEATHER )
            {
                client.map.UpdateWeather( newWeather );
                client.SendMessage( _MasterName, "Changed weather to " + newWeather.ToString() );
            }
            else
            {
                client.SendMessage(_MasterName, "Invalid weather type, Possible values: ");
                client.SendMessage(_MasterName, "cloudy,raining,shower,sunny,snowing,partly cloudy,mostly cloudy");
            }
        }

        public void ProcessBroadcast( MapClient client, string args )
        {
            Dictionary<uint, Client> clients = MapClientManager.Instance.Clients();
            foreach (Client i in clients.Values)
            {
                MapClient cli;
                if (i.GetType() != typeof(MapClient)) continue;
                cli = (MapClient)i;
                if (cli.Char == null) continue;
                cli.SendMessage("System", args, SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE_RED);
            }
        }

        public void ProcessTinfo( MapClient client, string args )
        {
            client.SendMessage( "You", "Target: " + client.Char.TradeTarget + " status: " + client.Char.trading );
            ActorPC target = (ActorPC)client.map.GetActor( client.Char.TradeTarget );
            if( target != null )
                client.SendMessage( "Them", "Target: " + target.TradeTarget + " status: " + target.trading );
        }

        public void ProcessTReset( MapClient client, string args )
        {
            client.Char.trading = Trading.NOT_TRADING;
            client.Char.TradeTarget = 0;
            client.SendMessage( _MasterName, "Trading status reset" );
        }

        public void ProcessCash( MapClient client, string args )
        {
            int tzeny, trufi;
            string[] inputs = args.Split( " ".ToCharArray() );
            if( inputs.Length != 2 )
                client.SendMessage( _MasterName, "To get lootz use !cash <rufi> <zeny>" );
            else
            {
                trufi = int.Parse( inputs[0] );
                tzeny = int.Parse( inputs[1] );

                int total = ( tzeny * 10000 ) + trufi;
                uint modtotal = (uint)Math.Abs( total );

                if( total > 0 )
                {
                    client.Char.zeny += modtotal;
                    client.SendMessage( "Cashier", "Enjoy your loot" );
                }
                else
                {
                    if( ( client.Char.zeny - modtotal ) >= 0 )
                        client.Char.zeny -= modtotal;
                    else
                        client.SendMessage( "Cashier", "You don't have enough loot!" );
                    client.SendMessage( "Cashier", "All your loot are belong to me" );
                }
                client.SendZeny();
            }

        }

        public void ProcessRCash( MapClient client, string args )
        {
            int tzeny, trufi;
            string[] inputs = args.Split( " ".ToCharArray() );
            if( inputs.Length != 3 )
                client.SendMessage( _MasterName, "To remote lootz use ~cash <name> <rufi> <zeny>" );
            else
            {
                ActorPC remote = client.map.GetPC( inputs[0] );
                if( remote == null )
                    client.SendMessage( _MasterName, "To remote lootz use ~cash <name> <rufi> <zeny>" );
                else
                {
                    trufi = int.Parse( inputs[1] );
                    tzeny = int.Parse( inputs[2] );

                    int total = ( tzeny * 10000 ) + trufi;
                    uint modtotal = (uint)Math.Abs( total );

                    if( total > 0 )
                    {
                        remote.zeny += modtotal;
                        remote.e.OnSendMessage( "Cashier", "Your monies have been modified" );
                    }
                    else
                    {
                        if( ( client.Char.zeny - modtotal ) >= 0 )
                            client.Char.zeny -= modtotal;
                        else
                            client.SendMessage( "Cashier", "Character doesn't have enough money" );
                        remote.e.OnSendMessage( "Cashier", "Your monies have been modified" );
                    }
                    client.SendZeny();
                }
            }

        }

        public void ProcessLevel( MapClient client, string args )
        {
            string[] inputs = args.Split(' ');
            uint cdifference;
            uint jdifference;
            args = args.ToLower();
            uint level = 1;

            if( inputs.Length >= 2 )
            {
                uint secondVal;
                if( uint.TryParse( inputs[1], out secondVal ) )
                    level = secondVal;
            }

            switch (inputs[0])
            {
                case "c":
                case "char":
                    cdifference = ExperienceManager.Instance.GetExpForLevel((client.Char.cLevel + level - 1), ExperienceManager.LevelType.CLEVEL) - client.Char.cExp;                    
                    client.Char.cExp = client.Char.cExp + cdifference + 1;
                    ExperienceManager.Instance.CheckExp(client, ExperienceManager.LevelType.CLEVEL);
                    break;
                case "j":
                case "job":
                    jdifference = ExperienceManager.Instance.GetExpForLevel((client.Char.jLevel + level - 1), ExperienceManager.LevelType.JLEVEL) - client.Char.jExp;
                    client.Char.jExp = client.Char.jExp + jdifference + 1;
                    ExperienceManager.Instance.CheckExp(client, ExperienceManager.LevelType.JLEVEL);
                    break;
                case "reset":
                    client.Char.cExp = 1;
                    client.Char.jExp = 1;
                    client.Char.cLevel = 1;
                    client.Char.jLevel = 1;
                    client.SendCharStatus(36);
                    break;
                default:
                    cdifference = ExperienceManager.Instance.GetExpForLevel((client.Char.cLevel), ExperienceManager.LevelType.CLEVEL) - client.Char.cExp;
                    jdifference = ExperienceManager.Instance.GetExpForLevel((client.Char.jLevel), ExperienceManager.LevelType.JLEVEL) - client.Char.jExp;
                    client.Char.cExp = client.Char.cExp + cdifference + 1;
                    client.Char.jExp = client.Char.jExp + jdifference + 1;
                    ExperienceManager.Instance.CheckExp(client, ExperienceManager.LevelType.CLEVEL);
                    ExperienceManager.Instance.CheckExp(client, ExperienceManager.LevelType.JLEVEL);
                    break;
            }
        }

        public void ProcessRes(MapClient client, string args)
        {
            if (client.Char.stance == Global.STANCE.DIE)
            {
                client.Char.HP = client.Char.maxHP;
                client.Char.stance = Global.STANCE.REBORN;
                client.Char.state = 0;
                client.SendBattleStatus();
                client.SendCharStatus(0);

                Packets.Server.ActorChangeState p1 = new SagaMap.Packets.Server.ActorChangeState();
                p1.SetActorID(client.Char.id);
                p1.SetBattleState(false);
                p1.SetStance(Global.STANCE.REBORN);
                client.netIO.SendPacket(p1, client.SessionID);  
              
                SagaMap.Map.SkillArgs arg = new Map.SkillArgs();
                client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, arg, client.Char, false);
                client.map.TeleportActor(client.Char, client.Char.x, client.Char.y, client.Char.z);                                
            }
            else
            {
                client.SendMessage(_MasterName, "You aren't dead >:[");
            }
        }

        public void ProcessDie(MapClient client, string args)
        {
            if (client.Char.stance != Global.STANCE.STAND || client.Char.stance != Global.STANCE.RUN)
            {
                client.Char.HP = 0;
                client.Char.e.OnDie();
                SagaMap.Map.SkillArgs arg = new Map.SkillArgs();
                client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, arg, client.Char, true);
            }
            else
            {
                client.SendMessage(_MasterName, "You must be standing to die");
            }
        }

        public void ProcessRRes(MapClient client, string playername)
        {
            MapClient target = MapClientManager.Instance.GetClient(playername);
            if (target != null && target.Char.stance == Global.STANCE.DIE)
            {
                target.Char.HP = target.Char.maxHP;
                target.Char.stance = Global.STANCE.REBORN;
                target.Char.state = 0;
                target.SendBattleStatus();
                target.SendCharStatus(0);

                Packets.Server.ActorChangeState p1 = new SagaMap.Packets.Server.ActorChangeState();
                p1.SetActorID(target.Char.id);
                p1.SetBattleState(false);
                p1.SetStance(Global.STANCE.REBORN);
                target.netIO.SendPacket(p1, target.SessionID); 

                SagaMap.Map.SkillArgs arg = new Map.SkillArgs();
                target.map.TeleportActor(target.Char, target.Char.x, target.Char.y, target.Char.z);
                target.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, arg, target.Char, false);
            }
            else
            {
                client.SendMessage(_MasterName, "Player doesn't exist or isn't dead.");
            }
        }

        public void ProcessRDie(MapClient client, string playername)
        {
            MapClient target = MapClientManager.Instance.GetClient(playername);
            if (target != null && (target.Char.stance != Global.STANCE.STAND || target.Char.stance != Global.STANCE.RUN))
            {
                target.Char.HP = 0;
                target.Char.e.OnDie();
                SagaMap.Map.SkillArgs arg = new Map.SkillArgs();
                target.Char.e.OnActorChangesState(target.Char, arg);
                target.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, arg, target.Char, false);
            }
            else
            {
                client.SendMessage(_MasterName, "Player doesn't exist, or isn't standing.");
            }
        }

        public void ProcessHeal(MapClient client, string args)
        {
            client.Char.HP = client.Char.maxHP;
            client.Char.SP = client.Char.maxSP;
            client.SendCharStatus(0);
        }

        public void ProcessRHeal(MapClient client, string username)
        {
            MapClient target = MapClientManager.Instance.GetClient(username);
            if (target != null)
            {
                target.Char.HP = target.Char.maxHP;
                target.Char.SP = target.Char.maxSP;
                target.SendCharStatus(0);
            }
            else
            {
                client.SendMessage(_MasterName, "Player is not online");
            }
        }

        public void ProcessGMChat(MapClient client, string args)
        {
            Dictionary<uint, Client> clients = MapClientManager.Instance.Clients();
            foreach (Client i in clients.Values)
            {
                MapClient cli;
                if (i.GetType() != typeof(MapClient)) continue;
                cli = (MapClient)i;
                if (cli.Char == null) continue;
                if (cli.Char.GMLevel > 1)
                {
                    cli.SendMessage("", "Message from GM: "+client.Char.name, SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE_RED);
                    cli.SendMessage("GM Chat:", args, SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.YELL);
                }
            }
        }

        /*public void ProcessWlevel(MapClient client, string args)
        {
            string[] inputs = args.Split(" ".ToCharArray());
            uint wdifference;
            args = args.ToLower();
            uint level = 1;

            if (inputs.Length >= 2)
            {
                uint secondVal;
                if (uint.TryParse(inputs[1], out secondVal))
                    level = secondVal;
            }

            switch (args)
            {
                case "w":
                case "weapon":
                    wdifference = LevelManager.Instance.GetExpForLevel((client.Weapon.Level + level), 1);
                    wdifference++;
                    client.weapon.wExp = client.Weapon.wExp + wdifference;
                    LevelManager.Instance.CheckExp(client, 1);
                    break;
                case "reset":
                    client.Weapon.wExp = 1;
                    client.Weapon.wLevel = 1;
                    client.SendCharStatus();
                    break;
                default:
                    edifference = LevelManager.Instance.GetExpForLevel((client.Weapon.Level), 1);
                    edifference++;
                    client.weapon.wExp = client.Weapon.wExp + edifference;
                    LevelManager.Instance.CheckExp(client, 1);
                    break;
            }
        }*/

        #region "Admin commands"

        public void ProcessKickAll(MapClient client, string playername)
        {
            uint[] sessions;
            sessions = new uint[MapClientManager.Instance.Clients().Count];
            MapClientManager.Instance.Clients().Keys.CopyTo(sessions, 0);
            foreach (uint i in sessions)
            {
                try
                {
                    MapClient client_ = (MapClient)MapClientManager.Instance.Clients()[i];
                    if (client_.Char == null) continue;
                    if (client_ == client) continue;
                    client_.Disconnect();
                }
                catch (Exception)
                {

                }
            }

            client.SendMessage(_MasterName, "All Player kicked");
        }


        public void ProcessKick(MapClient client, string playername)
        {
            MapClient target = MapClientManager.Instance.GetClient(playername);
            if (target != null)
            {
                target.Char.e.OnKick();
                client.SendMessage(_MasterName, "Player " + playername + " kicked!");
            }
            else
            {
                client.SendMessage(_MasterName, "Player not found");
            }
        }

        private void ProcessSpawn(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg.Length < 3)
            {
                client.SendMessage(_MasterName, "Invalid parameters!", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
                return;
            }
            System.IO.FileStream fs = new System.IO.FileStream("autospawn.xml", System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            sw.WriteLine("  <spawn>");
            sw.WriteLine(string.Format("    <id>{0}</id>", arg[0]));
            sw.WriteLine(string.Format("    <map>{0}</map>", MapManager.Instance.GetMapName(client.Char.mapID)));
            sw.WriteLine(string.Format("    <x>{0}</x>", (int)client.Char.x));
            sw.WriteLine(string.Format("    <y>{0}</y>", (int)client.Char.y));
            if (arg.Length == 4)
                sw.WriteLine(string.Format("    <z>{0}</z>", (int)client.Char.z));    
            sw.WriteLine(string.Format("    <amount>{0}</amount>", arg[1]));
            sw.WriteLine(string.Format("    <range>{0}</range>", arg[2]));
            sw.WriteLine("  </spawn>");
            sw.Flush();
            fs.Flush();
            fs.Close();
            client.SendMessage(_MasterName, string.Format("Spawn:{0} amount:{1} range:{2} added", arg[0], arg[1], arg[2]), SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
        }

        private void ProcessCallMap(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg.Length != 1)
            {
                client.SendMessage("Saga", "You MUST supply a mapID");
                return;
            }
            int map = int.Parse(arg[0]);
            foreach (MapClient player in MapClientManager.Instance.Players)
            {
                if (player.Char.mapID == map)
                {
                    Map destmap;
                    float destx;
                    float desty;
                    float destz;

                    if (MapManager.Instance.GetMap(client.Char.mapID, out destmap))
                    {
                        destx = client.Char.x + (float)Global.Random.Next(100, 1000);
                        desty = client.Char.y + (float)Global.Random.Next(100, 1000);
                        destz = destmap.GetHeight(destx, desty);

                    }
                    else
                    {
                        destx = client.Char.x + (float)Global.Random.Next(100,1000);
                        desty = client.Char.y + (float)Global.Random.Next(100, 1000);
                        destz = client.Char.z + 100;
                    }

                    if (client.Char.mapID == player.Char.mapID)
                        client.map.TeleportActor(player.Char, destx, desty, destz);
                    else
                        client.map.SendActorToMap(player.Char, client.Char.mapID, destx, desty, destz);
                }
            }            
        }

        //Be careful with this command
        private void ProcessCallAll(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg.Length != 1)
            {
                client.SendMessage("Idiot", "You MUST supply a limit");
                return;
            }
            int limit = int.Parse(arg[0]);
            for (int i = 0; i < limit; i++)
            {
                MapClient target = MapClientManager.Instance.Players[i];
                if (target.Char != null)
                {
                    Map destmap;
                    float destx;
                    float desty;
                    float destz;

                    if (MapManager.Instance.GetMap(client.Char.mapID, out destmap))
                    {
                        destx = client.Char.x + (float)Global.Random.Next(100, 1000);
                        desty = client.Char.y + (float)Global.Random.Next(100, 1000);
                        destz = destmap.GetHeight(destx, desty);

                    }
                    else
                    {
                        destx = client.Char.x + (float)Global.Random.Next(100, 1000);
                        desty = client.Char.y + (float)Global.Random.Next(100, 1000);
                        destz = client.Char.z + 100;
                    }

                    if (client.Char.mapID == target.Char.mapID)
                        client.map.TeleportActor(target.Char, destx, desty, destz);
                    else
                        client.map.SendActorToMap(target.Char, client.Char.mapID, destx, desty, destz);
                }
            }
        }


        #endregion

        #region "Dev commands"
        private void ProcessGC(MapClient client, string args)
        {
            GC.Collect();
        }

        public void ProcessRaw(MapClient client, string args)
        {
            Packets.Server.GenericPacket sendPacket = new SagaMap.Packets.Server.GenericPacket();
            string[] inputs = args.Split(" ".ToCharArray());
            int size = (inputs.Length);
            for (int i = 0; i < size; i++)
            {
                sendPacket.SetData(Conversions.ToByte(inputs[i]));
            }
            client.netIO.SendPacket(sendPacket, client.SessionID);
        }

        private void ProcessTest2(MapClient client, string args)
        {
            ActorPC aActor = MapServer.charDB.GetChar(client.Char.charID);
            Weapon activeweapon = null;
            Packets.Server.ActorPCInfo p1 = new SagaMap.Packets.Server.ActorPCInfo();
            p1.SetActorID((uint)Global.Random.Next(0x000100, 0x0000200));
            p1.SetGender(aActor.sex);
            p1.SetLocation(client.Char.x, client.Char.y, client.Char.z + 500);
            p1.SetYaw(aActor.yaw);
            p1.SetName(aActor.name);
            p1.SetRace(aActor.race);
            p1.SetFace(aActor.face);
            p1.SetDetails(aActor.details);
            foreach (Weapon i in aActor.Weapons)
            {
                if (i.active == 1) activeweapon = i;
            }
            p1.SetEquip(aActor.inv.GetEquipIDs(), aActor.inv.GetEquipDyes(), activeweapon);
            p1.SetJob(aActor.job);
            client.netIO.SendPacket(p1, client.SessionID);

        }

        private void ProcessTest(MapClient client, string args)
        {
            string[] arg = args.Split(' ');

            Packets.Server.QuestInfo p = new SagaMap.Packets.Server.QuestInfo();
            SagaDB.Quest.Quest quest = new SagaDB.Quest.Quest();
            quest.ID = uint.Parse(arg[0]);
            Dictionary<uint, SagaDB.Quest.Step> steps = new Dictionary<uint, SagaDB.Quest.Step>();
            for (int i = 1; i < arg.Length; i++)
            {
                SagaDB.Quest.Step step = new SagaDB.Quest.Step();
                step.ID = uint.Parse(arg[i]);
                step.step = (byte)i;
                step.Status = 1;
                steps.Add(uint.Parse(arg[i]), step);
            }
            quest.Steps = steps;
            List<SagaDB.Quest.Quest> lists = new List<SagaDB.Quest.Quest>();
            lists.Add(quest);
            p.SetQuestInfo(lists);
            client.netIO.SendPacket(p, client.SessionID);

        }

        private void ProcessTest3(MapClient client, string args)
        {
            string[] arg = args.Split('|');
            int itemID;
            Item nItem;
            try { itemID = int.Parse(arg[0]); }
            catch (Exception) { itemID = 1; }
            try
            {
                nItem = new Item(itemID);
            }
            catch (Exception)
            {
                client.SendMessage(_MasterName, "cannot create item with ID " + itemID);
                return;
            }
            Packets.Server.UseDyeingItem packet = new SagaMap.Packets.Server.UseDyeingItem();
            packet.SetError((byte)Global.GENERAL_ERRORS.NO_ERROR);
            packet.SetItemID(nItem.id);
            packet.SetContainer((CONTAINER_TYPE)byte.Parse(arg[1]));
            packet.SetEquipment((EQUIP_SLOT)byte.Parse(arg[2]));
            client.netIO.SendPacket(packet, client.SessionID);
            Item item = client.Char.inv.EquipList[EQUIP_SLOT.BACK];            
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, new Map.ChangeEquipArgs((EQUIP_SLOT)item.index, item.id), client.Char, true); 
        }

        #endregion

        #endregion

        public static AtCommand Instance
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

            internal static readonly AtCommand instance = new AtCommand();
        }
    }
}