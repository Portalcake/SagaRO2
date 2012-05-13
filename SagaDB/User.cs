using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB
{
    [Serializable]
    public enum GenderType { MALE=1, FEMALE=2 };

    [Serializable]
    public class User
    {
        private int account_id=-1;
        private string name;
        private string password;
        private GenderType sex;
        private string lastlogin;
        private bool banned;

        public Dictionary<byte,List<uint>> chars;

        public string Name { get { return this.name; } }
        public string Password { get { return this.password; } set { this.password = value; } }
        public GenderType Sex { get { return this.sex; } set { this.sex = value; } }
        public string lastLogin { get { return this.lastlogin; } set { this.lastlogin = value;  } }
        public int AccountID { get { return this.account_id; } set { this.account_id = value; } }
        public bool Banned { get { return this.banned; } set { this.banned = value; } }

        public User(string name, string password, GenderType sex,string lastlogin)
        {
            this.name = name;
            this.password = password;
            this.sex = sex;
            this.lastLogin = lastLogin;
            this.chars = new Dictionary<byte, List<uint>>();
        }

        public User(string name, string password, GenderType sex)
        {
            this.name = name;
            this.password = password;
            this.sex = sex;
            this.chars = new Dictionary<byte, List<uint>>();
        }

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
            this.sex = GenderType.MALE;
            this.chars = new Dictionary<byte, List<uint>>();
        }

        public User(string name)
        {
            this.name = name;
            this.chars = new Dictionary<byte, List<uint>>();
        }

        public User()
        {
        }
    }
}
