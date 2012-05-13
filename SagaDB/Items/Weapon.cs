using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using SagaLib;

namespace SagaDB.Items
{
    [Serializable]
    public class Weapon
    {
        public string name;
        public byte level;
        public ushort type;
        public uint augeSkillID;
        public uint exp;
        public ushort durability;
        public byte U1;//Something like aspd,the greater the slower you will attack
        public byte active;
        public uint[] stones;

        public string Name { get { return this.name; } set { this.name = value; } }
        public byte Level { get { return this.level; } set { this.level = value; } }
        public SagaDB.Actors.WeaponType Type { get { return (SagaDB.Actors.WeaponType)this.type; } set { this.type = (ushort)value; } }
        public uint AugenStone { get { return this.augeSkillID; } set { this.augeSkillID = value; } }
        public uint EXP { get { return this.exp; } set { this.exp = value; } }
        public ushort Durability { get { return this.durability; } set { this.durability = value; } }
        public uint[] Stones { get { return this.stones; } set { this.stones = value; } }
    }
    [Serializable]
    public class WeaponInfo
    {
		public static readonly WeaponInfo Empty = new WeaponInfo();

        public byte level;
        public uint maxdurability;
        public uint minatk;
        public uint maxatk;
        public uint minrangeatk;
        public uint maxrangeatk;
        public uint minmatk;
        public uint maxmatk;
    }

    public static class WeaponFactory
    {
        private static XmlParser xml;
        private static Dictionary<byte,Dictionary<byte,WeaponInfo>> weapons;

        public static void Start(string configFile)
        {
            weapons = new Dictionary<byte, Dictionary<byte, WeaponInfo>>();
            try { xml = new XmlParser(configFile); }
            catch (Exception) { Logger.ShowError(" cannot read the weapon database file.", null); return; }

            XmlNodeList XMLitems = xml.Parse("Weapon");
            Logger.ShowInfo("Weapon database contains " + XMLitems.Count + " weapons.", null);

            for (int i = 0; i < XMLitems.Count; i++)
                AddWeapon(XMLitems.Item(i));
            xml = null;
        }

        private static void AddWeapon(XmlNode skill)
        {
            Dictionary<string, string> data;
            byte type;
            Dictionary<byte, WeaponInfo> nentry = new Dictionary<byte, WeaponInfo>();
                    
            type = byte.Parse(skill.ChildNodes[0].InnerText);
            for (int i = 0; i < skill.ChildNodes.Count - 1; i++)
            {
                WeaponInfo nweapon=new WeaponInfo();
                data = new Dictionary<string, string>();
                XmlNodeList childList = skill.ChildNodes[i + 1].ChildNodes;
                for (int j = 0; j < childList.Count; j++)
                    data.Add(childList.Item(j).Name, childList.Item(j).InnerText);

                if (!data.ContainsKey("Level")) continue;
                try
                {
                    nweapon.level = byte.Parse(data["Level"]);
                    nweapon.maxdurability = uint.Parse(data["MaxDurability"]);
                    nweapon.minatk = uint.Parse(data["MinAtk"]);
                    nweapon.maxatk = uint.Parse(data["MaxAtk"]);
                    nweapon.minrangeatk = uint.Parse(data["MinRangeAtk"]);
                    nweapon.maxrangeatk = uint.Parse(data["MaxRangeAtk"]);
                    nweapon.minmatk = uint.Parse(data["MinMagicAtk"]);
                    nweapon.maxmatk = uint.Parse(data["MaxMagicAtk"]);
                    nentry.Add(nweapon.level, nweapon); 
                }
                catch (Exception e) { Logger.ShowError("cannot parse: " + data["skillId"], null); Logger.ShowError(e, null); return; }

   
            }
            weapons.Add(type, nentry);
      }
        public static Weapon GetActiveWeapon(SagaDB.Actors.ActorPC pc)
        {
            foreach (Weapon w in pc.Weapons)
            {
                if (w.active == 1) return w;
            }
            Weapon nw = new Weapon();
            nw.type = 0;
            return nw;
        }


        public static WeaponInfo GetWeaponInfo(byte type, byte level)
        {
			if (!weapons.ContainsKey(type))
			{
				// TODO implement default weapon type to safe if an non existing type should be input
				Logger.ShowWarning(String.Format("Tried to get WeaponInfo by non existing type '{0}'", type));
				return null;
			}
			if (!weapons[type].ContainsKey(level))
			{
				Logger.ShowWarning(String.Format("Tried to get WeaponInfo by type '{0}' for non existing level '{1}'. Picking first level in stack for safety", type, level));
				
				// NOTE well, better than returning null :)
				if(weapons[type].Count > 0)
				{
					Dictionary<byte, WeaponInfo>.ValueCollection.Enumerator enumerator = weapons[type].Values.GetEnumerator();
					enumerator.MoveNext();
					return enumerator.Current;
				}
				else
					return WeaponInfo.Empty;
			}
            return weapons[type][level];
        }

    }

}
