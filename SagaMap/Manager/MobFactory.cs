using System;
using System.Collections.Generic;
using System.Text;
using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using System.Xml;
using SagaMap.Scripting;
using SagaMap.Tasks;

namespace SagaMap
{
    public static class MobFactory
    {
        private static XmlParser xml;
        private static Dictionary<uint, Mob> mobs;

        public static void Start(string configFile)
        {
            mobs = new Dictionary<uint, Mob>();

            try { xml = new XmlParser(configFile); }
            catch (Exception) { Logger.ShowError(" cannot read the Mob database file.", null); return; }

            XmlNodeList XMLitems = xml.Parse("Mob");
            Logger.ShowInfo("Mob database contains " + XMLitems.Count + " Mobs.", null);

            for (int i = 0; i < XMLitems.Count; i++)
                AddMob(XMLitems.Item(i));
            xml = null;

        }

        private static void AddMob(XmlNode portal)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            XmlNodeList childList = portal.ChildNodes;
            for (int i = 0; i < childList.Count; i++)
                data.Add(childList.Item(i).Name, childList.Item(i).InnerText);

            if (!data.ContainsKey("ID")) return;
            try
            {
                uint id;
                Mob mob = new Mob();
                mob.Actor = new ActorNPC("mob");
                id = uint.Parse(data["ID"]);
                mob.Name = data["Name"];
                if (mob.Name == "0") return;
                try
                {
                    mob.Actor.maxHP = ushort.Parse(data["HP"]);
                    mob.Actor.maxSP = ushort.Parse(data["SP"]);
                }
                catch (Exception)
                {
                }
                mob.Actor.BattleStatus = new BattleStatus();
                mob.Actor.level = byte.Parse(data["Level"]);
                mob.Actor.cEXP = ushort.Parse(data["CEXP"]);
                mob.Actor.jEXP = ushort.Parse(data["JEXP"]);
                mob.Actor.wEXP = ushort.Parse(data["WEXP"]);
                mob.Actor.BattleStatus.def = int.Parse(data["Def"]);
                mob.Actor.BattleStatus.flee = int.Parse(data["Flee"]);

                mob.MinAtk = ushort.Parse(data["AtkMin"]);
                mob.MaxAtk = ushort.Parse(data["AtkMax"]);
                mob.Cri = byte.Parse(data["Cri"]);
                mob.Actor.BattleStatus.hit = int.Parse(data["Hit"]);
                mob.ASPD = ushort.Parse(data["ASPD"]);
                mob.Actor.sightRange = uint.Parse(data["SightRange"]);
                mob.Size = ushort.Parse(data["Size"]);
                mob.WalkSpeed = ushort.Parse(data["WalkSpeed"]);
                mob.RunSpeed = ushort.Parse(data["RunSpeed"]);
                if (data.ContainsKey("LivingSpace")) mob.LivingSpace = (Mob.Space)Enum.Parse(typeof(Mob.Space), data["LivingSpace"]);
                mobs.Add(id, mob);
            }
            catch (Exception e) { Logger.ShowError("cannot parse: " + data["ID"], null); Logger.ShowError(e, null); return; }

        }

        public static Mob GetMob(uint ID, ref ActorNPC actor)
        {
            Mob mob;
            if (!mobs.ContainsKey(ID)) return null;
            mob = mobs[ID];
            Mob newmob = new Mob();
            actor.BattleStatus = new BattleStatus();
            actor.Tasks = new Dictionary<string, MultiRunTask>();
            actor.HP = mob.Actor.maxHP;
            actor.maxHP = mob.Actor.maxHP;
            actor.SP = mob.Actor.maxSP;
            actor.maxSP = mob.Actor.maxSP;
            actor.level = mob.Actor.level;
            actor.cEXP = mob.Actor.cEXP;
            actor.jEXP = mob.Actor.jEXP;
            actor.wEXP = mob.Actor.wEXP;
            actor.BattleStatus.def = mob.Actor.BattleStatus.def;
            actor.BattleStatus.flee = mob.Actor.BattleStatus.flee;
            newmob.Name = mob.Name;
            newmob.Type = ID;
            newmob.MinAtk = mob.MinAtk;
            newmob.MaxAtk = mob.MaxAtk;
            newmob.Cri = mob.Cri;
            actor.BattleStatus.hit = mob.Actor.BattleStatus.hit;
            newmob.ASPD = mob.ASPD;
            actor.sightRange = mob.Actor.sightRange;
            newmob.Size = mob.Size;
            newmob.WalkSpeed = mob.WalkSpeed;
            newmob.RunSpeed = mob.RunSpeed;
            newmob.Actor = actor;
            newmob.LivingSpace = mob.LivingSpace;
            actor.e = newmob;
            newmob.corpsetask = new DeleteCorpse(newmob);
            return newmob;
        }
    }
}
