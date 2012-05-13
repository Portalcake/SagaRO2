using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace SagaLogin
{
    [Serializable]
    public class LoginConfig
    {
        private string host;
        private int port;
        private string dbhost;
        private int dbport;
        private string dbname;
        private string dbuser;
        private string dbpass;
        private string userdbfile;
        private string chardbfile;
        private string mapserverpass;

        public int LogLevel;
        public int ifSQL;

        public string Host { get { return this.host; } }

        public int Port { get { return this.port; } }

        public string UserDbFile { get { return this.userdbfile; } }

        public string CharDbFile { get { return this.chardbfile; } }

        public string MapServerPass { get { return this.mapserverpass; } }

        public string DBHost { get { return this.dbhost; } }

        public string DBName { get { return this.dbname; } }

        public int DBPort { get { return this.dbport; } }

        public string DBUser { get { return this.dbuser; } }

        public string DBPass { get { return this.dbpass; } }


        public LoginConfig()
        {
            //default values
            host = "127.0.0.1";
            port = 6000;
            userdbfile = "user.db";
            chardbfile = "char.db";
            dbhost = "127.0.0.1";
            dbport = 8000;
            LogLevel = 31;
            ifSQL = 0;
            dbname = "sagalogin";
            dbuser = "saga";
            dbpass = "saga";
            mapserverpass = "secret";

            try
            {
                XmlTextReader reader = new XmlTextReader("Config/login.config");
                while (reader.Read())
                {
                    XmlNodeType nType = reader.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "host": reader.Read(); this.host = reader.Value; break;
                            case "port": reader.Read(); this.port = int.Parse(reader.Value); break;
                            case "dbhost": reader.Read(); this.dbhost = reader.Value; break;
                            case "dbname": reader.Read(); this.dbname = reader.Value; break;
                            case "dbport": reader.Read(); this.dbport = int.Parse(reader.Value); break;
                            case "LogLevel": reader.Read(); this.LogLevel = int.Parse(reader.Value); break;
                            case "ifSQL": reader.Read(); this.ifSQL = int.Parse(reader.Value); break;
                            case "dbuser": reader.Read(); this.dbuser = reader.Value; break;
                            case "dbpass": reader.Read(); this.dbpass = reader.Value; break;
                            case "userdbfile": reader.Read(); this.userdbfile = reader.Value; break;
                            case "chardbfile": reader.Read(); this.chardbfile = reader.Value; break;
                            case "mapserverpass": reader.Read(); this.mapserverpass = reader.Value; break;
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                host = "127.0.0.1";
                port = 6000;
                userdbfile = "user.db";
                userdbfile = "char.db";
                mapserverpass = "secret";;
                SaveConfig();
            }

        }

        public void SaveConfig()
        {
            XmlTextWriter writer;
            try { writer = new XmlTextWriter("login.config", null); }
            catch (Exception)
            {
                Console.WriteLine("Error: cannot write to config file");
                return;
            }
            writer.WriteStartDocument();
            writer.WriteStartElement("SagaLogin");
            writer.WriteStartElement("host");
            writer.WriteString(this.host);
            writer.WriteEndElement();
            writer.WriteStartElement("port");
            writer.WriteString(this.port.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("userdbfile");
            writer.WriteString(this.userdbfile.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("chardbfile");
            writer.WriteString(this.chardbfile.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("mapserverpass");
            writer.WriteString(this.mapserverpass.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
