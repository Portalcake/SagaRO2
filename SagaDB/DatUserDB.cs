using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Db4objects.Db4o;

namespace SagaDB
{
    public class DatUserDB : UserDB
    {
        private UnicodeEncoding encoder = new UnicodeEncoding();
        private MD5 md5 = new MD5CryptoServiceProvider();
        private string dbpath = null;

        public DatUserDB(string dbpath)
        {
            this.dbpath = dbpath;
        }

        public bool Connect()
        {           
            return true;
        }

        public bool isConnected()
        {
            return true;
        }

        public int GetAccountID(string user)
        {
            return -1;
        }        

        public void WriteUser(User user)
        {
            System.IO.FileStream fs = null;
            try
            {
                if (System.IO.Directory.Exists(dbpath + "Save") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save");
                if (System.IO.Directory.Exists(dbpath + "Save/Accounts") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save/Accounts");
                fs = new System.IO.FileStream(dbpath + "Save/Accounts/" + user.Name + ".dat", System.IO.FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter xs = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                xs.Serialize(fs, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: can't Write User in database");
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
                    Console.WriteLine("Error: can't write User in database");
                    throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
                }
            }

        }

        public User GetUser(User user)
        {
            return GetUser(user.Name);
        }

        public User GetUser(string username)
        {
            User result = null;
            System.IO.FileStream fs = null;
            try
            {
                if (System.IO.Directory.Exists(dbpath + "Save") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save");
                if (System.IO.Directory.Exists(dbpath + "Save/Accounts") == false)
                    System.IO.Directory.CreateDirectory(dbpath + "Save/Accounts");
                if (!System.IO.File.Exists(dbpath + "Save/Accounts/" + username + ".dat"))
                    return null;
                fs = new System.IO.FileStream(dbpath + "Save/Accounts/" + username + ".dat", System.IO.FileMode.Open);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter xs = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                result = (User)xs.Deserialize(fs);
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't get User in database");
                return null;
            }
            finally
            {
                try
                {
                    if (fs != null) fs.Close();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: can't get User in database");
                    throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
            return result;
        }

        public bool CheckPassword(string user, string password)
        {
            byte[] unibytes = encoder.GetBytes(password);
            //byte[] hash = md5.ComputeHash(unibytes);
            User tmpUser = GetUser(user);
            if(tmpUser == null) return false;
            return (tmpUser.Password == password);
        }
    }
}
