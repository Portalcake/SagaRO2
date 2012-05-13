using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Items;


namespace SagaDB.Actors
{
    [Serializable]
    public class ActorPC : Actor
    { 
        public uint charID;
        public string userName;
        public byte[] face;
        public byte[] details;
        public GenderType sex;
        public RaceType race;
        public JobType job;
        public uint cExp, jExp;
        public uint cLevel, jLevel;
        public byte pendingDeletion;
        public uint validationKey;
        public ushort HP, maxHP, SP, maxSP;
        public byte LC, maxLC, LP, maxLP;
        public byte str, dex, intel, con, luk, stpoints;
        public uint zeny;
        public byte save_map;
        public float save_x, save_y, save_z;
        public uint speed;
        public Inventory inv;
        public Slots mslots;
        public byte[] slots;
        public string weaponName;
        public int weaponType;
        public ITEM_TYPE currentInvTab;
        public uint GMLevel;
        public Trading trading;
        public TradeStatus TradeStatus;
        public uint TradeTarget;
        public Party PartyStatus;
        public uint PartyTarget;
        public ActorItem LastMissionBoard;
        //public byte NumShortcuts;
        public Dictionary<byte,Shortcut> ShorcutIDs;
        public List<SagaDB.Items.Item> CurNPCinv ;
        public Actor CurTarget;
        public List<Weapon> Weapons;
        public Dictionary<JobType, byte> JobLevels;
        public Dictionary<uint, SkillInfo> LivingSkills;
        public Dictionary<uint, SkillInfo> SpecialSkills;
        public Dictionary<uint, SkillInfo> BattleSkills;
        public Dictionary<uint, SkillInfo> InactiveSkills;
        public Dictionary<uint, Quest.Quest> QuestTable;
        public Dictionary<uint, Quest.Quest> PersonalQuestTable;
        public Dictionary<byte, byte> MapInfo;
        public uint Scenario;
        public byte online;
        public byte muted;

        /// <summary>
        /// Unique ID in Character Database
        /// </summary>
        public uint CharID { get { return this.charID; } }
        public string Account { get { return this.userName; } }
        public byte Online { get { return this.online; } set { this.online = value; } }
        public uint GMLvl { get { return this.GMLevel; } set { this.GMLevel = value; } }
        public byte[] Face { get { return this.face; } set { this.face = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public GenderType Gender { get { return this.sex; } set { this.sex = value; } }
        public RaceType Race { get { return this.race; } set { this.race = value; } }
        public JobType Job { get { return this.job; } set { this.job = value; } }
        public uint CEXP { get { return this.cExp; } set { this.cExp = value; } }
        public uint JEXP { get { return this.jExp; } set { this.jExp = value; } }

        public List<SkillInfo> Skills
        {
            get
            {
                List<SkillInfo> tmp = new List<SkillInfo>();
                foreach(uint i in this.BattleSkills.Keys)
                {
                    this.BattleSkills[i].ID = i;
                    tmp.Add(this.BattleSkills[i]);
                }
                return tmp;
            }
            set
            {
                this.BattleSkills.Clear();
                foreach (SkillInfo i in value)
                {
                    this.BattleSkills.Add(i.ID, i);
                }                
            }
        }

        public List<SkillInfo> SkillsSpecial
        {
            get
            {
                List<SkillInfo> tmp = new List<SkillInfo>();
                foreach (uint i in this.SpecialSkills.Keys)
                {
                    this.SpecialSkills[i].ID = i;
                    tmp.Add(this.SpecialSkills[i]);
                }
                return tmp;
            }
            set
            {
                this.SpecialSkills.Clear();
                foreach (SkillInfo i in value)
                {
                    this.SpecialSkills.Add(i.ID, i);
                }
            }
        }

        public List<SkillInfo> SkillsInavtive
        {
            get
            {
                List<SkillInfo> tmp = new List<SkillInfo>();
                foreach (uint i in this.InactiveSkills.Keys)
                {
                    this.InactiveSkills[i].ID = i;
                    tmp.Add(this.InactiveSkills[i]);
                }
                return tmp;
            }
            set
            {
                this.InactiveSkills.Clear();
                foreach (SkillInfo i in value)
                {
                    this.InactiveSkills.Add(i.ID, i);
                }
            }
        }


        public List<Quest.Quest> Quests
        {
            get
            {
                List<Quest.Quest> tmp = new List<Quest.Quest>();
                foreach (uint i in this.QuestTable.Keys)
                {
                    tmp.Add(this.QuestTable[i]);
                }
                return tmp;
            }
            set
            {
                this.QuestTable.Clear();
                foreach (Quest.Quest i in value)
                {
                    this.QuestTable.Add(i.ID, i);
                }
            }
        }

        public List<Quest.Quest> PersonalQuests
        {
            get
            {
                List<Quest.Quest> tmp = new List<Quest.Quest>();
                foreach (uint i in this.PersonalQuestTable.Keys)
                {
                    tmp.Add(this.PersonalQuestTable[i]);
                }
                return tmp;
            }
            set
            {
                this.PersonalQuestTable.Clear();
                foreach (Quest.Quest i in value)
                {
                    this.PersonalQuestTable.Add(i.ID, i);
                }
            }
        }

        public List<Item> Inventory
        {
            get
            {
                return this.inv.GetInventoryList();
            }
            set
            {
                this.inv.inv.Clear();
                foreach (Item i in value)
                {
                    this.inv.AddItem(i);
                }
            }
        }

        public List<Item> Equipment
        {
            get
            {
                List<Item> items = new List<Item>();
                foreach(Item i in this.inv.EquipList.Values)
                {
                    items.Add(i);
                }
                return items;
            }

        }

        public List<Weapon> Weapon { get { return this.Weapons; } set { this.Weapons = value; } }

        public byte Str { get { return this.str; } set { this.str = value; } }
        public byte Dex { get { return this.dex; } set { this.dex = value; } }
        public byte Con { get { return this.con; } set { this.con = value; } }
        public byte Int { get { return this.intel; } set { this.intel = value; } }
        public byte Luk { get { return this.luk; } set { this.luk = value; } }

        public byte Save_Map { get { return this.save_map; } set { this.save_map = value; } }
        public float[] Sava_Position
        {
            get
            {
                float[] pos = new float[3];
                pos[0] = this.save_x;
                pos[1] = this.save_y;
                pos[2] = this.save_z;
                return pos;
            }
            set
            {
                this.save_x = value[0];
                this.save_y = value[1];
                this.save_z = value[2];
            }
        }

        public byte Map { get { return this.mapID; } set { this.mapID = value; } }
        public float[] Position
        {
            get
            {
                float[] pos = new float[3];
                pos[0] = this.x;
                pos[1] = this.y;
                pos[2] = this.z;
                return pos;
            }
            set
            {
                this.x = value[0];
                this.y = value[1];
                this.z = value[2];
            }
        }

        [Serializable]
        public struct Shortcut
        {
            public byte type;
            public uint ID;
        }
        /*
         *  Warning:
         * 
         *  Never ever add "new x[y]" commands to the constructors.
         *  As these are used to crate db4o templates, they have to be completely
         *  empty (only containing worldID & charID) when retreiving a char from the db.
         * 
         */

        public ActorPC(byte worldID, string cName)
        {
            this.type = ActorType.PC;
            this.name = cName;
            this.worldID = worldID;
        }

        public ActorPC(uint cId,byte worldID)
        {
            this.type = ActorType.PC;
            this.charID = cId;
            this.worldID = worldID;
        }

        public ActorPC()
        {
        }

    }
}
