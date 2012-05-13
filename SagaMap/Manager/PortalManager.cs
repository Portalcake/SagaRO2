using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SagaLib;

namespace SagaMap.Manager
{

    public static class PortalManager
    {
        public struct PortalInfo
        {
            public int m_mapID;
            public float m_x, m_y, m_z;
            public PortalInfo(int MapID, float x, float y, float z)
            {
                this.m_mapID = MapID;
                this.m_x = x;
                this.m_y = y;
                this.m_z = z;
            }
        }
 
        private static XmlParser xml;
        private static Dictionary<byte,Dictionary<byte, PortalInfo>> portals;

        public static void Start(string configFile)
        {
            portals = new Dictionary<byte,Dictionary<byte, PortalInfo>>();

            try { xml = new XmlParser(configFile); }
            catch (Exception) { Logger.ShowError(" cannot read the portal database file.",null); return; }

            XmlNodeList XMLitems = xml.Parse("portal");
            Logger.ShowInfo("Portal database contains " + XMLitems.Count + " portals.",null);

            for (int i = 0; i < XMLitems.Count; i++)
                AddPortal(XMLitems.Item(i));
            xml = null;
        }

        private static void AddPortal(XmlNode portal)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            XmlNodeList childList = portal.ChildNodes;
            for (int i = 0; i < childList.Count; i++)
                data.Add(childList.Item(i).Name, childList.Item(i).InnerText);

            if (!data.ContainsKey("toid")) return;
            try
            {
                Dictionary<byte,PortalInfo> tmpdic;
                System.Globalization.CultureInfo culture;
                culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                PortalInfo nPortal = new PortalInfo(int.Parse(data["toid"]), float.Parse(data["x"],culture), float.Parse(data["y"],culture), float.Parse(data["z"],culture));
                if (data.ContainsKey("mapid"))
                    nPortal.m_mapID = byte.Parse(data["mapid"]); 
                if (!portals.ContainsKey(byte.Parse(data["toid"])))
                {
                    tmpdic = new Dictionary<byte, PortalInfo>();
                    tmpdic.Add(byte.Parse(data["fromid"]), nPortal);
                    portals.Add(byte.Parse(data["toid"]), tmpdic);
                }
                else
                {
                    tmpdic = portals[byte.Parse(data["toid"])];
                    tmpdic.Add(byte.Parse(data["fromid"]), nPortal);
                }
                
            }
            catch (Exception e) { Logger.ShowError("cannot parse: " + data["toid"],null); Logger.ShowError(e,null); return; }

        }

        public static PortalInfo GetPortal(byte toid,byte fromid)
        {
            Dictionary<byte, PortalInfo> tmpdic;
            if (!portals.ContainsKey(toid)) return new PortalInfo(-1,0f,0f,0f);
            tmpdic = portals[toid];
            if (!tmpdic.ContainsKey(fromid)) return new PortalInfo(-1, 0f, 0f, 0f);
            return tmpdic[fromid];
        }

    }
}
