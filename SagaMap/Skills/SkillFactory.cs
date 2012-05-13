using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

using SagaLib;

namespace SagaMap.Skills
{
    public class Skill
    {
        public string name;
        public uint skillid;
        public byte skilltype;
        public byte skillctgroup;
        public ulong maxsxp;
        public byte growlv;
        public uint minrange;
        public uint maxrange;
        public byte target;
        public uint casttime;
        public uint delay;
        public uint sp;
        public byte race;
        public byte special;
        public byte special_jlv;
        public byte[] reqjob = new byte[15];
        public byte[] reqweapon = new byte[9];
        public uint addition;
        public uint[] stance = new uint[6];
        public uint[] effcast = new uint[5];
        public uint[] effactive = new uint[5];
        public uint[] effshot = new uint[5];
        public uint[] effblow = new uint[5];
        public uint[] effrange = new uint[5];
        public uint[] effdamage = new uint[5];
        public uint[] effdamageb = new uint[5];

        [Category("Basic Infomation"), Description("Name of the skill")]
        public string Name { get { return this.name; } }
        [Category("Basic Infomation"), Description("ID of the skill")]        
        public uint ID { get { return this.skillid; } }
        [Category("Basic Infomation"), Description("Type of the skill")]
        public byte SkillType { get { return this.skilltype; } }
        [Category("Basic Infomation"), Description("Group of the skill")]
        public byte SkillGroup { get { return this.skillctgroup; } }
        [Category("Basic Infomation"), Description("Maximun of the EXP the skill could reach")]
        public ulong SkillMaxEXP { get { return this.maxsxp; } }
        [Category("Basic Infomation"), Description("Maximal level of the skill")]
        public byte GrowLevel { get { return this.growlv; } }
        [Category("Basic Infomation"), Description("Minimal range to cast the skill")]
        public uint MinRange { get { return this.minrange; } }
        [Category("Basic Infomation"), Description("Maximal range to cast the skill")]
        public uint MaxRange { get { return this.skilltype; } }
        [Category("Basic Infomation"), Description("Type of the target")]
        public byte TargetType { get { return this.target; } }
        [Category("Basic Infomation"), Description("Casting time of the skill")]
        public uint CastTime { get { return this.casttime; } }
        [Category("Basic Infomation"), Description("Cool down time of the skill")]
        public uint CDTime { get { return this.delay; } }
        [Category("Basic Infomation"), Description("SP required to cast the skill")]
        public uint SP { get { return this.sp; } }
        [Category("Basic Infomation"), Description("Race required for the skill")]
        public SagaDB.Actors.RaceType Race { get { return (SagaDB.Actors.RaceType)this.race; } }
        [Category("Basic Infomation"), Description("Speciality Tiles needed for the skill")]
        public byte Special { get { return this.special; } }
        [Category("Basic Infomation"), Description("Job Level required to specialize the skill")]
        public byte SpecialJLV { get { return this.special_jlv; } }
        [Category("Basic Infomation"), Description("Job Requirement for the skill")]
        public Dictionary<SagaDB.Actors.JobType, bool> JobRequirement
        {
            get
            {
                Dictionary<SagaDB.Actors.JobType, bool> list = new Dictionary<SagaDB.Actors.JobType, bool>();
                int j = 1;
                foreach (byte i in this.reqjob)
                {
                    if (i == 255)
                        list.Add((SagaDB.Actors.JobType)j, false);
                    if (i == 1)
                        list.Add((SagaDB.Actors.JobType)j, true);
                    j++;
                }
                return list;
            }
        }
        [Category("Basic Infomation"), Description("Weapon Requirement for the skill")]
        public Dictionary<SagaDB.Actors.WeaponType, bool> WeaponRequirement
        {
            get
            {
                Dictionary<SagaDB.Actors.WeaponType, bool> list = new Dictionary<SagaDB.Actors.WeaponType, bool>();
                int j = 0;
                foreach (byte i in this.reqweapon)
                {
                    if (i == 255)
                        list.Add((SagaDB.Actors.WeaponType)j, false);
                    if (i == 1)
                        list.Add((SagaDB.Actors.WeaponType)j, true);
                    j++;
                }
                return list;
            }
        }
        [Category("Addition Bonus"), Description("ID of the addition")]
        public uint AdditionID { get { return this.addition; } }        
        [Category("Addition Bonus"), Description("Addition bonus along with the skill")]        
        public Dictionary<SagaDB.Items.ADDITION_BONUS, int> Bonus
        {
            get
            {
                Dictionary<SagaDB.Items.ADDITION_BONUS, int> list = new Dictionary<SagaDB.Items.ADDITION_BONUS, int>();
                List<SagaDB.Items.Bonus> addition = SagaDB.Items.AdditionFactory.GetBonus(this.addition);
                foreach (SagaDB.Items.Bonus i in addition)
                {
                    list.Add(i.Effect, i.Value);
                }
                return list;
            }
        }
    }
    
    public static class SkillFactory
    {
        
        private static XmlParser xml;
        private static Dictionary<uint,Skill> skills;

        public static void Start(string configFile)
        {
            skills = new Dictionary<uint, Skill>();
            try { xml = new XmlParser(configFile); }
            catch (Exception) { Logger.ShowError(" cannot read the skill database file.", null); return; }

            XmlNodeList XMLitems = xml.Parse("Skill");

            SagaLib.Logger.ShowInfo("Loading skill database:");
            Console.ForegroundColor = ConsoleColor.Green;
            int tenperc = XMLitems.Count / 20;
            for (int i = 0; i < XMLitems.Count; i++)
            {
                if (i % tenperc == 0)
                    Console.Write("*");
                AddSkill(XMLitems.Item(i));
            }
            Console.WriteLine();
            Console.ResetColor();            
           
            Logger.ShowInfo(XMLitems.Count + " Skills loaded.", null);
            xml = null;
            GC.Collect();
        }

        private static void AddSkill(XmlNode skill)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            XmlNodeList childList =skill.ChildNodes;
            for (int i = 0; i < childList.Count; i++)
                data.Add(childList.Item(i).Name, childList.Item(i).InnerText);

            if (!data.ContainsKey("skillId")) return;
            try
            {
                Skill nskill = new Skill();
                nskill.name = data["Name"];
                nskill.skillid = uint.Parse(data["skillId"]);
                nskill.skilltype =byte.Parse(data["skillType"]);
                nskill.maxsxp = ulong.Parse(data["MaxSkillExp"]);
                nskill.growlv = byte.Parse(data["GrowLevel"]);
                nskill.minrange = uint.Parse(data["MinRange"]);
                nskill.maxrange = uint.Parse(data["MaxRange"]);
                nskill.target = byte.Parse(data["Target"]);
                nskill.casttime = uint.Parse(data["CastTime"]);
                nskill.delay = uint.Parse(data["Delay"]);
                nskill.sp = uint.Parse(data["SP"]);
                nskill.race = byte.Parse(data["race"]);
                nskill.special = byte.Parse(data["Special"]);
                nskill.special_jlv = byte.Parse(data["SpecialJLVRequirement"]);
                nskill.reqjob = StringToBytes(data["JobRequirement"]);
                byte tmp = nskill.reqjob[2];
                nskill.reqjob[2] = nskill.reqjob[3];
                nskill.reqjob[3] = tmp;
                nskill.reqweapon = StringToBytes(data["WeaponRequirement"]);
                nskill.addition = uint.Parse(data["Addition"]);
                nskill.stance = StringToUInt(data["Stance"]);
                nskill.effcast = StringToUInt(data["EffCast"]);
                nskill.effactive = StringToUInt(data["EffActive"]);
                nskill.effshot = StringToUInt(data["EffShot"]);
                nskill.effblow = StringToUInt(data["EffBlow"]);
                nskill.effrange = StringToUInt(data["EffRange"]);
                nskill.effdamage = StringToUInt(data["EffDamage"]);
                nskill.effdamageb = StringToUInt(data["EffDamageB"]);
                skills.Add(nskill.skillid, nskill);
            }
            catch (Exception e) { Logger.ShowError("cannot parse: " + data["skillId"], null); Logger.ShowError(e, null); return; }
                    
        }

        private static byte[] StringToBytes(string txt)
        {
            byte[] buf;
            string[] txt2 = txt.Split(',');
            buf = new byte[txt2.Length];
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = byte.Parse(txt2[i]);
            }
            return buf;
        }

        private static uint[] StringToUInt(string txt)
        {
            uint[] buf;
            string[] txt2 = txt.Split(',');
            buf = new uint[txt2.Length];
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = uint.Parse(txt2[i]);
            }
            return buf;
        }

        public static Skill GetSkill(uint skillid)
        {
            if (skills.ContainsKey(skillid)) return skills[skillid];else  return null;
        }
    }
}
