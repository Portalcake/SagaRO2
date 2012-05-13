using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;

namespace SagaDB.Actors
{
    [Serializable]
    public class BattleStatus
    {
        public int hpBasic;
        public int spBasic;
        //BattleStatus
        /// <summary>
        /// ATK
        /// </summary>
        public int atk;
        /// <summary>
        /// ATK Min. Bonus
        /// </summary>
        public int atkminbonus;
        /// <summary>
        /// ATK Max. Bonus
        /// </summary>
        public int atkmaxbonus;
        /// <summary>
        /// Ranged ATK Min. Bonus
        /// </summary>
        public int ratkminbonus;
        /// <summary>
        /// Ranged ATK Max. Bonus
        /// </summary>
        public int ratkmaxbonus;
        /// <summary>
        /// MATK Min Bonus
        /// </summary>
        public int matkminbonus;
        /// <summary>
        /// MATK Max Bonus
        /// </summary>
        public int matkmaxbonus;
        /// <summary>
        /// DEF
        /// </summary>
        public int def;
        /// <summary>
        /// MATK
        /// </summary>
        public int matk;
        /// <summary>
        ///Magical Flee 
        /// </summary>
        public int mflee;
        /// <summary>
        /// Ranged ATK
        /// </summary>
        public int ratk;
        /// <summary>
        /// Ranged Flee
        /// </summary>
        public int rflee;
        /// <summary>
        /// Hit
        /// </summary>
        public int hit;
        /// <summary>
        /// Flee
        /// </summary>
        public int flee;
        //BattleStatus Skill Bonus
        /// <summary>
        /// ATK skill bonus
        /// </summary>
        public int atkskill;
        /// <summary>
        /// DEF skill bonus
        /// </summary>
        public int defskill;
        /// <summary>
        /// MATK skill bonus
        /// </summary>
        public int matkskill;
        /// <summary>
        /// Magical Flee bonus
        /// </summary>
        public int mfleeskill;
        /// <summary>
        /// Ranged ATK skill bonus
        /// </summary>
        public int ratkskill;
        /// <summary>
        /// Ranged Flee skill bonus
        /// </summary>
        public int rfleeskill;
        /// <summary>
        /// Hit skill bonus
        /// </summary>
        public int hitskill;
        /// <summary>
        /// Flee skill bonus
        /// </summary>
        public int fleeskill;
        /// <summary>
        /// Max HP skill bonus
        /// </summary>
        public short hpskill;
        /// <summary>
        /// MaxSP skill bonus
        /// </summary>
        public short spskill;
        /// <summary>
        /// ATK Equip Bonus
        /// </summary>
        public int atkbonus;
        /// <summary>
        /// Ranged ATK Equip Bonus
        /// </summary>
        public int ratkbonus;
        /// <summary>
        /// MATK Equip Bonus
        /// </summary>
        public int matkbonus;
        /// <summary>
        /// DEF Equip Bonus
        /// </summary>
        public int defbonus;
        /// <summary>
        /// Hit Equip Bonus
        /// </summary>
        public int hitbonus;
        /// <summary>
        /// Ranged Hit Equip Bonus
        /// </summary>
        public int rhitbonus;
        /// <summary>
        /// Magical Hit Equip Bonus
        /// </summary>
        public int mhitbonus;
        /// <summary>
        /// Flee Equip Bonus
        /// </summary>
        public int fleebonus;
        /// <summary>
        /// Ranged Flee Equip Bonus
        /// </summary>
        public int rfleebonus;
        /// <summary>
        /// Magical Flee Equip Bonus
        /// </summary>
        public int mfleebonus;
        /// <summary>
        /// CRI Equip Bonus;
        /// </summary>
        public int cribonus;
        /// <summary>
        /// Ranged CRI Equip Bonus;
        /// </summary>
        public int rcribonus;
        /// <summary>
        /// Magical CRI Equip Bonus;
        /// </summary>
        public int mcribonus;

        /// <summary>
        /// STR Equip Bonus
        /// </summary>
        public int strbonus;
        /// <summary>
        /// DEX Equip Bonus
        /// </summary>
        public int dexbonus;
        /// <summary>
        /// CON Equip Bonus
        /// </summary>
        public int conbonus;
        /// <summary>
        /// INT Equip Bonus
        /// </summary>
        public int intbonus;
        /// <summary>
        /// LUK Equip Bonus
        /// </summary>
        public int lukbonus;
        /// <summary>
        /// Max HP Equip Bonus;
        /// </summary>
        public int hpbonus;
        /// <summary>
        /// Max SP Equip Bonus;
        /// </summary>
        public int spbonus;
        /// <summary>
        /// HP Regeneration Bonus
        /// </summary>
        public int hpregbonus;
        /// <summary>
        /// SP Regeneration Bonus
        /// </summary>
        public int spregbonus;
        /// <summary>
        /// HP Regeneration Skill Bonus
        /// </summary>
        public int hpregskill;
        /// <summary>
        /// SP Regeneration Skill Bonus
        /// </summary>
        public int spregskill;

        public int speedbonus;

        public int speedskill;

        //Elemental Resists
        public int holyresist;
        public int darkresist;
        public int fireresist;
        public int iceresist;
        public int windresist;
        public int curseresist;
        public int spiritresist;
        public int ghostresist;

        public List<uint> Status = new List<uint>();
        public Dictionary<string, Addition> Additions = new Dictionary<string, Addition>();
    }
}
