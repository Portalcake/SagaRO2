using System;
using System.Collections.Generic;
using System.Text;

using Db4objects.Db4o;
using SagaDB.Actors;
using SagaDB.Items;

namespace SagaDB
{
    public class DatCharacterDB :  ActorDB
    {
        [Serializable]
        private class XMLCharDB
        {
           public uint currentCharID = 0;
           public Dictionary<uint, string> Chars = new Dictionary<uint, string>();
           public List<string> CharNames = new List<string>();
        }
        private UnicodeEncoding encoder = new UnicodeEncoding();
        private string worldID,dbpath;
        private bool isconnected = true;
        private XMLCharDB myDB = new XMLCharDB();

        public DatCharacterDB(string worldName,string dbpath)
        {
            this.worldID = worldName;
            this.dbpath = dbpath;
            Load();
        }

        private void Load()
        {
            if (System.IO.Directory.Exists(dbpath + "Save") == false)
                System.IO.Directory.CreateDirectory(dbpath + "Save");
            if (System.IO.File.Exists(dbpath + "Save/world" + this.worldID + ".sav") == false)
            {
                this.myDB = new XMLCharDB();
                return;
            }
            System.IO.FileStream fs = new System.IO.FileStream(dbpath + "Save/world" + this.worldID + ".sav", System.IO.FileMode.Open);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                this.myDB = (XMLCharDB)format.Deserialize(fs);
            }
            catch (Exception)
            {
            //    this.myDB = new XMLCharDB();
            }
            fs.Close();
        }

        private void Save()
        {
            if (System.IO.Directory.Exists(dbpath + "Save") == false)
                System.IO.Directory.CreateDirectory(dbpath + "Save");
            System.IO.FileStream fs = new System.IO.FileStream(dbpath + "Save/world" + this.worldID + ".sav", System.IO.FileMode.Create);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter format = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                format.Serialize(fs, this.myDB);
            }
            catch (Exception)
            {
            }
            fs.Close();
        }

        public bool Connect()
        {            
            return true;
        }

        public bool isConnected()
        {            
            return this.isconnected;
        }

        public uint[] GetCharIDs(int account_id)
        {
            return null;
        }

        public void CreateChar(ref ActorPC aChar, int account_id)
        {
            Load();
            if (aChar.Weapons == null)
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
                aChar.Weapons.Add(newweapon);
            }
            if(aChar.ShorcutIDs == null) aChar.ShorcutIDs = new Dictionary<byte, ActorPC.Shortcut>();
            if(aChar.BattleSkills==null) aChar.BattleSkills = new Dictionary<uint,SkillInfo>();
            if(aChar.LivingSkills==null)aChar.LivingSkills =new Dictionary<uint,SkillInfo>();
            if (aChar.SpecialSkills == null) aChar.SpecialSkills = new Dictionary<uint, SkillInfo>();
            if (aChar.InactiveSkills == null) aChar.InactiveSkills =new Dictionary<uint,SkillInfo>();
            if (aChar.Tasks == null) aChar.Tasks = new Dictionary<string, SagaLib.MultiRunTask>();
            if (aChar.JobLevels == null) aChar.JobLevels = new Dictionary<JobType, byte>();
            aChar.QuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            aChar.PersonalQuestTable = new Dictionary<uint, SagaDB.Quest.Quest>();
            aChar.MapInfo = new Dictionary<byte, byte>();
           
            System.IO.FileStream fs = null;
            try
            {
                if (System.IO.Directory.Exists(dbpath + "Save") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save");
                if (System.IO.Directory.Exists(dbpath + "Save/" + aChar.userName) == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save/" + aChar.userName);
                fs = new System.IO.FileStream(dbpath + "Save/" + aChar.userName + "/" + aChar.name + ".dat", System.IO.FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter xs = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                xs.Serialize(fs, aChar);
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: can't create new char in database");
                throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {

                try
                {
                    this.myDB.CharNames.Add(aChar.name);
                    aChar.charID = this.myDB.currentCharID;
                    this.myDB.Chars.Add(aChar.charID, aChar.userName + "," + aChar.name);
                    this.myDB.currentCharID++;
                    if (fs != null) fs.Close();
                    Save();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: can't create new char in database");
                    throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
                }
            }

        }


        public void SaveChar(ActorPC aChar)
        {
            Load();
            System.IO.FileStream fs = null;
            try
            {
                if (System.IO.Directory.Exists(dbpath + "Save") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save");
                if (System.IO.Directory.Exists(dbpath + "Save/" + aChar.userName) == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save/" + aChar.userName);
                fs = new System.IO.FileStream(dbpath + "Save/" + aChar.userName + "/" + aChar.name + ".dat", System.IO.FileMode.Create);
                //ignore some fields
                ActorEventHandler tmp = aChar.e;
                Dictionary<string, SagaLib.MultiRunTask> tmp2 = aChar.Tasks;
                Actor tmp3 = aChar.CurTarget;
                Actor tmp4 = aChar.LastMissionBoard;
                BattleStatus bs = aChar.BattleStatus;
                aChar.BattleStatus = null;
                aChar.e = null;
                aChar.Tasks = null;
                aChar.CurTarget = null;
                aChar.LastMissionBoard = null;
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter xs = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                xs.Serialize(fs, aChar);
                aChar.e = tmp;
                aChar.Tasks = tmp2;
                aChar.LastMissionBoard = (ActorItem)tmp4;
                aChar.CurTarget = tmp3;
                aChar.BattleStatus = bs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't save char in database");
                throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                try
                {
                    if (fs != null) fs.Close();                    
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: can't create new char in database");
                    throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
                }
            }

        }



        public void DeleteChar(ActorPC aChar)
        {
            Load();
            try
            {
                if (System.IO.Directory.Exists(dbpath + "Save") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save");
                if (System.IO.Directory.Exists(dbpath + "Save/" + aChar.userName) == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save/" + aChar.userName);
                if (System.IO.File.Exists(dbpath + "Save/" + aChar.userName + "/" + aChar.name + ".dat"))
                    System.IO.File.Delete(dbpath + "Save/" + aChar.userName + "/" + aChar.name + ".dat");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't delete char in database");
                throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                try
                {
                    this.myDB.Chars.Remove(aChar.charID);
                    this.myDB.CharNames.Remove(aChar.name);
                    Save();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }


        public ActorPC GetChar(uint charID)
        {
            ActorPC result = null;
            Load();
            System.IO.FileStream fs = null;
            try
            {
                string username, name;
                string[] tmp;
                if(!this.myDB.Chars.ContainsKey(charID))
                    return null;
                tmp = this.myDB.Chars[charID].Split(',');
                username = tmp[0];
                name = tmp[1];
                if (System.IO.Directory.Exists(dbpath + "Save") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save");
                if (System.IO.Directory.Exists(dbpath + "Save/" + username) == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save/" + username);
                if (!System.IO.File.Exists(dbpath + "Save/" + username + "/" + name + ".dat"))
                {
                    this.myDB.Chars.Remove(charID);
                    Save();
                    return null;
                }
                fs = new System.IO.FileStream(dbpath + "Save/" + username + "/" + name + ".dat", System.IO.FileMode.Open);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter xs = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                result = (ActorPC)xs.Deserialize(fs);
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't load char in database");
                return null;
            }
            finally
            {
                try
                {
                    if (fs != null) fs.Close();
                    if (result.JobLevels == null) result.JobLevels = new Dictionary<JobType, byte>();            
                    if (result.MapInfo == null) result.MapInfo = new Dictionary<byte, byte>();
                    if (result.BattleStatus == null) result.BattleStatus = new BattleStatus();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: can't create load char in database");
                    throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
            if (result.ShorcutIDs == null) result.ShorcutIDs = new Dictionary<byte,ActorPC.Shortcut>(); 
            
            return result;
        }

        public bool CharExists(byte worldID, string name)
        {
            return this.myDB.CharNames.Contains(name);
        }

        public string GetCharName(uint id)
        {
            return null;
        }

        public void NewMail(Mail.Mail mail)
        {

        }

        public void SaveMail(Mail.Mail mail)
        {
        }

        public List<Mail.Mail> GetMail(Mail.SearchType type, string value)
        {
            return null;
        }

        public void DeleteMail(uint id)
        {
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
        
        public void SaveNpc(ActorNPC aNpc)
        {
            return;
        }

        public void DeleteNpc(ActorNPC aNpc)
        {
            return;
        }


        public ActorNPC GetNpc(string scriptName)
        {
            return null;
        }

    }
}
