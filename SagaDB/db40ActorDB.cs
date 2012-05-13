using System;
using System.Collections.Generic;
using System.Text;

using Db4objects.Db4o;
using SagaDB.Actors;
using SagaDB.Items;

namespace SagaDB
{
    public class db4oCharacterDB : MarshalByRefObject, ActorDB
    {
        private IObjectContainer db;
        private UnicodeEncoding encoder = new UnicodeEncoding();
        private byte worldID;
        private string host;
        private int port;
        private string dbuser;
        private string dbpass;
        private bool isconnected;


        public db4oCharacterDB(string host, int port, string user, string pass)
        {
            this.host = host;
            this.port = port;
            this.dbuser = user;
            this.dbpass = pass;
            this.isconnected = false;
            
            Db4oFactory.Configure().GenerateVersionNumbers(System.Int32.MaxValue);
            Db4oFactory.Configure().GenerateUUIDs(System.Int32.MaxValue);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorPC)).CascadeOnActivate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorPC)).CascadeOnUpdate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorPC)).CascadeOnDelete(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorPC)).StoreTransientFields(false);

            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorNPC)).CascadeOnActivate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorNPC)).CascadeOnUpdate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorNPC)).CascadeOnDelete(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Actors.ActorNPC)).StoreTransientFields(false);

            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Items.Inventory)).CascadeOnActivate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Items.Inventory)).CascadeOnUpdate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Items.Inventory)).CascadeOnDelete(true);

            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Items.Item)).CascadeOnActivate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Items.Item)).CascadeOnUpdate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.Items.Item)).CascadeOnDelete(true);

        }

        public bool Connect()
        {
            if (!this.isconnected)
            {
                try { db.Close(); }
                catch (Exception) { }

                try { db = Db4oFactory.OpenClient(host, port, dbuser, dbpass); }
                catch (Exception) { return false; }

                this.isconnected = true;
            }
            return true;
        }

        public bool isConnected()
        {
            if (this.isconnected)
            {
                try { db.Ext().Version(); }
                catch (Exception)
                {
                    this.isconnected = false;
                }
            }
            return this.isconnected;
        }

        public uint[] GetCharIDs(int account_id)
        {
            return null;
        }

        public void CreateChar(ref ActorPC aChar, int account_id)
        {
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
            
            try
            {
                db.Set(aChar);
                db.Commit();
            }
            catch(Exception) {
                Console.WriteLine("Error: can't create new char in database");
                this.isconnected = false;
                throw new Exception("can't create new char in database");
            }
            finally
            {
                try
                {
                    aChar.charID = (uint)db.Ext().GetObjectInfo(aChar).GetUUID().GetLongPart();
                    db.Set(aChar);
                    db.Commit();

                }

                catch (Exception)
                {
                    Console.WriteLine("Error: can't create new char in database");
                    this.isconnected = false;
                    throw new Exception("can't create new char in database");
                }
            }

        }


        public void SaveChar(ActorPC aChar)
        {
            try
            {
                db.Set(aChar);
                db.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't create new char in database");
                this.isconnected = false;
                throw new Exception("can't create new char in database");
            }
            finally
            {

            }
        }



        public void DeleteChar(ActorPC aChar)
        {
            try
            {
                db.Delete(aChar);
                db.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't delete char in database");
                this.isconnected = false;
                throw new Exception("can't delete char in database");
            }
            finally
            {

            }
        }


        public ActorPC GetChar(uint charID)
        {

            ActorPC result = null;
            try
            {
                IObjectSet queryResult = db.Get(new ActorPC(charID,worldID));
                if (queryResult.Count > 0) result = (ActorPC)queryResult[0];
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't get char from database");
                this.isconnected = false;
                throw new Exception("can't get char from database");
            }
            finally
            {

            }
            if (result.Weapons == null)
            {
                Weapon newweapon = new Weapon();
                result.Weapons = new List<Weapon>();
                newweapon.name = result.weaponName;
                newweapon.level = 1;
                newweapon.type = (ushort)result.weaponType;
                newweapon.augeSkillID = 150001;
                newweapon.U1 = 1;
                newweapon.exp = 0;
                newweapon.durability = 1000;
                newweapon.active = 1;
                result.Weapons.Add(newweapon);
            }
            if (result.ShorcutIDs == null) result.ShorcutIDs = new Dictionary<byte,ActorPC.Shortcut>(); 
            
            return result;
        }

        public bool CharExists(byte worldID, string name)
        {
            IObjectSet queryResult;
            try
            {
                queryResult = db.Get(new ActorPC(worldID, name));
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't get char from database");
                this.isconnected = false;
                throw new Exception("can't get char from database");
            }
            if (queryResult.Count > 0) return true;

            return false;
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
            try
            {
                db.Set(aNpc);
                db.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't create new npc in database");
                this.isconnected = false;
                throw new Exception("can't create new npc in database");
            }
            finally
            {

            }
        }

        public void DeleteNpc(ActorNPC aNpc)
        {
            try
            {
                db.Delete(aNpc);
                db.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't delete npc in database");
                this.isconnected = false;
                throw new Exception("can't delete npc in database");
            }
            finally
            {

            }
        }


        public ActorNPC GetNpc(string scriptName)
        {

            ActorNPC result = null;
            try
            {
                IObjectSet queryResult = db.Get(new ActorNPC(scriptName));
                if (queryResult.Count > 0) result = (ActorNPC)queryResult[0];
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't get npc from database");
                this.isconnected = false;
                throw new Exception("can't get npc from database");
            }
            finally
            {

            }
            return result;
        }

    }
}
