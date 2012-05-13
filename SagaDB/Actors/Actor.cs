using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;

namespace SagaDB.Actors
{
    public enum ActorType { NONE, PC, NPC, Item };
    public enum JobType { NOVICE = 1, SWORDMAN, RECRUIT, THIEF, ENCHANTER, CLOWN, KNIGHT, ASSASSIN, SPECIALIST, SAGE, GAMBLER, FALCATA, FPRSYTHIE, NEMOPHILA, VEILCHENBLAU };
    public enum RaceType { NORMAN, ELLR, DIMAGO };
    public enum WeaponType { HAND, SHORT_SWORD, LONG_SWORD, SWORD_STICK, DAMPTFLINTE, BOW, ROD, DAMPTSCHWERTZ, KATANA };
    public enum Trading { TRADING, NOT_TRADING };
    public enum TradeStatus { NOT_CONFIRMED, LIST_CONFIRM, TRADE_CONFIRM };
    public enum Party { PENDING, IN_PARTY, NOT_IN_PARTY };

    public enum SkillType
    {
        Battle,
        Living,
        Special,
        Inactive,
    }
    [Serializable]
    public class SkillInfo
    {
        private uint id;
        public uint dbID;
        public uint exp;
        public byte slot;

        public uint ID { get { return this.id; } set { this.id = value; } }
        public uint EXP { get { return this.exp; } set { this.exp = value; } }
        public byte Slot { get { return this.slot; } set { this.slot = value; } }

        public override string ToString()
        {
            return id.ToString();
        }
    }

    [Serializable]
    public class Actor
    {
        public ActorType type;
        public byte mapID, worldID;
        public float x, y, z;
        public int yaw;
        public uint id;
        public uint region;
        public uint sightRange;
        public uint maxMoveRange;
        public bool invisble;
        public byte state;
        public Global.STANCE stance;
        public uint guild, party;
        public string name;
        public Dictionary<string, MultiRunTask> Tasks;        

        [Db4objects.Db4o.Transient]
        [System.Runtime.Serialization.OptionalField]
        public BattleStatus BattleStatus;
        [Db4objects.Db4o.Transient]
        [System.Runtime.Serialization.OptionalField]
        public ActorEventHandler e;


        public Actor()
        {
            this.type = ActorType.NONE;
        }
    }
}
