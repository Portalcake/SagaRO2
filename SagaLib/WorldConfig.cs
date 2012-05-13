using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SagaLib
{
    [Serializable]
    public class WorldConfig
    {
        private Dictionary<int,World> worlds = new Dictionary<int,World>();
        
        public struct World {
            public int ID;
            public string Name;
            public string DBHost;
            public int DBPort;
            public int ifSQL;
            public string DBName;
            public string DBUser;
            public string DBPass;

            public bool IsFilled()
            {
                return (ID != 0 && Name != null && DBHost != null && DBName != null && DBUser != null && DBPass != null);
            }
        }

        public Dictionary<int,World> Worlds { get { return this.worlds; } }

        public WorldConfig()
        {
            //default values

            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader("Config/worlds.config");
                while (reader.Read())
                {
                    XmlNodeType nType = reader.NodeType;
                    if (nType == XmlNodeType.Element && reader.Name == "world")
                    {
                        World newWorld = new World();
                        while (!newWorld.IsFilled())
                        {
                            reader.Read();
                            switch (reader.Name)
                            {
                                case "id": reader.Read(); newWorld.ID = int.Parse(reader.Value); reader.Read(); break;
                                case "name": reader.Read(); newWorld.Name = reader.Value; reader.Read(); break;
                                case "dbhost": reader.Read(); newWorld.DBHost = reader.Value; reader.Read(); break;
                                case "dbport": reader.Read(); newWorld.DBPort = int.Parse(reader.Value); reader.Read(); break;
                                case "ifSQL": reader.Read(); newWorld.ifSQL = int.Parse(reader.Value); reader.Read(); break;
                                case "dbname": reader.Read(); newWorld.DBName = reader.Value; reader.Read(); break;
                                case "dbuser": reader.Read(); newWorld.DBUser = reader.Value; reader.Read(); break;
                                case "dbpass": reader.Read(); newWorld.DBPass = reader.Value; reader.Read(); break;
                            }
                            //reader.Read();
                        }
                        worlds.Add(newWorld.ID, newWorld);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                if (reader != null && reader.ReadState != ReadState.Closed)
                    reader.Close();
            }
        }
    }
}
