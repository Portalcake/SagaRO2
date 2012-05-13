using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

namespace SagaMap
{
    public class Config
    {

        private uint exp, drop;
        private bool loggm, gmtrade;
        private List<string> motd;

        public uint EXPRate { get { return exp; } }

        public uint DropRate { get { return exp; } }

        public List<string> MessageOfTheDay { get { return motd; } }

        public bool LogGMCommands { get { return this.loggm; } }

        public bool AllowGMTrade { get { return this.gmtrade; } }

        private string currentPath;

        public void ReloadConfig()
        {
            ReadConfig(currentPath);
        }

        public void ReadConfig(string path)
        {
            XmlDocument xml=new XmlDocument() ;
            try
            {
                XmlElement root;
                XmlNodeList list;
                currentPath = path;
                xml.Load(path);
                root = xml["MapConfig"];
                list = root.ChildNodes;
                motd = new List<string>();
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name)
                    {
                        case "EXPRate":
                            exp = uint.Parse(i.InnerText);
                            break;
                        case "DropRate":
                            drop = uint.Parse(i.InnerText);
                            break;
                        case "Motd":
                            string[] tmp;
                            tmp = i.InnerText.Split("\n".ToCharArray());
                            for (int k = 0; k < tmp.Length; k++)
                            {
                                if (tmp[k].Substring(tmp[k].Length - 1) == "\r")
                                    tmp[k] = tmp[k].Substring(0, tmp[k].Length - 1);                                
                                if (tmp[k] != "")
                                {
                                    while (tmp[k].Length>1 && tmp[k].Substring(0, 1) == " ")
                                    {
                                        tmp[k] = tmp[k].Substring(1, tmp[k].Length - 1);
                                    }
                                    if (tmp[k] == " ") continue;
                                    motd.Add(tmp[k]);
                                }
                            }
                            break;
                        case "LogGMCommands":
                            loggm = Convert.ToBoolean(Byte.Parse(i.InnerText));
                            break;
                        case "AllowGMTrade":
                            gmtrade = Convert.ToBoolean(Byte.Parse(i.InnerText));
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
