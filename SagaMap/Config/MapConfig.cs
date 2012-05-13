using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace SagaMap
{
    /// <summary>
    /// The MapConfig reads a config file that specifies all the settings for the mapserver.
    /// Like which ip to listen on, which port, which maps it is hosting etc.
    /// </summary>
    [Serializable]
    public class MapConfig
    {
        private string host;
        private short port;
        private string loginserverhost;
        private int loginserverport;
        private string loginserverpass;
        private string dbhost;
        private int dbport;
        public int LogLevel;
        public int ifSQL;
        private string  name;
        private string dbuser;
        private string dbpass;
        private string worldname;
        private string userdbfile;
        private string chardbfile;
        private List<int> hostedmaps;
        //private int hostedmapCount;

        /// <summary>
        /// Hostname/IP to bind to.
        /// </summary>
        public string Host { get { return this.host; } }

        /// <summary>
        /// Port to listen on.
        /// </summary>
        public string DBName { get { return this.name ; } }

        public int DBPort { get { return this.dbport; } }
        /// <summary>
        /// Address of the login server.
        /// </summary>
        public string LoginServerHost { get { return this.loginserverhost; } }

        /// <summary>
        /// Port number that the login server uses.
        /// </summary>
        public int LoginServerPort { get { return this.loginserverport; } }

        /// <summary>
        /// Password to use with the login server.
        /// </summary>
        public string LoginServerPass { get { return this.loginserverpass; } }

        /// <summary>
        /// The name of the world that this mapserver is hosting.
        /// </summary>
        public string WorldName { get { return this.worldname; } }

        /// <summary>
        /// The id numbers of the maps that his server will host.
        /// </summary>
        public List<int> HostedMaps { get { return this.hostedmaps; } }

        /// <summary>
        /// Deprecated.
        /// </summary>
        public string UserDbFile { get { return this.userdbfile; } }

        /// <summary>
        /// Deprecated.
        /// </summary>
        public string CharDbFile { get { return this.chardbfile; } }

        /// <summary>
        /// Address of the InterServer.
        /// </summary>
        public string DBHost { get { return this.dbhost; } }

        /// <summary>
        /// Port of the InterServer.
        /// </summary>
        public short Port { get { return this.port; } }

        public string DBUser { get { return this.dbuser; } }

        public string DBPass { get { return this.dbpass; } }

        /// <summary>
        /// Upon creation the MapConfig will read the "map.config" file that should be present in the same
        /// dir as the mapserver executable. All attributes will be set during creation.
        /// </summary>
        public MapConfig()
        {
            //default values
            host = "127.0.0.1";
            port = 3011;
            loginserverhost = "127.0.0.1";
            loginserverport = 6000;
            loginserverpass = "secret";
            worldname = "Saga";
            dbhost = "127.0.0.1";
            dbport = 8000;
            LogLevel = 31;
            ifSQL = 0;
            name = "sagamap";
            dbuser = "saga";
            dbpass = "saga";

            hostedmaps = new List<int>();

            try
            {
                XmlTextReader reader = new XmlTextReader(new System.IO.StreamReader("Config/map.config", System.Text.Encoding.Unicode));
                while (reader.Read())
                {
                    XmlNodeType nType = reader.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        { 
                            case "host": reader.Read(); this.host = reader.Value; break;
                            case "port": reader.Read(); this.port = short.Parse(reader.Value); break;
                            case "dbport": reader.Read(); this.dbport = short.Parse(reader.Value); break;
                            case "LogLevel": reader.Read(); this.LogLevel = int.Parse(reader.Value); break;
                            case "ifSQL": reader.Read(); this.ifSQL = short.Parse(reader.Value); break;
                            case "dbhost": reader.Read(); this.dbhost = reader.Value; break;
                            case "dbname": reader.Read(); this.name = reader.Value; break;
                            case "dbuser": reader.Read(); this.dbuser = reader.Value; break;
                            case "dbpass": reader.Read(); this.dbpass = reader.Value; break;
                            case "userdbfile": reader.Read(); this.userdbfile = reader.Value; break;
                            case "chardbfile": reader.Read(); this.chardbfile = reader.Value; break;
                            case "loginserverhost": reader.Read(); this.loginserverhost = reader.Value; break;
                            case "loginserverport": reader.Read(); this.loginserverport = int.Parse(reader.Value); break;
                            case "loginserverpass": reader.Read(); this.loginserverpass = reader.Value; break;
                            case "worldname": reader.Read(); this.worldname = reader.Value; break;
                            case "hostedmap": reader.Read(); this.hostedmaps.Add(int.Parse(reader.Value)); break;
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                SagaLib.Logger.ShowError("Error while reading config file: "+e.Message,null);
                host = "127.0.0.1";
                name = "sagamap";
                loginserverhost = "127.0.0.1";
                loginserverport = 6000;
                loginserverpass = "secret";
                worldname = "Saga";
                hostedmaps = new List<int>();
                for (int i = 1; i < 13; i++)
                    hostedmaps.Add(i);
                SaveConfig();
            }
        }

        /// <summary>
        /// Save the current settings to the "map.config" file.
        /// </summary>
        public void SaveConfig()
        {
            XmlTextWriter writer;
            try { writer = new XmlTextWriter("map.config", null); }
            catch (Exception)
            {
                Console.WriteLine("Error: cannot write to config file");
                return;
            }
            writer.WriteStartDocument();
            writer.WriteStartElement("SagaMap");

            writer.WriteStartElement("host");
            writer.WriteString(this.host);
            writer.WriteEndElement();

            writer.WriteStartElement("port");
            writer.WriteString(this.port.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("loginserverhost");
            writer.WriteString(this.loginserverhost);
            writer.WriteEndElement();

            writer.WriteStartElement("loginserverport");
            writer.WriteString(this.loginserverport.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("loginserverpass");
            writer.WriteString(this.loginserverpass);
            writer.WriteEndElement();

            writer.WriteStartElement("worldname");
            writer.WriteString(this.worldname);
            writer.WriteEndElement();

            for (int i = 0; i < hostedmaps.Count; i++)
            {
                writer.WriteStartElement("hostedmap");
                writer.WriteString(this.hostedmaps[i].ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
