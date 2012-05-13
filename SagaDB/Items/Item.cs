using System;
using System.Xml;
using System.Collections.Generic;

using SagaDB;
using SagaDB.Actors;

namespace SagaDB.Items
{
    [Serializable]
    public enum ITEM_TYPE
    {
        USEABLE, EQUIP, ETC
    }
    [Serializable]
    public enum ITEM_TYPE2
    {
        HEAD, BODY, WAIST, PANTS, HAND, FOOT, BACK, EAR,
        NECKLACE, FINGER, SHIELD, UNKNOWN, POTION, FOOD, DYER, SOUL, ELEMENT,
        ETC, MATRIRIAL, COLLECTION, AUGE_STONE, ALTER_STONE, KARMA_STONE,
        SKILL_BOOK, MAP, FILE
    }
    public enum ITEM_UPDATE_REASON { DISCARD = 1, PURCHASED = 2, SOLD = 3, NPC_GAVE = 4, NPC_TOOK = 5, FOUND = 6, DESTROYED = 7, OTHER = 8 };
    

    [Serializable]
    public class Item
    {
        [Serializable]
        private class BaseItem
        {
            // These values are stored in item.dat
            private ITEM_TYPE m_Type;
            private ITEM_TYPE2 m_itemType;
            private int m_ItemID;
            private uint m_Price;
            private uint m_IconID;
            private uint m_RequiredAddition; // TODO: ?
            private uint m_SkillID;
            private uint m_RequiredCLvl;
            private uint m_RequiredStr;
            private uint m_RequiredDex;
            private uint m_RequiredInt;
            private uint m_RequiredCon;
            private uint m_RequiredLuk;
            private ushort m_MeshID;
            private ushort m_Quest; // TODO: ?
            private ushort m_MaxDurability;
            private bool m_QuestItem;
            private bool m_Tradeable;
            private bool m_Dropable;
            private bool m_Equipable;
            private EQUIP_SLOT m_EquipSlot;
            private string m_Name;
            private string m_Description;
            private Dictionary<GenderType, bool> m_RequiredGender;
            private Dictionary<RaceType, bool> m_RequiredRace;
            private Dictionary<JobType, byte> m_RequiredJob;
            private Dictionary<WeaponType, byte> m_RequiredWeapon;
            private List<Bonus> m_Bonuses;
            private byte m_MaxStackSize;
            private uint m_OptionID;

            public ITEM_TYPE Type { get { return m_Type; } }
            public ITEM_TYPE2 ItemType { get { return m_itemType; } }
            public int ItemID { get { return m_ItemID; } }
            public uint Price { get { return m_Price; } }
            public uint IconID { get { return m_IconID; } }
            public uint RequiredAddition { get { return m_RequiredAddition; } }
            public uint SkillID { get { return m_SkillID; } }
            public uint RequiredCLvl { get { return m_RequiredCLvl; } }
            public uint RequiredStr { get { return m_RequiredStr; } }
            public uint RequiredDex { get { return m_RequiredDex; } }
            public uint RequiredInt { get { return m_RequiredInt; } }
            public uint RequiredCon { get { return m_RequiredCon; } }
            public uint RequiredLuk { get { return m_RequiredLuk; } }
            public ushort MeshID { get { return m_MeshID; } }
            public ushort Quest { get { return m_Quest; } }
            public ushort MaxDurability { get { return m_MaxDurability; } }
            public bool QuestItem { get { return m_QuestItem; } }
            public bool Tradeable { get { return m_Tradeable; } }
            public bool Dropable { get { return m_Dropable; } }
            public bool Equipable { get { return m_Equipable; } }
            public EQUIP_SLOT EquipSlot { get { return m_EquipSlot; } }
            public string Name { get { return m_Name; } }
            public string Description { get { return m_Description; } }
            public Dictionary<GenderType, bool> RequiredGender { get { return m_RequiredGender; } }
            public Dictionary<RaceType, bool> RequiredRace { get { return m_RequiredRace; } }
            public Dictionary<JobType, byte> RequiredJob { get { return m_RequiredJob; } }
            public Dictionary<WeaponType, byte> RequiredWeapon { get { return m_RequiredWeapon; } }
            public List<Bonus> Bonuses { get { return m_Bonuses; } }
            public byte MaxStackSize { get { return m_MaxStackSize; } }

            public static BaseItem Parse( System.Xml.XmlNode node )
            {
                BaseItem item = new BaseItem();
                item.m_RequiredRace = new Dictionary<RaceType, bool>();
                item.m_RequiredGender = new Dictionary<GenderType, bool>();
                int j = 0;
                for( int i = 0; i < node.ChildNodes.Count; i = j )
                {
                    if( node.ChildNodes[j].Name == "id" ) item.m_ItemID = Int32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "mesh" ) item.m_MeshID = UInt16.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "icon" ) item.m_IconID = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "unknown5" ) ++j;
                    if( node.ChildNodes[j].Name == "questitem" ) item.m_QuestItem = node.ChildNodes[j++].InnerText != "0";
                    if( node.ChildNodes[j].Name == "equip_type" ) item.m_EquipSlot = (EQUIP_SLOT)Int32.Parse( node.ChildNodes[j++].InnerText );
                    if (node.ChildNodes[j].Name == "type") ++j;
                    if( node.ChildNodes[j].Name == "price" ) item.m_Price = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "unknown1" ) ++j;
                    if( node.ChildNodes[j].Name == "trade" ) item.m_Tradeable = node.ChildNodes[j++].InnerText != "0";
                    if( node.ChildNodes[j].Name == "drop" ) item.m_Dropable = node.ChildNodes[j++].InnerText != "0";
                    if( node.ChildNodes[j].Name == "unknown3" ) ++j;
                    if( node.ChildNodes[j].Name == "unknown4" ) ++j;
                    if( node.ChildNodes[j].Name == "name" ) item.m_Name = node.ChildNodes[j++].InnerText;
                    if( node.ChildNodes[j].Name == "desc" ) item.m_Description = node.ChildNodes[j++].InnerText;
                    if( node.ChildNodes[j].Name == "quest" ) item.m_Quest = UInt16.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "req_clv" ) item.m_RequiredCLvl = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if (node.ChildNodes[j].Name == "req_male")
                    {                       
                        if (node.ChildNodes[j++].InnerText == "1")
                            item.m_RequiredGender.Add(GenderType.MALE, true);
                        else
                            item.m_RequiredGender.Add(GenderType.MALE, false);                        
                    }
                    if (node.ChildNodes[j].Name == "req_female")
                    {
                        if (node.ChildNodes[j++].InnerText == "1")
                            item.m_RequiredGender.Add(GenderType.FEMALE, true);
                        else
                            item.m_RequiredGender.Add(GenderType.FEMALE, false);            
                    }
                    if (node.ChildNodes[j].Name == "req_norman")
                    {
                        if (node.ChildNodes[j++].InnerText == "1")
                            item.m_RequiredRace.Add(RaceType.NORMAN, true);
                        else
                            item.m_RequiredRace.Add(RaceType.NORMAN, false); 
                    }
                    if (node.ChildNodes[j].Name == "req_ellr")
                    {
                        if (node.ChildNodes[j++].InnerText == "1")
                            item.m_RequiredRace.Add(RaceType.ELLR, true);
                        else
                            item.m_RequiredRace.Add(RaceType.ELLR, false); 
                    }
                    if (node.ChildNodes[j].Name == "req_dimago")
                    {
                        if (node.ChildNodes[j++].InnerText == "1")
                            item.m_RequiredRace.Add(RaceType.DIMAGO, true);
                        else
                            item.m_RequiredRace.Add(RaceType.DIMAGO, false); 
                    }
                    if( node.ChildNodes[j].Name == "req_str" ) item.m_RequiredStr = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "req_dex" ) item.m_RequiredDex = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "req_int" ) item.m_RequiredInt = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "req_con" ) item.m_RequiredCon = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "req_luc" ) item.m_RequiredLuk = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if (node.ChildNodes[j].Name == "JobRequirement")
                    {
                        item.m_RequiredJob = new Dictionary<JobType, byte>();
                        byte[] tmp = StringToBytes(node.ChildNodes[j++].InnerText);
                        for (int k = 0; k < tmp.Length; k++)
                        {
                            if (k == 2)
                            {
                                item.m_RequiredJob.Add((JobType)(4), tmp[k]);
                                continue;
                            }
                            if (k == 3)
                            {
                                item.m_RequiredJob.Add((JobType)(3), tmp[k]);
                                continue;
                            }
                            item.m_RequiredJob.Add((JobType)(k + 1), tmp[k]);
                        }                        
                    }
                    if (node.ChildNodes[j].Name == "WeaponRequirement")
                    {
                        byte[] tmp;
                        item.m_RequiredWeapon = new Dictionary<WeaponType, byte>();
                        tmp = StringToBytes(node.ChildNodes[j++].InnerText);
                        for (int k = 0; k < tmp.Length; k++)
                        {
                            item.m_RequiredWeapon.Add((WeaponType)k, tmp[k]);
                        }
                    }
                    if( node.ChildNodes[j].Name == "req_summons" ) ++j;
                    if( node.ChildNodes[j].Name == "req_additions" ) item.m_RequiredAddition = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "max_stack" ) item.m_MaxStackSize = Byte.Parse( node.ChildNodes[j++].InnerText );
                    if( node.ChildNodes[j].Name == "max_dur" ) item.m_MaxDurability = UInt16.Parse( node.ChildNodes[j++].InnerText );
                    if (node.ChildNodes[j].Name == "unknown2") item.m_itemType = (ITEM_TYPE2)Int32.Parse(node.ChildNodes[j++].InnerText); 
                    if( node.ChildNodes[j].Name == "skill_id" ) item.m_SkillID = UInt32.Parse( node.ChildNodes[j++].InnerText );
                    if (node.ChildNodes[j].Name == "option_id")
                    {
                        item.m_OptionID = UInt32.Parse(node.ChildNodes[j++].InnerText);
                        if (item.m_OptionID != 0)
                            item.m_Bonuses = AdditionFactory.GetBonus(item.m_OptionID);
                    }
                    if( j == i )
                        ++j;
                }
                return item;
            }

            public new int GetHashCode()
            {
                return m_ItemID;
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
        }

        private static Dictionary<int, BaseItem> m_BaseItems;

        private BaseItem m_BaseItem;
        private ushort m_Durability;
        private byte m_Amount;
        private string m_CreatorName; // TODO: Should probably be an ActorPC reference (or null for SAGA)
        private byte m_Dye;
        private uint[] m_Additions = new uint[3];
        private bool m_Equiped;
        public uint dbID;
        public int equipSlot = -1;

        public ITEM_TYPE type { get { return m_BaseItem.Type; } }
        public ITEM_TYPE2 ItemType { get { return m_BaseItem.ItemType; } }
        public int id {
            get 
            {
                return m_BaseItem.ItemID; 
            }
            set
            {
                m_BaseItem = m_BaseItems[value];

                m_CreatorName = string.Empty;
                m_Durability = maxDur;
                m_Amount = 1;
                m_Equiped = false;
            }
        }
        public uint price { get { return m_BaseItem.Price; } }
        public uint icon { get { return m_BaseItem.IconID; } }
        public uint req_addition { get { return m_BaseItem.RequiredAddition; } }
        public uint skillID { get { return m_BaseItem.SkillID; } }
        public uint req_clvl { get { return m_BaseItem.RequiredCLvl; } }
        public uint req_str { get { return m_BaseItem.RequiredStr; } }
        public uint req_dex { get { return m_BaseItem.RequiredDex; } }
        public uint req_int { get { return m_BaseItem.RequiredInt; } }
        public uint req_con { get { return m_BaseItem.RequiredCon; } }
        public uint req_luc { get { return m_BaseItem.RequiredLuk; } }
        public ushort mesh { get { return m_BaseItem.MeshID; } }
        public ushort quest { get { return m_BaseItem.Quest; } }
        public ushort maxDur { get { return m_BaseItem.MaxDurability; } }
        public ushort durability { get { return m_Durability; } set { m_Durability = value; } }
        public bool isQuestItem { get { return m_BaseItem.QuestItem; } }
        public bool tradeAble { get { return m_BaseItem.Tradeable; } }
        public bool dropAble { get { return m_BaseItem.Dropable; } }
        public bool equipAble { get { return m_BaseItem.Equipable; } }
        public EQUIP_SLOT EquipSlot { get { return m_BaseItem.EquipSlot; } }
        public string name { get { return m_BaseItem.Name; } }
        public string desc { get { return m_BaseItem.Description; } }
        public Dictionary<GenderType, bool> req_gender { get { return m_BaseItem.RequiredGender; } }
        public Dictionary<RaceType, bool> req_race { get { return m_BaseItem.RequiredRace; } }
        public Dictionary<JobType, byte> req_job { get { return m_BaseItem.RequiredJob; } }
        public Dictionary<WeaponType, byte> req_weapon { get { return m_BaseItem.RequiredWeapon; } }
        public List<Bonus> Bonus { get { return m_BaseItem.Bonuses; } }
        public byte maxStack { get { return m_BaseItem.MaxStackSize; } }
        public byte stack { get { return m_Amount; } set { m_Amount = value; } }
        public string creatorName { get { return m_CreatorName; } set { m_CreatorName = value; } }
        public uint addition1 { get { return m_Additions[0]; } }
        public uint addition2 { get { return m_Additions[1]; } }
        public uint addition3 { get { return m_Additions[2]; } }
        public bool equiped { get { return m_Equiped; } }
        public byte Dye { get { return m_Dye; } set { m_Dye = value; } }
        //req_summons
        //unknown1;unknown2

        //used internally, NOT cloned
        public byte index;
        public byte active = 1;

        public static void LoadItems( string filename )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( filename );
            XmlNodeList source = doc.GetElementsByTagName( "item" );

            m_BaseItems = new Dictionary<int, BaseItem>( source.Count );
            SagaLib.Logger.ShowInfo("Loading item database:");
            Console.ForegroundColor = ConsoleColor.Green;
            int tenperc = source.Count / 20;
            for( int i = 0; i < source.Count; ++i )
            {
                BaseItem item = BaseItem.Parse( source[i] );
                if (i % tenperc == 0)
                    Console.Write("*");
                if( item != null )
                    m_BaseItems[item.ItemID] = item;

            }
            Console.WriteLine();
            Console.ResetColor();
            SagaLib.Logger.ShowInfo(source.Count + " Items Loaded.", null);
            doc = null;
            GC.Collect();
        }

        public override string ToString()
        {
            return this.name;
        }

        public Item()
        {
            m_BaseItem = m_BaseItems[1];

            m_CreatorName = string.Empty;
            m_Durability = maxDur;
            m_Amount = 1;
            m_Equiped = false;
        }

        public Item( int itemID )
        {
            m_BaseItem = m_BaseItems[itemID];

            m_CreatorName = string.Empty;
            m_Durability = maxDur;
            m_Amount = 1;
            m_Equiped = false;
        }

        public Item( int itemID, string creatorName, ushort durability, byte amount )
        {
            m_BaseItem = m_BaseItems[itemID];

            m_CreatorName = creatorName;
            m_Durability = durability;
            m_Amount = amount;
            m_Equiped = false;
        }
    }
}
