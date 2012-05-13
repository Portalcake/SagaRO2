using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

namespace SagaLogin
{
    public class Config
    {

        private byte _map;
        private float x, y, z;
        private ushort hp, sp;
        private byte str, dex, intel, con;
        private byte register;
        private List<uint> items = new List<uint>();
        private List<uint> skills = new List<uint>();

        public byte Map
        {
            get
            {
                return _map;
            }
        }

        public float X { get { return x; } }

        public float Y { get { return y; } }

        public float Z { get { return z; } }

        public ushort HP { get { return hp; } }

        public ushort SP { get { return sp; } }

        public byte STR { get { return str; } }

        public byte DEX { get { return dex; } }

        public byte INT { get { return intel; } }

        public byte CON { get { return con; } }

        public bool Registration
        {
            get
            {
                if (this.register == 0) return false;
                if (this.register == 1) return true;
                return false;
            }
        }

        public List<uint> Items { get { return items; } }

        public List<uint> Skills { get { return skills; } }

        public void ReadConfig(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(path);
                root = xml["CharConfig"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name)
                    {
                        case "Map":
                            this._map = byte.Parse(i.InnerText);
                            break;
                        case "X":
                            this.x = float.Parse(i.InnerText);
                            break;
                        case "Y":
                            this.y = float.Parse(i.InnerText);
                            break;
                        case "Z":
                            this.z = float.Parse(i.InnerText);
                            break;
                        case "HP":
                            this.hp = ushort.Parse(i.InnerText);
                            break;
                        case "SP":
                            this.sp = ushort.Parse(i.InnerText);
                            break;
                        case "STR":
                            this.str = byte.Parse(i.InnerText);
                            break;
                        case "DEX":
                            this.dex = byte.Parse(i.InnerText);
                            break;
                        case "INT":
                            this.intel = byte.Parse(i.InnerText);
                            break;
                        case "CON":
                            this.con = byte.Parse(i.InnerText);
                            break;
                        case "Item":
                            this.items.Add(uint.Parse(i.InnerText));
                            break;
                        case "Skill":
                            this.skills.Add(uint.Parse(i.InnerText));
                            break;
                        case "REG":
                            this.register = byte.Parse(i.InnerText);
                            break;
                    }
                }
                Logger.ShowInfo("Done reading configuration...", null);
            }
            catch (Exception ex)
            {
                Logger.ShowError("Cannot open the config file:" + path, null);
                if (ex.InnerException != null)
                    Logger.ShowError(ex.InnerException, null);
                else
                    Logger.ShowError(ex, null);
            }
        }

        public static Config Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Config instance = new Config();
        }

    }
}
