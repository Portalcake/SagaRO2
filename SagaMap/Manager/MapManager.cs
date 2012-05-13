using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SagaLib;

namespace SagaMap.Manager
{

    public struct MapInfo
    {
        public int id;
        public string name;
        public int CattleyaMapID;
        public float CattleyaX;
        public float CattleyaY;
        public float CattleyaZ;
        public List<HeightMapInfo> heightmaps;
    }

    public struct HeightMapInfo
    {
        public string name;
        public int size;
        public float water_level;
        public int[] scale;
        public float[] location;
    }

    public sealed class MapManager
    {
        private Dictionary<int, Map> maps;
        private Dictionary<int, MapInfo> mapInfo;

        private static XmlParser xml;

        MapManager()
        {
            this.maps = new Dictionary<int, Map>();
            this.mapInfo = new Dictionary<int, MapInfo>();

            try { xml = new XmlParser("DB/MapInfo.xml"); }
            catch (Exception) { Logger.ShowError("cannot read the MapInfo.xml file.", null); return; }

            XmlNodeList XMLitems = xml.Parse("map");
            Logger.ShowInfo("MapInfo database contains " + XMLitems.Count + " maps.", null);

            for (int i = 0; i < XMLitems.Count; i++)
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                List<Dictionary<string, string>> heightmaps = new List<Dictionary<string,string>>();

                XmlNodeList childList = XMLitems.Item(i).ChildNodes;
                for (int j = 0; j < childList.Count; j++)
                {
                    if (childList.Item(j).Name.ToLower() == "heightmap")
                    {
                        Dictionary<string, string> tmp = new Dictionary<string,string>();
                        for (int k = 0; k < childList.Item(j).ChildNodes.Count; k++)
                            tmp.Add(childList.Item(j).ChildNodes.Item(k).Name.ToLower(), childList.Item(j).ChildNodes.Item(k).InnerText);
                        heightmaps.Add(tmp);
                    }
                    else
                        data.Add(childList.Item(j).Name.ToLower(), childList.Item(j).InnerText);

                }
                if (!data.ContainsKey("id")) continue;
                if (!data.ContainsKey("name")) continue;

                try
                {
                    MapInfo info = new MapInfo();
                    info.id = int.Parse(data["id"]);
                    info.name = data["name"];
                    if (data.ContainsKey("cattleyamapid"))
                    {
                        info.CattleyaMapID = int.Parse(data["cattleyamapid"]);
                        info.CattleyaX = float.Parse(data["cattleyax"]);
                        info.CattleyaY = float.Parse(data["cattleyay"]);
                        info.CattleyaZ = float.Parse(data["cattleyaz"]);
                    }

                    info.heightmaps = new List<HeightMapInfo>();

                    foreach(Dictionary<string,string> hmap in heightmaps)
                    {
                        try
                        {
                            HeightMapInfo tmpInf = new HeightMapInfo();

                            tmpInf.name = hmap["name"];
                            tmpInf.size = int.Parse(hmap["size"]);
                            tmpInf.location = new float[3];
                            tmpInf.location[0] = float.Parse(hmap["x"]);
                            tmpInf.location[1] = float.Parse(hmap["y"]);
                            tmpInf.location[2] = float.Parse(hmap["z"]);
                            tmpInf.scale = new int[3];
                            tmpInf.scale[0] = int.Parse(hmap["scale-x"]);
                            tmpInf.scale[1] = int.Parse(hmap["scale-y"]);
                            tmpInf.scale[2] = int.Parse(hmap["scale-z"]);
                            if (hmap.ContainsKey("waterlevel")) tmpInf.water_level = float.Parse(hmap["waterlevel"]);

                            info.heightmaps.Add(tmpInf);
                            
                        }
                        catch (Exception ex) {
                            Logger.ShowError("Cannot read heightmap info", null);
                            Logger.ShowError(ex, null);
                        }
                    }

                    this.mapInfo.Add(info.id, info);
                }
                catch (Exception e) { Logger.ShowError(" cannot parse mapInfo of map: " + data["id"], null); Logger.ShowError(e, null); continue; }
            }
            xml = null;
        }

        public string GetMapName(int mapID)
        {

            if (this.mapInfo.ContainsKey(mapID))
                return this.mapInfo[mapID].name;
            else
                return "MAP_NAME_NOT_FOUND";
        }

        public int GetMapId(string mapName)
        {
            foreach (KeyValuePair<int, MapInfo> kv in mapInfo)
            {
                if (kv.Value.name.ToLower() == mapName.ToLower())//make the map name case insensitive
                    return kv.Key;
            }
            return -1;
        }

        public void LoadMaps(List<int> maps)
        {
            foreach (int mapID in maps)
            {
                if (this.mapInfo.ContainsKey(mapID))
                    if (!this.AddMap(new Map(this.mapInfo[mapID])))
                        Logger.ShowError("Cannot load map " + mapID, null);
            }
        }


        public bool AddMap(Map addMap)
        {
            foreach (Map map in this.maps.Values)
                if (addMap.ID == map.ID) return false;

            this.maps.Add(addMap.ID, addMap);
            return true;
        }

        public bool GetMap(int mapID, out Map getMap)
        {
            if(this.maps.TryGetValue(mapID, out getMap)) return true;
            else
            {
                Logger.ShowDebug("Requesting unknown mapID:" + mapID.ToString(), Logger.defaultlogger);
                return false;
            }
        }

        public static MapManager Instance
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

            internal static readonly MapManager instance = new MapManager();
        }
    }
}
