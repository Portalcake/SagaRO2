using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using Db4objects.Db4o;

namespace SagaDB
{
    public class db4oUserDB : MarshalByRefObject, UserDB

    {
        private IObjectContainer db;
        private UnicodeEncoding encoder = new UnicodeEncoding();
        private MD5 md5 = new MD5CryptoServiceProvider();
        private string host;
        private string user;
        private string pass;
        private int port;
        private bool isconnected;

        public db4oUserDB(string host, int port, string user, string pass)
        {
            this.host = host;
            this.port = port;
            this.user = user;
            this.pass = pass;
            this.isconnected = false;
            
            Db4oFactory.Configure().GenerateVersionNumbers(System.Int32.MaxValue);
            Db4oFactory.Configure().GenerateUUIDs(System.Int32.MaxValue);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.User)).GenerateVersionNumbers(false);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.User)).GenerateUUIDs(false);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.User)).CascadeOnActivate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.User)).CascadeOnUpdate(true);
            Db4oFactory.Configure().ObjectClass(typeof(SagaDB.User)).CascadeOnDelete(true);

        }
       
        public bool Connect()
        {
            if (!this.isconnected)
            {
                try { db.Close(); }
                catch (Exception) { }

                try { db = Db4oFactory.OpenClient(host, port, user, pass); }
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

        public int GetAccountID(string user)
        {
            return -1;
        }
        

        public void WriteUser(User user)
        {

            try
            {
                db.Set(user);
                db.Commit();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't write user to database");
                this.isconnected = false;
                throw new Exception("can't write user to database");
            }
            finally
            {

            }
        }

        public User GetUser(User user)
        {
            
            User result = null;

            try
            {
                IObjectSet queryResult = db.Get(user);
                if (queryResult.Count > 0)
                {
                    result = (User)queryResult[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't get user from database");
                this.isconnected = false;
                throw new Exception("can't get user from database");
            }
            finally
            {

            }

            return result;
        }

        public User GetUser(string username)
        {

            User result = null;
            try
            {
                User tmpUser = new User(username);

                IObjectSet queryResult = db.Get(tmpUser);

                if (queryResult.Count > 0)
                    result = (User)queryResult[0];
            }
            catch (Exception)
            {
                Console.WriteLine("Error: can't get user from database");
                this.isconnected = false;
                throw new Exception("can't get user from database");
            }
            finally
            {

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
