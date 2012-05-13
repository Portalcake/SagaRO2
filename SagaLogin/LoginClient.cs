//#define Preview_Version

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

using SagaLib;
using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;

namespace SagaLogin
{
    public class LoginClient : SagaLib.Client
    {
        public User User;
        private List<ActorPC> Chars;

        public bool isMapServer;
        public MapServer mapServer;
        public int MapWorldIndex;
        public bool pinging;

        private MapHeartbeat heartbeatService;

        public static LoginClient CPGateway;

        private enum SESSION_STATE
        {
            NOT_LOGGED_IN, LOGGED_IN, CSERVER_SELECTED, SENT_TO_MAP
        }
        private SESSION_STATE state;

        private byte activeWorldID;


        public LoginClient( Socket mSock, Dictionary<ushort, Packet> mCommandTable , uint session)
        {
            this.SessionID = session;
            this.netIO = new NetIO( mSock, mCommandTable, this ,LoginClientManager.Instance);
            if( this.netIO.sock.Connected ) this.OnConnect();            
        }

        public LoginClient(uint session)
        {
            this.SessionID = session;
        }

        public override string ToString()
        {
            try
            {
                if (this.User != null) return this.User.Name;
                else
                    return "LoginClient";
            }
            catch (Exception)
            {
                return "LoginClient";
            }
        }

        public override void OnDisconnect()
        {
            //TODO:
            //Dispose all clients that connected through this gateway session!
            if (this.User != null) Logger.ShowInfo(this.User.Name + ": [LOGGED_OUT]");
            LoginClientManager.Instance.OnClientDisconnect( this );
        }

        public void RequestMapHeartbeat()
        {
            Packets.Map.Send.MapPing p = new SagaLogin.Packets.Map.Send.MapPing();
            this.netIO.SendPacket(p, this.SessionID);
        }

        public void OnSendKey( SagaLogin.Packets.Client.SendKey p )
        {
            this.netIO.ClientKey = p.GetKey();

            SagaLib.Packets.Server.AskGUID sendPacket = new SagaLib.Packets.Server.AskGUID();
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }


        public void OnSendGUID( SagaLogin.Packets.Client.SendGUID p )
        {

            Packets.Server.Identify sendPacket = new Packets.Server.Identify();
            sendPacket.SetSessionID( LoginClientManager.Instance.GetNextSessionID() );
            this.netIO.SendPacket(sendPacket, this.SessionID);

            /* Does not work with latest client, so let's skip it
            Packets.Server.AskCRC sendPacket = new Packets.Server.AskCRC();
            this.netIO.SendPacket(sendPacket); */
        }

        public void OnRequestNew(SagaLogin.Packets.Client.GwRequestNew p)
        {
            uint NewID = LoginClientManager.Instance.GetNextSessionID();
            try
            {
                LoginClient client = new LoginClient(NewID);
                client.netIO = this.netIO;
                LoginClientManager.Instance.clients.Add(NewID, client);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            Packets.Server.ResponseRequest p1 = new SagaLogin.Packets.Server.ResponseRequest();
            p1.SetSessionID(NewID);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnLogout(SagaLogin.Packets.Client.GwLogout p)
        {
            uint session = p.GetSessionId();
            LoginClient client = LoginClientManager.Instance.clients[session];
            client.OnDisconnect();
        }

        public void Disconnect()
        {
            Packets.Server.ClientKick p = new SagaLogin.Packets.Server.ClientKick();
            p.SetSessionID(this.SessionID);
            this.netIO.SendPacket(p, this.SessionID);
            this.OnDisconnect();
        }

        public void OnSendCRC( SagaLogin.Packets.Client.SendCRC p )
        {
            Logger.ShowInfo( "got guid", null );
            Packets.Server.Identify sendPacket = new Packets.Server.Identify();
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnSendVersion( SagaLogin.Packets.Client.SendVersion p )
        {
            Logger.ShowInfo( this.netIO.sock.RemoteEndPoint.ToString() + " has version " + p.GetIntVersion() + " - " + p.GetUShortVersion() + " ( " + p.GetVersionString() + " ) ", null );

            Packets.Server.SendAck sendPacket = new Packets.Server.SendAck();
            sendPacket.SetAck( true );

            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnSendLogin( SagaLogin.Packets.Client.SendLogin p )
        {
            Packets.Server.LoginAnswer sendPacket = new Packets.Server.LoginAnswer();

            string name = p.GetName().ToUpper(); 
            string pass = p.GetMD5Pass();
            name = name.Replace("'", "\\'");
            Packets.Server.LoginError error;

            try
            {
                if (LoginServer.userDB.CheckPassword(name, pass))
                {
                    this.User = LoginServer.userDB.GetUser( name );
                    if (!User.Banned)
                    {
                        // Login OK
                        if (LoginClientManager.Instance.CheckOnline(this.User.Name, this))
                        {
                            Logger.ShowInfo(name + ": [ALREADY_CONNECTED]");
                            error = SagaLogin.Packets.Server.LoginError.ALREADY_CONNECTED;
                        }
                        else
                        {
                            sendPacket.SetGender(this.User.Sex);
                            sendPacket.SetMaxCharsAllowed(4);
                            sendPacket.SetLastLogin(this.User.lastLogin);
                            error = SagaLogin.Packets.Server.LoginError.NO_ERROR;
                            Logger.ShowInfo(name + ": [LOGIN_OK]", null);
                        }
                    }
                    else
                    {
                        error = SagaLogin.Packets.Server.LoginError.ACCOUNT_NOT_ACTIVATED;
                        Logger.ShowWarning(name + ": [USER_BANNED]", null);
                    }
                }
                else
                {
                    if ((name.EndsWith("_M") || name.EndsWith("_F")) && Config.Instance.Registration)
                    {
                        //Check if user already exists
                        //If not: create & Login ok
                        string realName = name.Substring(0, name.Length - 2);
                        //Check if name contains spaces and strip
                        if (realName.Contains(" "))
                        {
                            realName = Regex.Replace(realName, " ", "");
                        }
                        this.User = LoginServer.userDB.GetUser(realName);
                        if (this.User == null)
                        {     
                            if (name.EndsWith("_M")) this.User = new User(realName, pass, GenderType.MALE);
                            else this.User = new User(realName, pass, GenderType.FEMALE);
                            this.User.lastLogin = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                            LoginServer.userDB.WriteUser(this.User);
                            this.User = LoginServer.userDB.GetUser(this.User);
                            sendPacket.SetGender(this.User.Sex);
                            sendPacket.SetMaxCharsAllowed(3);
                            sendPacket.SetLastLogin("Welcome to Saga!");
                            error = SagaLogin.Packets.Server.LoginError.NO_ERROR;
                            Logger.ShowInfo(realName + ": [ACC_CREATED]", null);
                        }
                        else
                        {
                            error = SagaLogin.Packets.Server.LoginError.WRONG_PASS;
                            Logger.ShowWarning(realName + ": [WRONG_PASS]", null);
                        }
                    }
                    else
                    {
                        this.User = LoginServer.userDB.GetUser(name);
                        if (this.User == null)
                        {
                            error = SagaLogin.Packets.Server.LoginError.WRONG_USER;
                            Logger.ShowWarning(name + ": [ACCOUNT_NOT_EXIST]", null);
                        }
                        else
                        {
                            error = SagaLogin.Packets.Server.LoginError.WRONG_PASS;
                            Logger.ShowWarning(name + ": [WRONG_PASS]", null);
                        }
                    }
                }

                switch (error)
                {
                    case SagaLogin.Packets.Server.LoginError.NO_ERROR :
                        
                        this.state = SESSION_STATE.LOGGED_IN;
                        this.User.lastLogin = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                        LoginServer.userDB.WriteUser(this.User);
                        break;
                    case SagaLogin.Packets.Server.LoginError.WRONG_PASS :
                    case SagaLogin.Packets.Server.LoginError.WRONG_USER :
                    case SagaLogin.Packets.Server.LoginError.ALREADY_CONNECTED :
                        Logger.ShowWarning(name + ": [LOGIN_FAILED]", null);
                        break;
                }
                sendPacket.SetLoginError(error);
                this.netIO.SendPacket(sendPacket, this.SessionID);
            }
            catch( Exception ex )
            {
                Logger.ShowError( ex, null );
                this.Disconnect();
                return;
            }
        }

        public void OnWantServerList( Packets.Client.WantServerList p )
        {
            if( this.state == SESSION_STATE.NOT_LOGGED_IN ) return;

            Packets.Server.SendServerList sendPacket = new Packets.Server.SendServerList();

            foreach (CharServer cInfo in LoginServer.charServerList.Values)
            {
                short charCount = 0;
                try
                {
                    charCount = (short)LoginServer.charServerList[cInfo.worldID].charDB.GetCharIDs(this.User.AccountID).Length;
                }
                catch (Exception)
                {
                    charCount = (short)this.User.chars.Count;
                }
                if (cInfo.mapServers.Count == 0)
                    cInfo.ping = CharServer.Status.MAINTENANCE;
                else
                {
                    MapServer server = cInfo.mapServers[0];
                    if (server.lastPong < server.lastPing)
                        cInfo.ping = CharServer.Status.MAINTENANCE;
                    else
                    {
                        TimeSpan span = server.lastPong - server.lastPing;
                        if (span.TotalMilliseconds < 50)
                            cInfo.ping = CharServer.Status.OK;
                        else if (span.TotalMilliseconds < 300)
                            cInfo.ping = CharServer.Status.CROWDED;
                        else if (span.TotalMilliseconds >= 300)
                            cInfo.ping = CharServer.Status.OVERLOADED;
#if Preview_Version
                        if (LoginClientManager.Instance.clients.Count > 15)
                            cInfo.ping = CharServer.Status.OVERLOADED;
#endif
                    }
                    
                }
                sendPacket.AddServer(cInfo.worldID, cInfo.worldname, charCount, cInfo.ping);
            }

            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnSelectServer( SagaLogin.Packets.Client.SelectServer p )
        {
            if( this.state == SESSION_STATE.NOT_LOGGED_IN ) return;

            byte selServer = p.GetSelServer();

            if( !LoginServer.charServerList.ContainsKey( (int)selServer ) )
            {
                Logger.ShowWarning( this.User.Name + " sent invalid charserver index " + selServer, null );
                this.Disconnect();
                return;
            }
            CharServer cInfo = (CharServer)LoginServer.charServerList[(int)selServer];
            Logger.ShowInfo( this.User.Name + " selected Server: " + cInfo.worldname, null );
            this.activeWorldID = cInfo.worldID;
            uint[] charlist;
            uint tempChar;
            charlist = LoginServer.charServerList[this.activeWorldID].charDB.GetCharIDs( this.User.AccountID );            
            if( charlist != null )
            {
                this.User.chars = new Dictionary<byte, List<uint>>();
                for( int i = 0; i < charlist.Length; i++ )
                {
                    tempChar = charlist[i];

                    if( !this.User.chars.ContainsKey( this.activeWorldID ) )
                    {
                        this.User.chars[this.activeWorldID] = new List<uint>();
                    }

                    this.User.chars[this.activeWorldID].Add( tempChar );
                }
            }
            if( this.Chars == null ) this.Chars = new List<ActorPC>( 1 );
            this.Chars.Clear();

            if( this.User.chars.ContainsKey( selServer ) )
            {
                for( int i = 0; i < this.User.chars[selServer].Count; i++ )
                {
                    try
                    {
                        ActorPC loadChar = cInfo.charDB.GetChar( this.User.chars[selServer][i] );
                        if( loadChar == null ) throw new Exception( "cannot load char" );
                        this.Chars.Add( loadChar );

                    }
                    catch( Exception e )
                    {
                        Console.WriteLine( e.ToString() );
                        this.Disconnect();
                        return;
                    }

                }
            }

            this.state = SESSION_STATE.CSERVER_SELECTED;
            this.SendCharList();
        }

        public void OnWantCharList( Packets.Client.WantCharList p )
        {
            if( this.state != SESSION_STATE.CSERVER_SELECTED ) return;

            this.SendCharList();
        }

        public void OnSelectChar( SagaLogin.Packets.Client.SelectChar p )
        {
            if( this.state != SESSION_STATE.CSERVER_SELECTED ) return;

            int selChar = p.GetSelChar();

            bool error = true;

            if( this.Chars[selChar] != null )
            {
                Logger.ShowInfo( "Getting select char: " + p.GetSelChar() + " char id: " + this.Chars[selChar].charID, null );

                Packets.Server.SendToMapServer sendPacket = new Packets.Server.SendToMapServer();


                if( LoginServer.charServerList.ContainsKey( (int)this.Chars[selChar].worldID ) )
                {

                    MapServer mServer = ( (CharServer)( LoginServer.charServerList[(int)this.Chars[selChar].worldID] ) ).GetMapServer( this.Chars[selChar].mapID );
                    if( mServer != null )
                    {
                        sendPacket.SetServer( mServer.IP, (ushort)mServer.port );

                        uint val1 = this.Chars[selChar].charID;
                        uint val2 = (uint)Global.Random.Next();

                        this.Chars[selChar].validationKey = val2;

                        try { LoginServer.charServerList[this.activeWorldID].charDB.SaveChar( this.Chars[selChar] ); }
                        catch( Exception ) { this.Disconnect(); return; }

                        sendPacket.SetValidation( val1, val2 );

                        this.netIO.SendPacket(sendPacket, this.SessionID);

                        Logger.ShowInfo( "Sending client " + this.User.Name + " to map server " + mServer.IP + " , port " + mServer.port, null );

                        this.state = SESSION_STATE.SENT_TO_MAP;
                        error = false;

                    }
                }
                if( error )
                {
                    Logger.ShowError( "Cannot find map server for selected char: " + p.GetSelChar(), null );
                    this.Disconnect();
                    return;
                }
            }
            else
            {
                Console.WriteLine( "Invalid char index: " + p.GetSelChar() );
                this.Disconnect();
                return;
            }
        }

        public void OnGetCharData( Packets.Client.GetCharData p )
        {
            if( this.state != SESSION_STATE.CSERVER_SELECTED ) return;

            int selChar = (int)p.GetSelChar();

            if (this.Chars.Count < selChar)
            {
                Logger.ShowWarning(this.User.Name + " sent invalid char index " + selChar);
                this.Disconnect();
                return;
            }
            if( this.Chars[selChar] == null )
            {
                Console.WriteLine( this.User.Name + " sent invalid char index " + selChar );
                this.Disconnect();
                return;
            }

            Packets.Server.SendCharData sendPacket = new Packets.Server.SendCharData();

            sendPacket.SetJobExp( this.Chars[selChar].jExp );
            sendPacket.SetFace( this.Chars[selChar].face[0], this.Chars[selChar].face[1], this.Chars[selChar].face[2], this.Chars[selChar].face[3], this.Chars[selChar].face[4] );
            sendPacket.SetEquipment( this.Chars[selChar].inv.GetEquipIDs() );
            sendPacket.SetAugeSkill( this.Chars[selChar].Weapons[0].augeSkillID );
            sendPacket.SetSaveMap( this.Chars[selChar].mapID );

            this.netIO.SendPacket(sendPacket, this.SessionID);

        }

        public void OnCreateChar( Packets.Client.CreateChar p )
        {
            if( this.state != SESSION_STATE.CSERVER_SELECTED ) return;

            bool charAlreadyExists = false;
            try { charAlreadyExists = LoginServer.charServerList[this.activeWorldID].charDB.CharExists( this.activeWorldID, p.GetCharName() ); }
            catch( Exception ) { this.Disconnect(); return; }

            if( charAlreadyExists )
            {
                Packets.Server.SendError p1 = new SagaLogin.Packets.Server.SendError();
                p1.SetError( Packets.Server.ERROR_TYPE.ERROR1 );
                this.netIO.SendPacket(p1, this.SessionID);

                return;
            }


            // WARNING: the data from the client needs to be validated!
            // this is INSECURE!
            ActorPC tempChar = new ActorPC( this.activeWorldID, p.GetCharName() );
            tempChar.userName = this.User.Name;
            tempChar.face = new byte[5];
            tempChar.slots = new byte[7];
            tempChar.details = new byte[6];

            tempChar.face[0] = p.GetEye();
            tempChar.face[1] = p.GetEyeColor();
            tempChar.face[2] = p.GetEyebrows();
            tempChar.face[3] = p.GetHair();
            tempChar.face[4] = p.GetHairColor();
            tempChar.weaponType = p.GetWeaponNameType();
            tempChar.weaponName = p.GetWeaponName();
            tempChar.race = RaceType.NORMAN;
            tempChar.jExp = 0;
            tempChar.cExp = 0;
            tempChar.cLevel = 1;
            tempChar.jLevel = 1;
            tempChar.job = JobType.NOVICE;
            tempChar.pendingDeletion = 0;
            tempChar.HP = Config.Instance.HP;
            tempChar.maxHP = Config.Instance.HP;
            tempChar.SP = Config.Instance.SP;
            tempChar.maxSP = Config.Instance.SP;
            tempChar.LC = 45; 
            tempChar.maxLC = 45; 
            tempChar.LP = 0;
            tempChar.maxLP = 5;
            tempChar.worldID = this.activeWorldID;
            tempChar.GMLevel = 1;
            tempChar.mapID = Config.Instance.Map;
            tempChar.x = Config.Instance.X;
            tempChar.y = Config.Instance.Y;
            tempChar.z = Config.Instance.Z;
            tempChar.yaw = 0;
            tempChar.str = Config.Instance.STR;
            tempChar.dex = Config.Instance.DEX;
            tempChar.intel = Config.Instance.INT;
            tempChar.con = Config.Instance.CON;
            tempChar.luk = 0;
            tempChar.stpoints = 0;
            tempChar.state = 0;
            tempChar.slots[0] = 0x00; // weapon set slot 0
            tempChar.slots[1] = 0x00; // weapon set slot 1
            tempChar.slots[2] = 0x00; // current set slot
            tempChar.slots[3] = 0x32; // invent slot?
            tempChar.slots[4] = 0x32; // ??
            tempChar.slots[5] = 0x01; // weapon slot?
            tempChar.slots[6] = 0x00; // ??
            tempChar.details[0] = 0x01; // pattern
            tempChar.details[1] = 0x01; // ear type
            tempChar.details[2] = 0x0C; // horn type
            tempChar.details[3] = 0x01; // horn color (not used)
            tempChar.details[4] = 0x01; // wing type
            tempChar.details[5] = 0x01; // wing color (not used)
            tempChar.sex = this.User.Sex;
            tempChar.sightRange = Global.MakeSightRange( 6000 );
            tempChar.maxMoveRange = 10000; // just a "guessed" number, we should find a better, smaller value which works
            tempChar.invisble = false; // this actor is visible to other actors
            tempChar.state = 0;
            tempChar.stance = 0;
            tempChar.inv = new SagaDB.Items.Inventory( 50 ); // 50 item slots in the inventory
            // add 2 test items
            foreach (uint i in Config.Instance.Items)
            {
                tempChar.inv.AddItem(new Item((int)i));
            }

            tempChar.BattleSkills = new Dictionary<uint, SkillInfo>();
            foreach (uint i in Config.Instance.Skills)
            {
                tempChar.BattleSkills.Add(i, new SkillInfo());
            }
            if( this.User.AccountID == -1 ) this.User.AccountID = LoginServer.userDB.GetAccountID( this.User.Name );

            try { LoginServer.charServerList[this.activeWorldID].charDB.CreateChar( ref tempChar, this.User.AccountID ); }
            catch( Exception ex )
            {
                Logger.ShowError( ex, null );
                this.Disconnect();
                return;
            }

            if( this.Chars == null ) this.Chars = new List<ActorPC>();
            this.Chars.Add( tempChar );

            if( !this.User.chars.ContainsKey( this.activeWorldID ) )
            {
                this.User.chars[this.activeWorldID] = new List<uint>();
            }

            this.User.chars[this.activeWorldID].Add( tempChar.charID );

            try { LoginServer.userDB.WriteUser( this.User ); }
            catch( Exception ) { this.Disconnect(); return; }

            Packets.Server.SendError p2 = new SagaLogin.Packets.Server.SendError();
            p2.SetError( Packets.Server.ERROR_TYPE.NO_ERROR );
            this.netIO.SendPacket(p2, this.SessionID);
        }

        public void OnDeleteChar( Packets.Client.DeleteChar p )
        {
            if( this.state != SESSION_STATE.CSERVER_SELECTED ) return;

            int delIndex = (int)p.GetCharIndex();

            if( this.Chars[delIndex] == null )
            {
                Console.WriteLine( this.User.Name + " sent invalid char index " + delIndex );
                Packets.Server.SendError p1 = new SagaLogin.Packets.Server.SendError();
                p1.SetError( Packets.Server.ERROR_TYPE.ERROR1 );
                this.netIO.SendPacket(p1, this.SessionID);

                return;
            }


            if( !this.User.chars.ContainsKey( this.activeWorldID ) )
            {
                this.Disconnect();
                return;
            }

            this.User.chars[this.activeWorldID].Remove( this.Chars[delIndex].charID );

            try { LoginServer.userDB.WriteUser( this.User ); }
            catch( Exception ) { this.Disconnect(); return; }

            try { LoginServer.charServerList[this.activeWorldID].charDB.DeleteChar( this.Chars[delIndex] ); }
            catch( Exception ) { this.Disconnect(); return; }

            this.Chars.RemoveAt( delIndex );

            Packets.Server.SendError p2 = new SagaLogin.Packets.Server.SendError();
            p2.SetError( Packets.Server.ERROR_TYPE.NO_ERROR );
            this.netIO.SendPacket(p2, this.SessionID);
        }


        public void OnMapIdentify( Packets.Map.Get.Identify p )
        {
            if( this.state != SESSION_STATE.NOT_LOGGED_IN ) return;

            string serverPass = p.GetPass();
            string serverName = p.GetWorldName();

            Logger.ShowInfo( "new map server connected, worldname: " + serverName, null );

            Packets.Map.Send.LoginAnswer sendPacket = new Packets.Map.Send.LoginAnswer();

            bool validServer = false;
            bool aMapIsAlreadyHosted = false;
            if( serverPass == LoginServer.lcfg.MapServerPass )
            {

                foreach( CharServer cServer in LoginServer.charServerList.Values )
                {
                    if( cServer.worldname == serverName )
                    {
                        if( cServer.MapsAreNotHostedYet( p.GetHostedMaps() ) )
                        {
                            validServer = true;
                            MapServer newServer = new MapServer( cServer.worldID, p.GetIP(), p.GetPort(), this, p.GetHostedMaps(), 0, 0 );
                            cServer.AddMapServer( newServer );
                            this.mapServer = newServer;
                            this.MapWorldIndex = cServer.worldID;
                            this.isMapServer = true;
                            this.heartbeatService = new MapHeartbeat(this);
                            this.heartbeatService.Activate();
                        }
                        else aMapIsAlreadyHosted = true;
                    }
                }
            }

            if( validServer )
            {
                sendPacket.SetError( Packets.Map.Send.IdentError.NO_ERROR );
                Console.WriteLine( "map-server " + serverName + " : [LOGIN_OK]" );
            }
            else if( aMapIsAlreadyHosted )
            {
                sendPacket.SetError( Packets.Map.Send.IdentError.MAP_ALREADY_HOSTED );
                Console.WriteLine( "map-server " + serverName + " : [LOGIN_FAILED] map-server tried to host a map which is already hosted by another map-server" );
            }
            else
            {
                sendPacket.SetError( Packets.Map.Send.IdentError.ERROR );
                Console.WriteLine( "map-server " + serverName + " : [LOGIN_FAILED]" );
            }
            this.netIO.SendPacket(sendPacket, this.SessionID);


        }



        private void SendCharList()
        {
            Packets.Server.SendCharList sendPacket = new Packets.Server.SendCharList();

            sendPacket.SetCharCountAllServer( (byte)this.Chars.Count );
            sendPacket.SetServerName( LoginServer.charServerList[this.activeWorldID].worldname );
            for( int i = 0; i < this.Chars.Count; i++ )
                sendPacket.AddChar( this.Chars[i].charID, this.Chars[i].name, this.Chars[i].race, this.Chars[i].cExp, this.Chars[i].job, this.Chars[i].pendingDeletion );

            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

    }
}
