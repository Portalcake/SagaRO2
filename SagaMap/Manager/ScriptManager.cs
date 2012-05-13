using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using System.Xml;
using Microsoft.CSharp;

using SagaDB.Actors;
using SagaMap.ActorEventHandlers;
using SagaLib;
using SagaMap.Tasks;
using SagaMap.Scripting;

namespace SagaMap.Manager
{
    public class SpawnTemplate
    {
        public uint typeID=10069;
        public string name="Poring";
        public int delay=-1;
        public int AIMode=0;
        public List<Dictionary<string, string>> lootItems = new List<Dictionary<string, string>>();
    }

    public class ScriptManager
    {
        public Dictionary<string, Npc> scripts = new Dictionary<string, Npc>();
        private int spawnCount = 0;
        private SpawnMobs spawnTask;
        public Scenario Scenario;
        public Dictionary<uint, SpawnTemplate> templates = new Dictionary<uint, SpawnTemplate>();

        public ScriptManager()
        {
            Logger.ShowInfo("Loading spawn templates", null);
            LoadSpawnTemplates("DB/spawntemplates.xml");
            Logger.ShowInfo("Loading mob spawns", null);
            LoadSpawnFile("DB/Spawns");

            Logger.ShowInfo("Loading uncompiled scripts",null);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            int npccount = 0;
            int mapitemcount = 0;
            try
            {
                try
                {
                    Assembly newAssembly = CompileScript(Directory.GetFiles("scripts/npcs", "*.cs", SearchOption.AllDirectories), provider);
                    int[] tmp;
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        mapitemcount += tmp[0];
                        npccount += tmp[1];
                    }
                    newAssembly = CompileScript(Directory.GetFiles("scripts/Scenario", "*.cs", SearchOption.AllDirectories), provider);
                    Module[] newScripts = newAssembly.GetModules();
                    foreach (Module newScript in newScripts)
                    {
                        Type[] types = newScript.GetTypes();
                        foreach (Type npcType in types)
                        {
                            try
                            {
                                this.Scenario = (SagaMap.Scenario)newAssembly.CreateInstance(npcType.Name);
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex, null);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex, null);
                }
                       

            }
            catch (Exception) { }
            Logger.ShowInfo("Loading compiled scripts",null);
            try
            {
                foreach (string s in Directory.GetFiles("cscripts"))
                {
                    try
                    {
                        int[] tmp;
                        Assembly newAssembly = Assembly.LoadFrom(s);
                        tmp = LoadAssembly(newAssembly);
                        mapitemcount += tmp[0];
                        npccount += tmp[1];
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
            Logger.ShowInfo(string.Format("{0} NPCs and {1} MapItems Added", npccount, mapitemcount), null);
            this.spawnTask = new SpawnMobs(this);
            this.spawnTask.Activate();
        }

        public void ReloadScript(MapClient client)
        {
            //Get all NPCs
            List<string> tmp2 = new List<string>();
            foreach (string i in this.scripts.Keys)
            {
                Npc npc = this.scripts[i];
                if (npc.isItem == false)
                {
                    if (npc.Actor.npcType < 10000 || npc.Actor.npcType >50000)
                    {
                        tmp2.Add(i);
                        npc.Map.DeleteActor(npc.Actor);
                    }
                }
                else
                {
                    MapItem item = (MapItem)npc;
                    item.Map.DeleteActor(item.ActorI);
                    tmp2.Add(i);
                }
            }
            foreach (string i in tmp2)
            {
                this.scripts[i].Dispose();
                this.scripts.Remove(i);
            }

            Quest.QuestsManager.QuestItem = new Dictionary<uint, Dictionary<uint, List<SagaMap.Quest.QuestsManager.questI>>>();
            Quest.QuestsManager.MobQuestItem = new Dictionary<uint, List<SagaMap.Quest.QuestsManager.LootInfo>>();
            Quest.QuestsManager.Enemys = new Dictionary<uint, Dictionary<uint, List<SagaMap.Quest.QuestsManager.EnemyInfo>>>();
            Quest.QuestsManager.WayPoints = new Dictionary<uint, Dictionary<uint, List<SagaMap.Quest.QuestsManager.WayPointInfo>>>();
            Logger.ShowInfo("Loading uncompiled scripts", null);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            int npccount = 0;
            int mapitemcount = 0;
            try
            {
                try
                {
                    Assembly newAssembly = CompileScript(Directory.GetFiles("scripts/npcs", "*.cs", SearchOption.AllDirectories), provider, client);
                    int[] tmp;
                    if (newAssembly != null)
                    {
                        tmp = LoadAssembly(newAssembly);
                        mapitemcount += tmp[0];
                        npccount += tmp[1];
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex, null);
                }

            }
            catch (Exception) { }
            Logger.ShowInfo("Loading compiled scripts", null);
            try
            {
                foreach (string s in Directory.GetFiles("cscripts"))
                {
                    try
                    {
                        int[] tmp;
                        Assembly newAssembly = Assembly.LoadFrom(s);
                        tmp = LoadAssembly(newAssembly);
                        mapitemcount += tmp[0];
                        npccount += tmp[1];
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
            Logger.ShowInfo(string.Format("Reloaded {0} NPCs and {1} MapItems", npccount, mapitemcount), null);            
        }

        private void LoadSpawnTemplates(string file)
        {
            XmlParser xml;
            try { xml = new XmlParser(file); }
            catch (Exception ex) { Logger.ShowError(ex, null); return; }

            XmlNodeList XMLitems = xml.Parse("template");
            Logger.ShowInfo("SpawnTemplate database contains " + XMLitems.Count + " templates.", null);

            for (int i = 0; i < XMLitems.Count; i++)
                this.AddSpawnTemplate(XMLitems.Item(i));
            xml = null;
        }

        private void AddSpawnTemplate(XmlNode template)
        {
            XmlNodeList spawnParamList = template.ChildNodes;
            Dictionary<string, string> data = new Dictionary<string, string>();
            List<Dictionary<string, string>> lootItems = new List<Dictionary<string, string>>();
            //int lootCount = 0;

            foreach (XmlNode spawnParam in spawnParamList)
            {
                if (spawnParam.Name.ToLower() == "loot")
                {
                    Dictionary<string, string> loot = new Dictionary<string, string>();
                    foreach (XmlNode lootParam in spawnParam.ChildNodes)
                        loot.Add(lootParam.Name.ToLower(), lootParam.InnerText);
                    lootItems.Add(loot);
                }
                else
                    data.Add(spawnParam.Name.ToLower(), spawnParam.InnerText);
            }
            SpawnTemplate st = new SpawnTemplate();
            st.typeID = uint.Parse(data["id"]);
            st.name = data["name"];
            st.delay = int.Parse(data["delay"]);
            st.AIMode = int.Parse(data["aimode"]);
            st.lootItems = lootItems;
            templates.Add(st.typeID, st);
        }

        private void LoadSpawnFile(string path)
        {
            XmlParser xml;
            int count = 0;
            string[] file = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            for (int j = 0; j < file.Length; j++)
            {
                try { xml = new XmlParser(file[j]); }
                catch (Exception ex) { Logger.ShowError(ex, null); return; }

                XmlNodeList XMLitems = xml.Parse("spawn");

                for (int i = 0; i < XMLitems.Count; i++)
                    count += this.AddSpawn(XMLitems.Item(i));
                xml = null;
            }
            Logger.ShowInfo("Spawn database contains " + count + " entries.", null);         
        }

        private int AddSpawn(XmlNode spawn)
        {
            XmlNodeList spawnParamList = spawn.ChildNodes;
            Dictionary<string, string> data = new Dictionary<string, string>();
            List<Dictionary<string, string>> lootItems = new List<Dictionary<string, string>>();
            //int lootCount = 0;
            int count = 0;

            foreach (XmlNode spawnParam in spawnParamList)
            {
                if (spawnParam.Name == "loot")
                {
                    Dictionary<string, string> loot = new Dictionary<string, string>();
                    foreach (XmlNode lootParam in spawnParam.ChildNodes)
                        loot.Add(lootParam.Name, lootParam.InnerText);
                    lootItems.Add(loot);
                }
                else
                    data.Add(spawnParam.Name, spawnParam.InnerText);
            }
            for (int j = 0; j < int.Parse(data["amount"]); j++)
            {
                try
                {
                    Mob newMob;
                    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    ActorNPC newActor = new ActorNPC(uint.Parse(data["id"]), 100, 100, 100, 100);
                    SpawnTemplate template = new SpawnTemplate();
                    newMob = MobFactory.GetMob(uint.Parse(data["id"]), ref newActor);//Load Mob Infos from DB
                    if (templates.ContainsKey(newMob.Type))
                        template = templates[newMob.Type];
                    newMob.MapName = data["map"];
                    newActor.mapID = (byte)MapManager.Instance.GetMapId(newMob.MapName);

                    if (data.ContainsKey("name"))
                        newMob.Name = data["name"];
                    newActor.name = newMob.Name;

                    if (data.ContainsKey("delay"))
                        newMob.Delay = int.Parse(data["delay"]);
                    else
                        newMob.Delay = template.delay;
                    newMob.AIMode = template.AIMode;
                    Map map;
                    MapManager.Instance.GetMap(MapManager.Instance.GetMapId(newMob.MapName), out map);
                    float[] pos;
                    pos = GetRandomPos(map, float.Parse(data["x"], culture), float.Parse(data["y"], culture), float.Parse(data["range"], culture));
                    if (!map.HasHeightMap() && data.ContainsKey("z")) pos[2] = float.Parse(data["z"], culture);
                    newMob.StartX = float.Parse(data["x"], culture);
                    newMob.StartY = float.Parse(data["y"], culture);
                    if(data.ContainsKey("z"))
                        newMob.StartZ = float.Parse(data["z"], culture);
                    else
                        newMob.StartZ = map.GetHeight(newMob.StartX, newMob.StartY);
                    newMob.range = float.Parse(data["range"], culture);
                    newActor.x = pos[0];
                    newActor.y = pos[1];
                    newActor.z = pos[2];

                    if (lootItems.Count == 0)
                        lootItems = template.lootItems;

                    foreach (Dictionary<string, string> loot in lootItems)
                    {
                        int amount = 1;
                        if (loot.ContainsKey("amount")) amount = int.Parse(loot["amount"]);
                        int itemid = int.Parse(loot["id"]);
                        float perc = (float)Config.Instance.DropRate / 100;
                        for (int i = 0; i < amount; ++i)
                            if (Global.Random.Next(0, 10000) <= int.Parse(loot["rate"]) * perc) newMob.AddLoot(itemid);
                    }

                    Map newMobMap = null;
                    if (!MapManager.Instance.GetMap(newActor.mapID, out newMobMap))
                    {
                        Logger.ShowError("Could not obtain map for monster spawn ", null);
                        return 0;
                    }

                    newMob.Map = newMobMap;
                    //newMob.StartZ = newMob.Map.HeightMap.GetZ(newMob.StartX, newMob.StartY);
                    //newActor.z = newMob.StartZ;

                    //**********AI*************
                    newMob.ai = new MobAI(newMob);
                    //newMob.ai.Start(); // the ai should be switched on if there is a playere near by, for test now
                    //newActor.sightRange = Global.MakeSightRange(1000); //it is loaded in Mobdb, no more needed
                    if (newMobMap.RegisterActor(newActor))
                    {
                        if (newMob.Delay != -1)
                            newMob.RespawnTask = new SpawnOnce(newMob);
                        scripts.Add(newMobMap.ID + "-" + newMob.Name + "-" + spawnCount++, newMob);
                        //Logger.ShowInfo(newMob.Name.PadRight(20) + " MOB added at " + newMobMap.Name + ":" + newActor.x + "#" + newActor.y + "#" + newActor.z, null);
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex, null);
                }
            }
            return count;
        }

        public static float[] GetRandomPos(Map map,float x, float y,float range)
        {
            float[] pos = new float[3];
            float[] unitvec=new float[3];
            pos[0] = x;
            pos[1] = y;
            unitvec = MobAI.GetRandomUnitVector();
            pos = MobAI.Add(pos, MobAI.ScalarProduct(unitvec, Global.Random.Next(0, (int)range)));
            pos[2] = map.GetHeight(pos[0], pos[1]);
            return pos;
        }

        private int[] LoadAssembly(Assembly newAssembly)
        {
            Module[] newScripts = newAssembly.GetModules();
            int[] count = new int[2];
            foreach (Module newScript in newScripts)
            {
                Type[] types = newScript.GetTypes();
                foreach (Type npcType in types)
                {
                    try
                    {
                        if (npcType.IsAbstract == true) continue;
                        if (npcType.BaseType == typeof(Scripting.QuestElement))
                            continue;
                        if (npcType.GetCustomAttributes(false).Length > 0) continue;
                        Npc newNpc;
                        try
                        {
                            newNpc = (Npc)newAssembly.CreateInstance(npcType.FullName);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        Map newScriptMap = null;
                        int mapID;
                        if (newNpc == null) continue;
                        if (newNpc.isItem == true)
                        {
                            ActorItem newitem = new ActorItem();
                            MapItem item = (MapItem)newNpc;
                            item.ActorI = newitem;
                            item.OnInit();
                            newitem.itemtype = item.Type;
                            mapID = MapManager.Instance.GetMapId(newNpc.MapName);
                            if (!MapManager.Instance.GetMap(mapID, out newScriptMap))
                            {
                                Logger.ShowError("Could not obtain map for script ", null);
                                continue;
                            }
                            item.Map = newScriptMap;
                            newitem.name = newNpc.Name;
                            newitem.e = item;
                            newitem.x = item.StartX;
                            newitem.y = item.StartY;
                            newitem.z = item.StartZ;
                            newitem.yaw = item.Startyaw;
                            newitem.sightRange = Global.MakeSightRange(1000);
                            if (newScriptMap.RegisterActor(newitem))
                            {
                                scripts.Add(newScriptMap.ID + "-" + item.Name, item);
                                count[0]++;
                                //Logger.ShowInfo(newNpc.Name.PadRight(20) + " MapItem added at " + newScriptMap.Name + ":" + newitem.x + "#" + newitem.y + "#" + newitem.z, null);
                            }
                            else
                                Logger.ShowError("Error while registering MapItem: " + newNpc.Name, null);

                        }
                        else
                        {

                            ActorNPC newActor = new ActorNPC(newNpc.Type, 100, 100, 100, 100);
                            newNpc.Actor = newActor;
                            newNpc.OnInit();
                            newActor.npcType = newNpc.Type;

                            // Get the map for this npc
                            mapID = MapManager.Instance.GetMapId(newNpc.MapName);
                            if (!MapManager.Instance.GetMap(mapID, out newScriptMap))
                            {
                                Logger.ShowError("Could not obtain map for script ", null);
                                continue;
                            }


                            newNpc.Map = newScriptMap;
                            newActor.name = newNpc.Name;
                            newActor.mapID = (byte)mapID;
                            newActor.e = newNpc;
                            newActor.x = newNpc.StartX;
                            newActor.y = newNpc.StartY;
                            //newNpc.StartZ = newScriptMap.HeightMap.GetZ(newNpc.StartX, newNpc.StartY);
                            newActor.z = newNpc.StartZ;
                            newActor.yaw = newNpc.Startyaw;
                            newActor.sightRange = Global.MakeSightRange(1000);
                            newActor.scriptName = npcType.Name;
                            newActor.version = newNpc.Version;

                            if (newNpc.Persistent)
                            {
                                ActorNPC persActor = MapServer.charDB.GetNpc(npcType.Name);
                                if (persActor == null)
                                {
                                    Logger.ShowInfo("New persistent actor: " + npcType.Name, null);
                                }
                                // different version of the actor found, use all new values
                                else if (persActor.version != newNpc.Version)
                                {
                                    Logger.ShowWarning("different version for: " + npcType.Name + " version changed to: " + newNpc.Version, null);
                                    MapServer.charDB.DeleteNpc(new ActorNPC(npcType.Name));
                                }
                                // persistant actor found in db, use old values only update the event link
                                else if (persActor.version == newNpc.Version)
                                {
                                    Logger.ShowInfo("Found persistant actor in the db: " + npcType.Name, null);
                                    newActor = persActor;
                                    newActor.e = newNpc;
                                }
                                // whatever the outcome store the current situation in the db
                                MapServer.charDB.SaveNpc(newActor);
                            }

                            if (newScriptMap.RegisterActor(newActor))
                            {
                                scripts.Add(newScriptMap.ID + "-" + newNpc.Name, newNpc);
                                count[1]++;
                                //Logger.ShowInfo(newNpc.Name.PadRight(20) + " NPC added at " + newScriptMap.Name + ":" + newActor.x + "#" + newActor.y + "#" + newActor.z, null);
                            }
                            else
                                Logger.ShowError("Error while registering npc: " + newNpc.Name, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
            }
            return count;
        }

        public static Assembly CompileScript(string[] Source, CodeDomProvider Provider)
        {
            return CompileScript(Source, Provider, null);
        }

        public static Assembly CompileScript(string[] Source, CodeDomProvider Provider, MapClient client)
        {
            //ICodeCompiler compiler = Provider.;
            CompilerParameters parms = new CompilerParameters();
            CompilerResults results;

            // Configure parameters
            parms.CompilerOptions = "/target:library /optimize";
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = true;
            parms.ReferencedAssemblies.Add("System.dll");
            parms.ReferencedAssemblies.Add("SagaLib.dll");
            parms.ReferencedAssemblies.Add("SagaDB.dll");
            parms.ReferencedAssemblies.Add("SagaMap.exe");            
            // Compile
            results = Provider.CompileAssemblyFromFile(parms, Source);
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    if (client != null)
                    {
                        client.SendMessage("Saga", "Compile Error:" + error.ErrorText);
                        client.SendMessage("Saga", "File:" + error.FileName + ":" + error.Line);
                    }
                    Logger.ShowError("Compile Error:" + error.ErrorText, null);
                    Logger.ShowError("File:" + error.FileName + ":" + error.Line, null);
                }
                return null;
            }
            //get a hold of the actual assembly that was generated
            return results.CompiledAssembly;
        }
    }
}
