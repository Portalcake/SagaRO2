using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SagaDB
{
    public class MSSQLCharacterDB : ActorDB
    {
        private MSSQLOperator db;
        private MSSQLOperator dbinactive;
        private UnicodeEncoding encoder = new UnicodeEncoding();
        private byte worldID;
        private string host;
        private string port;
        private string database;
        private string dbuser;
        private string dbpass;
        private bool isconnected;
        private DateTime tick = DateTime.Now;
        

        public MSSQLCharacterDB( string host, int port, string database, string user, string pass )
        {
            this.host = host;
            this.port = port.ToString();
            this.dbuser = user;
            this.dbpass = pass;
            this.database = database;
            this.isconnected = false;
            try
            {
                db = new MSSQLOperator(string.Format("Server={1};Uid={2};Pwd={3};Database={0};", database, host, user, pass));
                dbinactive = new MSSQLOperator(string.Format("Server={1};Uid={2};Pwd={3};Database={0};", database, host, user, pass));
                db.Open();
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Connction fails, connection string:" + string.Format("Server={1};Uid={2};Pwd={3};Database={0};", database, host, user, pass), null);
                Logger.ShowSQL(ex, null);
            }
            if( db != null ) { if( db.State != ConnectionState.Closed )this.isconnected = true; else { Console.WriteLine( "SQL Connection error" ); } }
        }

        public bool Connect()
        {
            if( !this.isconnected )
            {
                if( db.State == ConnectionState.Open ) { this.isconnected = true; return true; }
                try
                {
                    db.Open();
                }
                catch( Exception ) { }
                if( db != null ) { if( db.State != ConnectionState.Closed )return true; else return false; }
            }
            return true;
        }

        public bool isConnected()
        {
            if( this.isconnected )
            {
                TimeSpan newtime = DateTime.Now - tick;
                if (newtime.TotalMinutes > 5)
                {
                    MSSQLOperator tmp;
                    Logger.ShowSQL("ActorDB:Pinging SQL Server to keep the connection alive", null);
                    tmp = dbinactive;
                    if (tmp.State == ConnectionState.Open) tmp.Close();
                    tmp.Open();
                    dbinactive = db;
                    db = tmp;
                    tick = DateTime.Now;
                }
                if( db.State == System.Data.ConnectionState.Broken || db.State == System.Data.ConnectionState.Closed )
                {
                    this.isconnected = false;
                }
            }
            return this.isconnected;
        }        

        public void CreateChar( ref ActorPC aChar, int account_id )
        {
            string sqlstr;
            if( aChar != null && this.isConnected() == true )
            {
                string x, y, z;
                x = aChar.x.ToString();
                y = aChar.y.ToString();
                z = aChar.z.ToString();
                if( x.Contains( "," ) ) x.Replace( ",", "." );
                if( y.Contains( "," ) ) y.Replace( ",", "." );
                if( z.Contains( "," ) ) z.Replace( ",", "." );
                aChar.ShorcutIDs = new Dictionary<byte, ActorPC.Shortcut>();
                sqlstr = string.Format( "INSERT INTO chardata(account_id,name,face,details,sex,race,job," +
                    "cEXP,jEXP,cLevel, jLevel , pendingDeletion , validationKey , HP , maxHP , SP , maxSP , LC ," +
                    " maxLC , LP , maxLP , str , dex , intel , con , luk , stpoints , slots , weaponName , weaponType ," +
                    " GMLevel , mapID , worldID , x , y , z , sightRange , maxMoveRange ," +
                    " state , stance , guild , party , yaw , zeny , save_map , save_x , save_y ,  save_z ) VALUES ({0},N'{1}','{2}','{3}',{4},{5},{6},{7}," +
                    "{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22}," +
                    "{23},{24},{25},{26},'{27}',N'{28}',{29},{30},{31},{32},{33},{34},{35},{36},{37}," +
                    "{38},{39},{40},{41},{42},{43},{44},{45},{46},{47})",
                    account_id, aChar.name, bytes2HexString( aChar.face ), bytes2HexString( aChar.details ),
                    (int)aChar.sex, (int)aChar.race, (int)aChar.job, aChar.cExp, aChar.jExp, aChar.cLevel, aChar.jLevel, aChar.pendingDeletion,
                    aChar.validationKey, aChar.HP, aChar.maxHP, aChar.SP, aChar.maxSP, aChar.LC, aChar.maxLC, aChar.LP, aChar.maxLP,
                    aChar.str, aChar.dex, aChar.intel, aChar.con, aChar.luk, aChar.stpoints, bytes2HexString( aChar.slots ), aChar.weaponName,
                    aChar.weaponType, aChar.GMLevel, aChar.mapID, aChar.worldID, x,
                    y, z, aChar.sightRange, aChar.maxMoveRange, aChar.state, aChar.stance,
                    aChar.guild, aChar.party, aChar.yaw, aChar.zeny, aChar.save_map, aChar.save_x, aChar.save_y, aChar.save_z );
                try
                {
                    aChar.charID=(uint)db.ExeSql(sqlstr,0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: can't create new char in database" + ex.Message);
                    throw new Exception("can't create new char in database");
                }
                //aChar.charID = getCharID( aChar.name );
                SaveInv( aChar );
                SaveWeapon( aChar );
                SaveSkills( aChar );
                SaveQuest( aChar );
            }
        }

        private uint getCharID( string name )
        {
            string sqlstr;
            DataRow result = null;
            sqlstr = "SELECT * FROM  chardata  WHERE name='" + name + "'";
            try
            {
                result = db.GetDataTable(sqlstr).Rows[0];
            }
            catch (MySqlException ex)
            {
                Logger.ShowSQL(ex, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error: can't get chars from database" + ex.Message );

                throw new Exception( "can't get chars from database" );
            }
            return (uint)result["charID"];

        }

        private string bytes2HexString( byte[] b )
        {
            string tmp = "";
            int i;
            for( i = 0; i < b.Length; i++ )
            {
                string tmp2 = Conversion.Hex( b[i] );
                if( tmp2.Length == 1 ) tmp2 = "0" + tmp2;
                tmp = tmp + tmp2;
            }
            return tmp;
        }
        private string uint2HexString( uint[] b )
        {
            string tmp = "";
            int i;
            if( b == null ) return "";
            for( i = 0; i < b.Length; i++ )
            {
                string tmp2 = Conversion.Hex( b[i] );
                if( tmp2.Length != 8 )
                {
                    for( int j = 0; j < 8 - tmp2.Length; j++ )
                    {
                        tmp2 = "0" + tmp2;
                    }
                }
                tmp = tmp + tmp2;
            }
            return tmp;
        }

        private byte[] HexStr2Bytes( string s )
        {
            byte[] b = new byte[s.Length / 2];
            int i;
            for( i = 0; i < s.Length / 2; i++ )
            {
                //b[i] = Conversions.ToByte( "&H" + s.Substring( i * 2, 2 ) );
                b[i] = Conversions.ToByte(s.Substring(i * 2, 2));
            }
            return b;
        }

        private uint[] HexStr2uint( string s )
        {
            uint[] b = new uint[s.Length / 8];
            int i;
            for( i = 0; i < s.Length / 8; i++ )
            {
                //b[i] = (uint)Conversions.ToInteger("&H" + s.Substring(i * 8, 8));
                b[i] = (uint)Conversions.ToInteger(s.Substring(i * 8, 8));
            }
            return b;
        }

        public void SaveChar( ActorPC aChar )
        {
            string sqlstr;
            if (aChar != null && this.isConnected() == true)
            {
                string x, y, z;
                x = aChar.x.ToString();
                y = aChar.y.ToString();
                z = aChar.z.ToString();
                if( x.Contains( "," ) ) x = x.Replace( ",", "." );
                if( y.Contains( "," ) ) y = y.Replace( ",", "." );
                if( z.Contains( "," ) ) z = z.Replace( ",", "." );
                if( aChar.ShorcutIDs == null ) aChar.ShorcutIDs = new Dictionary<byte, ActorPC.Shortcut>();
                sqlstr = string.Format("UPDATE  chardata  SET  name =N'{0}', face ='{1}', details ='{2}', sex ={3}, race ={4}, job ={5}," +
                     " cEXP ={6}, jEXP ={7}, cLevel ={8}, jLevel ={9}, pendingDeletion ={10}, validationKey ={11}, HP ={12}, maxHP ={13}, SP ={14}, maxSP ={15}, LC ={16}," +
                     " maxLC ={17}, LP ={18}, maxLP ={19}, str ={20}, dex ={21}, intel ={22}, con ={23}, luk ={24}, stpoints ={25}, slots ='{26}', weaponName =N'{27}', weaponType ={28}," +
                     " GMLevel ={29}, mapID ={30}, worldID ={31}, x ={32}, y ={33}, z ={34}, sightRange ={35}, maxMoveRange ={36}," +
                     " state ={37}, stance ={38}, guild ={39}, party ={40}, yaw ={41}, zeny ={42}, save_map ={43}, save_x ={44}, save_y ={45}, save_z ={46},  online ={48},  Scenario ={49}, muted ={50} WHERE charID={47}",
                     aChar.name, bytes2HexString(aChar.face), bytes2HexString(aChar.details),
                     (int)aChar.sex, (int)aChar.race, (int)aChar.job, aChar.cExp, aChar.jExp, aChar.cLevel, aChar.jLevel, aChar.pendingDeletion,
                     aChar.validationKey, aChar.HP, aChar.maxHP, aChar.SP, aChar.maxSP, aChar.LC, aChar.maxLC, aChar.LP, aChar.maxLP,
                     aChar.str, aChar.dex, aChar.intel, aChar.con, aChar.luk, aChar.stpoints, bytes2HexString(aChar.slots), aChar.weaponName,
                     aChar.weaponType, aChar.GMLevel, aChar.mapID, aChar.worldID, x,
                     y, z, aChar.sightRange, aChar.maxMoveRange, aChar.state, (byte)aChar.stance,
                     aChar.guild, aChar.party, 0, aChar.zeny, aChar.save_map, aChar.save_x, aChar.save_y, aChar.save_z, aChar.charID, aChar.online, aChar.Scenario, aChar.muted);
                try
                {
                    db.ExeSql(sqlstr);
                }
                catch (Exception ex)
                {                    
                    Console.WriteLine( "Error: can't create new char in database" + ex.Message );
                    throw new Exception("can't create new char in database", ex);
                }
                SaveInv( aChar );
                SaveStorage(aChar);
                SaveWeapon( aChar );
                //SaveShortcuts( aChar );
                SaveSkills( aChar );
                SaveQuest( aChar );
                SaveJLevel( aChar );
                SaveMapInfo(aChar);
            }
        }

        private void LoadSkills( ref ActorPC aChar )
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  skills  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }            
            catch (Exception ex)
            {
                Logger.ShowError( " can't get skills from database" + ex.Message, null );
                Logger.ShowError( ex, null );
            }
            aChar.BattleSkills = new Dictionary<uint, SkillInfo>();
            aChar.LivingSkills = new Dictionary<uint, SkillInfo>();
            aChar.SpecialSkills = new Dictionary<uint, SkillInfo>();
            aChar.InactiveSkills = new Dictionary<uint, SkillInfo>();

            for( i = 0; i < result.Count; i++ )
            {
                try
                {
                    byte type;
                    type = (byte)result[i]["type"];
                    SkillInfo info = new SkillInfo();
                    switch (type)
                    {
                        case 0:
                            info.exp = (uint)(int)result[i]["exp"];
                            info.slot = (byte)result[i]["slot"];
                            aChar.BattleSkills.Add((uint)result[i]["skillID"], info);
                            break;
                        case 1:
                            info.exp = (uint)(int)result[i]["exp"];
                            info.slot = (byte)result[i]["slot"];
                            aChar.LivingSkills.Add((uint)(int)result[i]["skillID"], info);
                            break;
                        case 2:
                            info.exp = (uint)result[i]["exp"];
                            info.slot = (byte)result[i]["slot"];
                            aChar.SpecialSkills.Add((uint)result[i]["skillID"], info);
                            break;
                        case 3:
                            info.exp = (uint)(int)result[i]["exp"];
                            info.slot = (byte)result[i]["slot"];
                            aChar.InactiveSkills.Add((uint)(int)result[i]["skillID"], info);
                            break;

                    }
                }
                catch (Exception)
                {
                }
            }

        }

        private void SaveSkills( ActorPC aChar )
        {
            string sqlstr = "DELETE FROM  skills  WHERE charID=" + aChar.charID + ";";
            if( aChar.BattleSkills == null ) aChar.BattleSkills = new Dictionary<uint, SkillInfo>();
            if( aChar.SpecialSkills == null ) aChar.SpecialSkills = new Dictionary<uint, SkillInfo>();
            if( aChar.LivingSkills == null ) aChar.LivingSkills = new Dictionary<uint, SkillInfo>();
            if( aChar.InactiveSkills == null ) aChar.InactiveSkills = new Dictionary<uint, SkillInfo>();
            foreach( uint i in aChar.BattleSkills.Keys )
            {
                sqlstr += string.Format( "INSERT INTO  skills ( charID , type , skillID , exp , slot ) VALUES({0},{1},{2},{3},{4});",
                    aChar.charID, 0, i, aChar.BattleSkills[i].exp, aChar.BattleSkills[i].slot );
                
            }
            foreach( uint i in aChar.LivingSkills.Keys )
            {
                sqlstr += string.Format( "INSERT INTO  skills ( charID , type , skillID , exp , slot ) VALUES({0},{1},{2},{3},{4});",
                aChar.charID, 1, i, aChar.LivingSkills[i].exp, aChar.LivingSkills[i].slot );
            }
            foreach( uint i in aChar.SpecialSkills.Keys )
            {
                sqlstr += string.Format( "INSERT INTO  skills ( charID , type , skillID , exp , slot ) VALUES({0},{1},{2},{3},{4});",
                aChar.charID, 2, i, aChar.SpecialSkills[i].exp, aChar.SpecialSkills[i].slot );
            }

            foreach( uint i in aChar.InactiveSkills.Keys )
            {
                sqlstr += string.Format( "INSERT INTO  skills ( charID , type , skillID , exp , slot ) VALUES({0},{1},{2},{3},{4});",
                aChar.charID, 3, i, aChar.InactiveSkills[i].exp, aChar.InactiveSkills[i].slot );                
            }
            try
            {
               db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new skill in database" + ex.Message);

            }
        }


        private void SaveShortcuts( ActorPC aChar )
        {
            string sqlstr = "DELETE FROM  shortcuts  WHERE charID=" + aChar.charID + "";
            if( aChar.ShorcutIDs == null ) aChar.ShorcutIDs = new Dictionary<byte, ActorPC.Shortcut>();
            foreach( byte i in aChar.ShorcutIDs.Keys )
            {
                sqlstr = string.Format( "INSERT INTO  shortcuts ( charID , slotnumber , type , itemID ) VALUES({0},{1},{2},{3})",
                aChar.charID, i, aChar.ShorcutIDs[i].type, aChar.ShorcutIDs[i].ID );
                try
                {
                    db.ExeSql(sqlstr);
                }
                catch (MySqlException ex)
                {
                    Logger.ShowSQL(ex, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine( "Error: can't insert new weapon in database" + ex.Message );

                }
            }

        }

        private void SaveQuest( ActorPC aChar )
        {
            string sqlstr = "DELETE FROM  quest  WHERE charID=" + aChar.charID + ";";
            if (aChar.QuestTable == null) aChar.QuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            if (aChar.PersonalQuestTable == null) aChar.PersonalQuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            foreach (uint i in aChar.QuestTable.Keys)
            {
                string tmp = "";
                foreach( uint j in aChar.QuestTable[i].Steps.Keys )
                {
                    Quest.Step step = aChar.QuestTable[i].Steps[j];
                    tmp = tmp + step.step.ToString() + "," + step.ID.ToString() + "," + step.Status.ToString() + "," + step.nextStep.ToString() + ",";
                }
                tmp = tmp.Substring( 0, tmp.Length - 1 );
                sqlstr += string.Format( "INSERT INTO  quest ( charID , questID , step , type ) VALUES({0},{1},'{2}',0);",
                aChar.charID, aChar.QuestTable[i].ID, tmp );                
            }
            if (aChar.PersonalQuestTable == null) aChar.PersonalQuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            foreach (uint i in aChar.PersonalQuestTable.Keys)
            {
                string tmp = "";
                foreach (uint j in aChar.PersonalQuestTable[i].Steps.Keys)
                {
                    Quest.Step step = aChar.PersonalQuestTable[i].Steps[j];
                    tmp = tmp + step.step.ToString() + "," + step.ID.ToString() + "," + step.Status.ToString() + "," + step.nextStep.ToString() + ",";
                }
                tmp = tmp.Substring(0, tmp.Length - 1);
                sqlstr += string.Format("INSERT INTO  quest ( charID , questID , step , type ) VALUES({0},{1},'{2}',1);",
                aChar.charID, aChar.PersonalQuestTable[i].ID, tmp);               
            }
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new quest in database" + ex.Message);

            }
        }

        private void SaveJLevel( ActorPC aChar )
        {
            string sqlstr = "DELETE FROM  joblevel  WHERE charID=" + aChar.charID + ";";
            if( aChar.JobLevels == null ) aChar.JobLevels = new Dictionary<JobType, byte>();
            foreach( JobType i in aChar.JobLevels.Keys )
            {
                sqlstr += string.Format( "INSERT INTO  joblevel ( charID , job , level ) VALUES({0},{1},{2});",
                aChar.charID, (byte)i, aChar.JobLevels[i] );                
            }
            try
            {
                db.ExeSql(sqlstr);
            }            
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new job levels in database" + ex.Message);

            }
        }

        private void SaveMapInfo(ActorPC aChar)
        {
            string sqlstr = "DELETE FROM  MapInfo  WHERE charID=" + aChar.charID + ";";
            if (aChar.JobLevels == null) aChar.JobLevels = new Dictionary<JobType, byte>();
            foreach (byte i in aChar.MapInfo.Keys)
            {
                sqlstr += string.Format("INSERT INTO  MapInfo ( charID , map , [value] ) VALUES({0},{1},{2});",
                aChar.charID, (byte)i, aChar.MapInfo[i]);
            }
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new mapinfo in database" + ex.Message);

            }
        }

        private void LoadJLevel( ref ActorPC aChar )
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  joblevel  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Logger.ShowError(" can't get joblevels from database" + ex.Message, null);
                Logger.ShowError(ex, null);
            }
            aChar.JobLevels = new Dictionary<JobType, byte>();
            for( i = 0; i < result.Count; i++ )
            {
                try
                {
                    aChar.JobLevels.Add((JobType)(byte)result[i]["job"], (byte)result[i]["level"]);
                }
                catch (Exception) { }
            }

        }

        private void LoadMapInfo(ref ActorPC aChar)
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  MapInfo  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Logger.ShowError(" can't get mapinfo from database" + ex.Message, null);
                Logger.ShowError(ex, null);
            }
            aChar.MapInfo = new Dictionary<byte, byte>();
            for (i = 0; i < result.Count; i++)
            {
                try
                {
                    aChar.MapInfo.Add((byte)result[i]["map"], (byte)result[i]["value"]);
                }
                catch (Exception) { }
            }
            if (!aChar.MapInfo.ContainsKey(2))
                aChar.MapInfo.Add(2, 128);

        }

        private void LoadShortcuts( ref ActorPC aChar )
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  shortcuts  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Logger.ShowError( " can't get shortcuts from database" + ex.Message, null );
                Logger.ShowError( ex, null );
            }
            aChar.ShorcutIDs = new Dictionary<byte, ActorPC.Shortcut>();
            for( i = 0; i < result.Count; i++ )
            {
                byte slot;
                ActorPC.Shortcut sc = new ActorPC.Shortcut();
                slot = (byte)result[i]["slotnumber"];
                sc.type = (byte)result[i]["type"];
                sc.ID = (uint)(int)result[i]["itemID"];
                aChar.ShorcutIDs.Add( slot, sc );
            }

        }

        private void LoadQuest( ref ActorPC aChar )
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  quest  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Logger.ShowError( " can't get quests from database" + ex.Message, null );
                Logger.ShowError( ex, null );
            }
            aChar.QuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            aChar.PersonalQuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            for( i = 0; i < result.Count; i++ )
            {
                try
                {
                    Quest.Quest sc = new SagaDB.Quest.Quest();
                    string[] tmp;
                    tmp = result[i]["step"].ToString().Split(',');
                    for (int j = 0; j < (tmp.Length / 4); j++)
                    {
                        Quest.Step step = new SagaDB.Quest.Step();
                        step.step = Convert.ToByte(tmp[j * 4]);
                        step.ID = Convert.ToUInt32(tmp[j * 4 + 1]);
                        step.Status = Convert.ToByte(tmp[j * 4 + 2]);
                        step.nextStep = Convert.ToUInt32(tmp[j * 4 + 3]);
                        sc.Steps.Add(step.ID, step);
                    }
                    sc.ID = (uint)(int)result[i]["questID"];
                    switch ((byte)result[i]["type"])
                    {
                        case 0:

                            aChar.QuestTable.Add(sc.ID, sc);
                            break;
                        case 1:
                            aChar.PersonalQuestTable.Add(sc.ID, sc);
                            break;
                    }
                }
                catch (Exception)
                {

                }
            }

        }

        private void SaveInv( ActorPC aChar )
        {
            int[] tmp;
            int equip;
            List<Items.Item> inv = aChar.inv.GetInventoryList();
            string sqlstr = "DELETE FROM  inventory  WHERE charID=" + aChar.charID + ";";
            tmp = aChar.inv.GetEquipIDs();

            for( int j = 0; j < tmp.Length; j++ )
            {
                Item tmpitem;
                if( tmp[j] != 0 )
                {
                    tmpitem = aChar.inv.EquipList[(EQUIP_SLOT)j];
                    sqlstr += string.Format( "INSERT INTO  inventory ( charID , nameid , amount , creatorName , durability , equip ) VALUES({0},{1},{2},N'{3}',{4},{5});",
                aChar.charID, tmpitem.id, tmpitem.stack, tmpitem.creatorName, tmpitem.durability, j );
                   
                }
            }
            foreach( Items.Item i in inv )
            {
                equip = -1;
                sqlstr += string.Format( "INSERT INTO  inventory ( charID , nameid , amount , creatorName , durability , equip ) VALUES({0},{1},{2},N'{3}',{4},{5});",
                aChar.charID, i.id, i.stack, i.creatorName, i.durability, equip );                
            }
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new inventory in database" + ex.Message);
                throw new Exception("can't insert new inventory in database");
            }
            
        }

        private void SaveStorage(ActorPC aChar)
        {
            int equip;
            List<Items.Item> inv = aChar.inv.GetStorageList();
            string sqlstr = "DELETE FROM  storage  WHERE charID=" + aChar.charID + ";";
            
            foreach (Items.Item i in inv)
            {
                equip = -1;
                sqlstr += string.Format("INSERT INTO  storage ( charID , nameid , amount , creatorName , durability , equip ) VALUES({0},{1},{2},N'{3}',{4},{5});",
                aChar.charID, i.id, i.stack, i.creatorName, i.durability, equip);
            }
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new items in database" + ex.Message);
                throw new Exception("can't insert new items in database");
            }

        }

        public void DeleteChar( ActorPC aChar )
        {
            string sqlstr;
            sqlstr = "DELETE FROM  chardata  WHERE charID=" + aChar.charID + ";";
            sqlstr += "DELETE FROM  inventory  WHERE charID=" + aChar.charID + ";";
            sqlstr += "DELETE FROM  weapon  WHERE charID=" + aChar.charID + ";";
            sqlstr += "DELETE FROM  skills  WHERE charID=" + aChar.charID + ";" +
                "DELETE FROM  quest  WHERE charID=" + aChar.charID + ";" + "DELETE FROM  joblevel  WHERE charID=" + aChar.charID + ";" +
                "DELETE FROM  storage  WHERE charID=" + aChar.charID + ";";

            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error: can't delete char in database" + ex.Message );
                throw new Exception( "can't insert new inventory in database" );
            }
        }

        public ActorPC GetChar( uint charID )
        {
            string sqlstr;
            DataRow result = null;
            DataSet tmp = null;
            ActorPC pc;
            sqlstr = "SELECT * FROM  chardata  WHERE charID=" + charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows[0];
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                Console.WriteLine( "Error: can't find char with charID:" + charID );
                Logger.ShowInfo("Table count:" + tmp.Tables.Count, null);                
                return null;
            }
            pc = new ActorPC( (byte)result["worldID"], (string)result["name"] );
            pc.Tasks = new Dictionary<string, MultiRunTask>();
            pc.inv = new Inventory( 50 );
            pc.charID = charID;
            pc.name = (string)result["name"];
            pc.face = HexStr2Bytes( (string)result["face"] );
            pc.details = HexStr2Bytes( (string)result["details"] );
            pc.sex = (GenderType)Convert.ToInt32( result["sex"] );
            pc.race = (RaceType)Convert.ToInt32( ( result["race"] ) );
            pc.job = (JobType)Convert.ToInt32( ( result["job"] ) );
            pc.cExp = (uint)(int)result["cEXP"];
            pc.jExp = (uint)(int)result["jEXP"];
            pc.cLevel = (uint)(byte)result["cLevel"];
            pc.jLevel = (uint)(byte)result["jLevel"];
            pc.pendingDeletion = (byte)result["pendingDeletion"];
            pc.validationKey = (uint)(int)result["validationKey"];
            pc.HP = (ushort)(short)result["HP"];
            pc.maxHP = (ushort)(short)result["maxHP"];
            pc.SP = (ushort)(short)result["SP"];
            pc.maxSP = (ushort)(short)result["maxSP"];
            pc.LC = (byte)result["LC"];
            pc.maxLC = (byte)result["maxLC"];
            pc.LP = (byte)result["LP"];
            pc.maxLP = (byte)result["maxLP"];
            pc.str = (byte)result["str"];
            pc.dex = (byte)result["dex"];
            pc.intel = (byte)result["intel"];
            pc.con = (byte)result["con"];
            pc.luk = (byte)result["luk"];
            pc.stpoints = (byte)result["stpoints"];
            pc.slots = HexStr2Bytes( (string)result["slots"] );
            pc.weaponName = (string)result["weaponName"];
            pc.weaponType = (int)(short)result["weaponType"];
            pc.GMLevel = (uint)(byte)result["GMLevel"];
            pc.mapID = (byte)result["mapID"];
            pc.worldID = (byte)result["worldID"];
            pc.x = (float)(double)result["x"];
            pc.y = (float)(double)result["y"];
            pc.z = (float)(double)result["z"];
            pc.zeny = (uint)(int)result["zeny"];
            pc.save_map = (byte)result["save_map"];
            pc.save_x = (float)(double)result["save_x"];
            pc.save_y = (float)(double)result["save_y"];
            pc.save_z = (float)(double)result["save_z"];
            pc.sightRange = (uint)(int)result["sightRange"];
            pc.maxMoveRange = (uint)(int)result["maxMoveRange"];
            pc.state = (byte)result["state"];
            pc.stance = (Global.STANCE)(byte)result["stance"];
            pc.guild = (uint)(int)result["guild"];
            pc.party = (uint)(int)result["party"];
            pc.Scenario = (uint)(int)result["Scenario"];
            LoadInv( ref pc );
            LoadStorage(ref pc);
            LoadWeapon( ref pc );
            //LoadShortcuts( ref pc );
            LoadSkills( ref pc );
            LoadQuest( ref pc );
            LoadJLevel( ref pc );
            LoadMapInfo(ref pc);
            return pc;
        }

        private void LoadInv( ref ActorPC aChar )
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  inventory  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error: can't get inventoris from database" + ex.Message );
                throw new Exception( "can't get inventoris from database" );
            }
            for( i = 0; i < result.Count; i++ )
            {
                int equip;
                byte index, amount;
                Item item = new Item(
                    (int)result[i]["nameid"],
                    (string)result[i]["creatorName"],
                    (ushort)(short)result[i]["durability"],
                    (byte)result[i]["amount"] );
                equip = (int)(short)result[i]["equip"];
                aChar.inv.AddItem(item, out index, out amount);
                if (equip != -1)
                {
                    aChar.inv.EquipItem(index, Convert.ToByte(equip), out item);
                }
            }
        }

        private void LoadStorage(ref ActorPC aChar)
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  storage  WHERE charID=" + aChar.charID + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't get items from database" + ex.Message);
                throw new Exception("can't get item from database");
            }
            for (i = 0; i < result.Count; i++)
            {
                int equip;
                byte index, amount;
                Item item = new Item(
                    (int)result[i]["nameid"],
                    (string)result[i]["creatorName"],
                    (ushort)(short)result[i]["durability"],
                    (byte)result[i]["amount"]);
                equip = (int)(short)result[i]["equip"];
                aChar.inv.AddItemStorage(item, out index, out amount);               
            }
        }

        private void LoadWeapon( ref ActorPC aChar )
        {
            string sqlstr;
            int i;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  weapon  WHERE charID=" + aChar.charID + ";";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch( Exception ex )
            {
                Logger.ShowError( " can't get weapons from database" + ex.Message, null );
                Logger.ShowError( ex, null );
            }
            aChar.Weapons = new List<Weapon>();
            if( result.Count == 0 )
            {
                Weapon newweapon = new Weapon();
                newweapon.name = aChar.weaponName;
                newweapon.level = 1;
                newweapon.type = (ushort)aChar.weaponType;
                newweapon.augeSkillID = 150001;
                newweapon.U1 = 1;
                newweapon.exp = 0;
                newweapon.durability = 1000;
                newweapon.active = 1;
                newweapon.stones = new uint[6];
                aChar.Weapons.Add( newweapon );
            }
            for( i = 0; i < result.Count; i++ )
            {
                Weapon weapon = new Weapon();
                weapon.stones = new uint[6];
                weapon.name = (string)result[i]["name"];
                weapon.level = (byte)result[i]["level"];
                weapon.type = (byte)result[i]["type"];
                weapon.exp = (uint)(int)result[i]["exp"];
                weapon.augeSkillID = (uint)(int)result[i]["augeSkillID"];
                weapon.durability = (ushort)(short)result[i]["durability"];
                weapon.U1 = (byte)result[i]["U1"];
                weapon.active = (byte)result[i]["active"];
                weapon.stones[0] = (uint)(int)result[i]["slot1"];
                weapon.stones[1] = (uint)(int)result[i]["slot2"];
                weapon.stones[2] = (uint)(int)result[i]["slot3"];
                weapon.stones[3] = (uint)(int)result[i]["slot4"];
                weapon.stones[4] = (uint)(int)result[i]["slot5"];
                weapon.stones[5] = (uint)(int)result[i]["slot6"];
                aChar.Weapons.Add( weapon );
            }

        }

        private void SaveWeapon( ActorPC aChar )
        {
            string sqlstr = "DELETE FROM  weapon  WHERE charID=" + aChar.charID + ";";
            //MySqlHelper.ExecuteNonQuery( db, sqlstr, null );
            if( aChar.Weapons == null )
            {
                Weapon newweapon = new Weapon();
                aChar.Weapons = new List<Weapon>();
                newweapon.name = aChar.weaponName;
                newweapon.level = 1;
                newweapon.type = 1;
                newweapon.augeSkillID = 150001;
                newweapon.U1 = 1;
                newweapon.exp = 0;
                newweapon.durability = 1800;
                newweapon.active = 1;
                newweapon.stones = new uint[6];
                aChar.Weapons.Add( newweapon );
            }
            foreach( Weapon i in aChar.Weapons )
            {
                sqlstr += string.Format( "INSERT INTO  weapon ( charID , name , level , type , augeSkillID , exp , durability , U1 , active , slot1 , slot2 , slot3 , slot4 , slot5 , slot6 ) VALUES({0},N'{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14});",
                aChar.charID, i.name, i.level, i.type, i.augeSkillID, i.exp, i.durability, i.U1, i.active, i.stones[0], i.stones[1], i.stones[2], i.stones[3], i.stones[4], i.stones[5] );
            }
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't insert new weapon in database" + ex.Message);

            }

        }

        public bool CharExists( byte worldID, string name )
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  chardata  WHERE name='" + name + "'";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error: can't get chars from database" + ex.Message );

                throw new Exception( "can't get chars from database" );
            }
            if( result.Count > 0 ) return true;
            return false;
        }

        public uint[] GetCharIDs( int account_id )
        {
            string sqlstr;
            uint[] buf;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM  chardata  WHERE account_id=" + account_id + "";
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error: can't get chars from database" + ex.Message );

                throw new Exception( "can't get chars from database" );
            }
            if( result.Count == 0 ) return null;
            buf = new uint[result.Count];
            for( int i = 0; i < buf.Length; i++ )
            {
                buf[i] = (uint)(int)result[i]["charID"];
            }
            return buf;
        }

        public void SaveNpc( ActorNPC aNpc )
        {

        }

        public void DeleteNpc( ActorNPC aNpc )
        {

        }

        public string GetCharName(uint id)
        {
            string sqlstr = "SELECT * FROM  chardata  WHERE  charID =" + id.ToString() + ";";
            
            DataRowCollection result = db.GetDataTable(sqlstr).Rows;
            if (result.Count == 0)
                return null;
            else
                return (string)result[0]["name"];
          
        }

        public void NewMail(Mail.Mail mail)
        {
            string sqlstr = string.Format("INSERT INTO  mail ( sender , receiver , topic , date ," +
                " content , [read] , valid , zeny , itemID , creator , stack , durability " +
                ") VALUES(N'{0}',N'{1}',N'{2}','{3}',N'{4}',{5},{6},{7},{8},N'{9}',{10},{11});",
                mail.sender, mail.receiver, mail.topic, mail.date.ToString(), mail.content, mail.read.ToString(),
                mail.valid.ToString(), mail.zeny.ToString(), mail.item.ToString(), mail.creator,
                mail.stack.ToString(), mail.durability);
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void SaveMail(Mail.Mail mail)
        {
            string sqlstr = string.Format("UPDATE  mail  SET  sender =N'{0}', receiver =N'{1}', topic =N'{2}'," +
                " date ='{3}', content =N'{4}', [read] ={5}, valid ={6}, zeny ={7}, itemID ={8}," +
                " creator =N'{9}', stack ={10}, durability ={11} WHERE  mailID ={12};",
                mail.sender, mail.receiver, mail.topic, mail.date.ToString(), mail.content,
                mail.read.ToString(), mail.valid.ToString(), mail.zeny.ToString(), mail.item.ToString(),
                mail.creator, mail.stack.ToString(), mail.durability.ToString(), mail.ID.ToString());
            try
            {
                db.ExeSql(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public List<Mail.Mail> GetMail(Mail.SearchType type, string value)
        {
            List<Mail.Mail> list = new List<SagaDB.Mail.Mail>();
            string sqlstr = "";
            DataRowCollection result;
            switch (type)
            {
                case SagaDB.Mail.SearchType.MailID:
                    sqlstr = "SELECT * FROM  mail  WHERE  mailID =" + value + ";";
                    break;
                case SagaDB.Mail.SearchType.Receiver:
                    sqlstr = "SELECT * FROM  mail  WHERE  receiver ='" + value + "';";
                    break;
                case SagaDB.Mail.SearchType.Sender:
                    sqlstr = "SELECT * FROM  mail  WHERE  sender ='" + value + "';";
                    break;                
            }
            try
            {
                result = db.GetDataTable(sqlstr).Rows;
                foreach (DataRow i in result)
                {
                    Mail.Mail mail = new SagaDB.Mail.Mail();
                    mail.ID = (uint)(int)i["mailID"];
                    mail.item = (uint)(int)i["itemID"];
                    mail.read = (byte)i["read"];
                    mail.receiver = (string)i["receiver"];
                    mail.sender = (string)i["sender"];
                    mail.stack = (byte)i["stack"];
                    mail.topic = (string)i["topic"];
                    mail.valid = (byte)i["valid"];
                    mail.zeny = (uint)(int)i["zeny"];
                    mail.content = (string)i["content"];
                    mail.creator = (string)i["creator"];
                    mail.date = DateTime.Parse((string)i["date"]);
                    mail.durability = (ushort)(short)i["durability"];
                    list.Add(mail);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return list;
        }

        public void DeleteMail(uint id)
        {
            string sqlstr = "DELETE FROM  mail  WHERE mailID=" + id.ToString() + ";";
            db.ExeSql(sqlstr);
        }

        public void NewItem(ActorPC pc, Item item)
        {
        }

        public void UpdateItem(ActorPC pc, Item item)
        {
        }

        public void DeleteItem(ActorPC pc, Item item)
        {
        }

        public void NewSkill(ActorPC pc, SkillType type, SkillInfo skill)
        {
        }

        public void UpdateSkill(ActorPC pc, SkillType type, SkillInfo skill)
        {
        }

        public void DeleteSkill(ActorPC pc, SkillInfo skill)
        {
        }

        public void NewQuest(ActorPC pc, Quest.QuestType type, Quest.Quest quest)
        {
        }

        public void UpdateQuest(ActorPC pc, Quest.QuestType type, Quest.Quest quest)
        {
        }

        public void DeleteQuest(ActorPC pc, Quest.Quest quest)
        {
        }

        public void NewJobLevel(ActorPC pc, JobType type, byte level)
        {
        }

        public void UpdateJobLevel(ActorPC pc, JobType type, byte level)
        {
        }

        public void DeleteJobLevel(ActorPC pc, JobType type)
        {
        }

        public void NewMapInfo(ActorPC pc, byte mapID, byte value)
        {
        }

        public void UpdateMapInfo(ActorPC pc, byte mapID, byte value)
        {
        }

        public void DeleteMapInfo(ActorPC pc, byte mapID)
        {
        }

        public void NewStorage(ActorPC pc, Item item)
        {
        }

        public void UpdateStorage(ActorPC pc, Item item)
        {
        }

        public void DeleteStorage(ActorPC pc, Item item)
        {
        }

        public ActorNPC GetNpc( string scriptName )
        {
            return null;

        }

        public void RegisterMarketItem(MarketplaceItem item)
        {
        }

        public void DeleteMarketItem(MarketplaceItem item)
        {
        }

        public List<MarketplaceItem> SearchMarketItem(MarketSearchOption option, ushort pageindex, object vars)
        {
            return null;
        }

        public MarketplaceItem GetMarketItem(uint id)
        {
            return null;
        }
    }

    
    
}
