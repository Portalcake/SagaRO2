using System;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using SagaLib;

namespace SagaGateway
{
    [Serializable]
    public class GatewayConfig
    {
        private string host;
        private int port;
        private string loginserver;
        private int loginport;
        private byte connection;
        private string crc_key;
        private List<string> allowed_crc;
        private List<string> banned_ip;
        public int LogLevel;
         
        public string Host { get { return this.host; } }

        public int Port { get { return this.port; } }

        public string LoginServer { get { return this.loginserver; } }

        public int LoginPort { get { return this.loginport; } }

        public string CRC_Key { get { return this.crc_key; } }

        public byte Conncetions { get { return this.connection; } }

        public List<string> Allowed_CRC { get { return this.allowed_crc; } }

        public List<string> Banned_IP { get { return this.banned_ip; } }

        public GatewayConfig()
        {
            //default values
            host = "127.0.0.1";
            port = 65000;
            loginserver = "127.0.0.1";
            loginport = 6000;
            connection = 5;
            crc_key = "A928CDC9DBE8751B3BC99EB65AE07E0C849CE739";
            allowed_crc = new List<string>();
            banned_ip = new List<string>();
            LogLevel = 31;
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load("Config/gateway.config");
                root = xml["SagaGateway"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    XmlNodeList child;                            
                    switch (i.Name.ToLower())
                    {
                        case "host": this.host = i.InnerText; break;
                        case "port": this.port = int.Parse(i.InnerText); break;
                        case "loginserver": this.loginserver = i.InnerText; break;
                        case "loginport": this.loginport = int.Parse(i.InnerText); break;
                        case "connections": this.connection = byte.Parse(i.InnerText); break;
                        case "loglevel": this.LogLevel = int.Parse(i.InnerText); break;
                        case "banned_ip" :
                            child = i.ChildNodes;
                            foreach (object l in child)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                if (k.Name.ToLower() != "ip") continue;
                                this.banned_ip.Add(k.InnerText);
                            }
                            break;
                        case "crc_key":
                            if (i.InnerText.Length != 40)
                            {
                                Logger.ShowError("Invalid CRC_KEY(" + i.InnerText + ") length! Using default key");
                                crc_key = "A928CDC9DBE8751B3BC99EB65AE07E0C849CE739";
                            }
                            else
                            {
                                try
                                {
                                    //try to parse
                                    Conversions.HexStr2Bytes(i.InnerText);
                                    crc_key = i.InnerText;
                                }
                                catch
                                {
                                    Logger.ShowError("Invalid CRC_Key(" + i.InnerText + ")! Using default key");
                                    crc_key = "A928CDC9DBE8751B3BC99EB65AE07E0C849CE739";                            
                                }
                            }
                            break;
                        case "allowed_crc":
                            child = i.ChildNodes;
                            foreach (object l in child)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                if (k.Name.ToLower() != "crc") continue;
                                if (k.InnerText.Length != 40)
                                {
                                    Logger.ShowError("Invalid CRC_KEY length(" + k.InnerText + ")! Discarding");                                    
                                }
                                else
                                {
                                    try
                                    {
                                        //try to parse
                                        Conversions.HexStr2Bytes(k.InnerText);
                                        allowed_crc.Add(k.InnerText);
                                    }
                                    catch
                                    {
                                        Logger.ShowError("Invalid CRC_Key(" + k.InnerText + ")! Discarding");                                      
                                    }
                                }
                            }
                            break;
                    }
                }               
            }
            catch (Exception)
            {
                host = "127.0.0.1";
                port = 65000;
                loginserver = "127.0.0.1";
                loginport = 6000;
                crc_key = "A928CDC9DBE8751B3BC99EB65AE07E0C849CE739";
                allowed_crc = new List<string>();
            
            }

        }
       
    }
}
