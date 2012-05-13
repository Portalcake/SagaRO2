using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data;

using SagaLib;
using MySql.Data;
using MySql.Data.MySqlClient; 


namespace SagaDB
{
    public class MSSQLUserDB :  UserDB
    {
        private MSSQLOperator db;
        private MSSQLOperator dbinactive;
        private UnicodeEncoding encoder = new UnicodeEncoding();
        private MD5 md5 = new MD5CryptoServiceProvider();
        private string host;
        private int port;
        private string user,database;
        private string pass;
         //private int port;
        private bool isconnected;
        private DateTime tick = DateTime.Now;
        
        public MSSQLUserDB(string host, int port, string database, string user, string pass)
        {
            this.host = host;
            this.port = port;
            this.user = user;
            this.pass = pass;
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
                Logger.ShowSQL(ex,null);
            }
            if (db != null) { if (db.State != ConnectionState.Closed)this.isconnected = true; else { Console.WriteLine("SQL Connection error"); } }

        }

        public bool Connect()
        {
            if (!this.isconnected)
            {
                if (db.State == ConnectionState.Open) { this.isconnected = true; return true; }
                try
                {
                    db.Open();
                }
                catch (Exception) { }
                if (db != null) { if (db.State != ConnectionState.Closed)return true; else return false ;  }
            }
            return true;
        }

        public bool isConnected()
        {
            if (this.isconnected)
            {
                TimeSpan newtime = DateTime.Now - tick;
                if (newtime.TotalMinutes > 5)
                {
                    MSSQLOperator tmp;
                    Logger.ShowSQL("UserDB:Pinging SQL Server to keep the connection alive", null);
                    tmp = dbinactive;
                    if (tmp.State == ConnectionState.Open) tmp.Close();
                    tmp.Open();
                    dbinactive = db;
                    db = tmp;
                    tick = DateTime.Now;
                }
                if (db.State == System.Data.ConnectionState.Broken || db.State == System.Data.ConnectionState.Closed)
                {
                    this.isconnected = false;
                }
            }
            return this.isconnected;
        }        

        public void WriteUser(User user)
        {
            string sqlstr;
            DataRowCollection result = null;
            if (user != null && this.isConnected()==true )
            {
                sqlstr = "SELECT * FROM login WHERE username='" + user.Name + "'";
                try
                {
                    result = db.GetDataTable(sqlstr).Rows;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: can't get user from database");
                    this.isconnected = false;
                    throw new Exception("can't get user from database");
                }
                if (result.Count > 0)
                {
                    sqlstr = string.Format("UPDATE login set password='{0}',sex={1},lastlogin='{2}' WHERE username='{3}'", user.Password, (int)user.Sex, user.lastLogin,user.Name );
                    try
                    {
                        db.ExeSql(sqlstr);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error: can't create new user in database");
                        this.isconnected = false;
                        throw new Exception("can't create new user in database");
                    }
                }
                else
                {
                    sqlstr = string.Format("INSERT INTO login(username,password,sex,lastlogin) VALUES ('{0}','{1}',{2},'{3}')",user.Name, user.Password,(int)user.Sex, user.lastLogin);
                    try
                    {
                        db.ExeSql(sqlstr);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: can't create new user in database:" + ex.Message );
                        throw new Exception("can't create new user in database");
                    }
                }
            }
        }

        public int GetAccountID(string user)
        {
            string sqlstr;
            DataRow result = null;
            if (user != null && this.isConnected() == true)
            {
                sqlstr = "SELECT * FROM login WHERE username='" + user + "'";
                try
                {
                    result = db.GetDataTable(sqlstr).Rows[0];
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: can't get user from database");
                    this.isconnected = false;
                    throw new Exception("can't get user from database");
                }
                return Convert.ToInt32(result["account_id"]);
            }
            return -1;
        }
        private GenderType byte2Gender(byte b)
        {
            switch (b)
            {
                case 2:
                    return GenderType.FEMALE; 
                    
                case 1:
                    return GenderType.MALE;
                    
            }
            return GenderType.FEMALE;
        }

        public User GetUser(User user)
        {
            string sqlstr;
            DataRow result;
            if (user != null && this.isConnected() == true)
            {
                sqlstr = "SELECT * FROM login WHERE username='" + user.Name + "'";
                try
                {
                    result = db.GetDataTable(sqlstr).Rows[0];
                }
                catch (Exception)
                {
                    return null;
                }
                try
                {
                    user.AccountID = Convert.ToInt32(result["account_id"]);
                    user.Password = (string)result["password"];
                    user.Sex = byte2Gender((byte)result["sex"]);
                   
                    user.lastLogin = (string)result["lastlogin"];
                    user.Banned = Convert.ToBoolean(result["Banned"]);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                return user;
            }
            return null;
        }

        public User GetUser(string username)
        {
            User user=new User(username);
            string sqlstr;
            DataRow result;
            if (user != null && this.isConnected() == true)
            {
                sqlstr = "SELECT * FROM login WHERE username = '" + user.Name + "'";
                try
                {
                    result = db.GetDataTable(sqlstr).Rows[0];
                }
                catch (Exception)
                {
                    return null;
                }
                try
                {
                    user.AccountID = Convert.ToInt32(result["account_id"]);
                    user.Password = (string)result["password"];
                    user.Sex = byte2Gender((byte)result["sex"]);
                    user.lastLogin = (string)result["lastlogin"];
                    user.Banned = Convert.ToBoolean(result["Banned"]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                } 
                 
                return user;
            }
            return null;
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
