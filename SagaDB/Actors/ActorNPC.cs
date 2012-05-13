using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Actors
{
    [Serializable]
    public class ActorNPC : Actor
    {
        public uint npcType;
        public ushort HP, maxHP, SP, maxSP;
        public ushort cEXP, jEXP, wEXP;
        public byte level;
        public int[] aStats;
        public string scriptName;
        public int version = 1;
        public List<SagaDB.Items.Item> NPCinv ;
        public NPCAttribute Attribute;
        public struct NPCAttribute
        {
            public uint script;//the text,it will be shown by client
            /*
             * iconIDs:
             * 1:Everyday conversation
             * 2:Location Guide
             * 3:Officail Quests
             * 4:Personal Quests
             * 5:Scenario Quests
             * 6:Event Quests
             * 10:Shop
             * 11:Bunny ???
             * 12: Hands ???
             * 13: Targets ???
             * 14: Learn skill
             * 15: Enter ship
             * 16: Leave ship
             * 17: Enter train
             * 18: Leave train
             */ 
            public byte[] icons;
            public byte IconNum { get { if (icons != null) return (byte)icons.Length; else return 0; } }
        }

        public ActorNPC(uint npcType, ushort HP, ushort maxHP, ushort SP, ushort maxSP)
        {
            this.type = ActorType.NPC;
            this.npcType = npcType;
            this.HP = HP;
            this.maxHP = maxHP;
            this.SP = SP;
            this.maxSP = maxSP;
            this.aStats = new int[3];
            this.Attribute =new NPCAttribute();
            this.Attribute.icons = new byte[0];
        }

        public ActorNPC(uint npcType, ushort HP, ushort maxHP, ushort SP, ushort maxSP, string scriptName)
        {
            this.type = ActorType.NPC;
            this.npcType = npcType;
            this.HP = HP;
            this.maxHP = maxHP;
            this.SP = SP;
            this.maxSP = maxSP;
            this.scriptName = scriptName;
            this.aStats = new int[3];
            this.Attribute = new NPCAttribute();
            this.Attribute.icons = new byte[0];
        
        }

        public ActorNPC(string scriptName)
        {
            this.scriptName = scriptName;
        }
    }
}
