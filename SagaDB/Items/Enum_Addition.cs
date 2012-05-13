using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Items
{
    [Serializable]
    public enum ADDITION_BONUS
    {
        Null = 0,
        AtkMin = 500,
        RangedAtkMin = 501,
        MagicalAtkMin = 502,
        AtkMax = 509,
        RangedAtkMax = 510,
        MagicalAtkMax = 511,
        MaxWDamage = 518,
        MinRWDamage = 519,
        MinMWDamage,
        MinWDamage,
        MaxRWDamage,
        MaxMWDamage,
        PhysicalATK = 524,
        RangedATK = 525,
        MagicalATK = 526,
        PhysicalHit = 533,
        RangedHit = 534,
        MagicalHit = 535,
        PhysicalFlee = 536,
        RangedFlee = 537,
        MagicalFlee = 538,
        PhysicalCri = 539,
        RangedCri = 540,
        MagicalCri = 541,
        STR = 545,
        DEX = 546,
        INT = 547,
        CON = 548,
        LUK = 549,
        HPMax = 550,
        SPMax = 551,
        OxygenMax = 552,
        CurrentHP = 553,
        CurrentSP = 554,
        OxygenMax2 = 555,
        LPPoint = 556,
        HPRecover = 561,
        SPRecover = 562,
        FireResist = 567,
        IceResist = 568,
        ThunderResist = 569,
        HolyResist = 570,
        DarkResist,
        SoulResist = 573,
        SpiritResist,
        WalkSpeed = 576,
        CastTime,
        PhysicalDef = 579,
        MDef,
        AtkSpeed = 581,
        PhysicalBlock = 582,
        ShieldPhysicalBlock = 584,
        MagicBlock,
        DropRate = 586,
        PhysicalATK2 = 594,
        RangedATK2 = 595,
        MagicalATK2 = 596,
        HPRecoverRate = 601,
        SPRecoverRate = 602,
        CExpRate = 613,
        JExpRate,
        FireAtk = 662,
        ItemHPRecover = 664,
        ItemSPRecover = 665,
        Def = 645,
    }
    /*public enum ADDITION_BONUS
    {
        PhysicalATKMin = 1,
        RangedATKMin,
        PhysicalATKMax = 9,
        RangedATKMax,
        PhysicalWATKMin = 17,
        RangedWATKMin,
        MagicalWATKMin,
        PhysicalWATKMax,
        RangedWATKMax,
        MagicalWATKMax,
        PhysicalATK,
        RangedATK,
        MagicalATK,
        FireATK,
        IceATK,
        WindATK,
        HolyATK,
        DarkATK,
        PhysicalHit = 31,
        RangedHit,
        MagicalHit,
        PhysicalFlee,
        RangedFlee,
        MagicalFlee,
        PhysicalCri,
        RangedCri,
        MagicalCri,
        STR,
        DEX,
        INT,
        CON,
        LUK,
        HPMax,
        SPMax,
        Oxygen,
        HP,
        SP,
        LP = 51,
        HPRecover,
        SPRecover,
        FireResist = 55,
        IceResist,
        WindResist,
        HolyResist,
        CurseResist,
        MoveSpeed = 62,
        CastingTime = 64,
        CoolDownTime,
        Def,
        ASPD = 68,
        PhysicalBlock = 71,
        MagicalBlock,
        DropRate,
        CannotMove = 80,
        NoSkill = 115,
        DarkResist = 120,
        Hate = 157,
        MagicalATKMin = 219,
        MagicalATKMax,
        PhysicalCri201 = 252,
    }
     */
    /*
    public enum ADDITION_BONUS
    {
        Null = 0,
        PhysicalATKMin = 1,     //Min. P. Atk +1%       //Min. P. Atk +1
        RangedATKMin = 2,
        PhysicalATKMax = 9,     //Max. P. ATK +1%       //Max. P. Atk +1 
        RangedATKMax = 10,      //Max. P. R Atk +1%     //Max. P. R Atk +1
        MagicalATKMin = 219,    //min. M. Attack
        MagicalATKMax = 220,    //Max. M Atk +1 
        PhysicalWATKMin = 17,   //(weapon) Min. P. Atk
        RangedWATKMin = 18,     //(weapon) Min. P. R Atk
        MagicalWAtkMin = 19,    //(weapon) Min. M. Atk
        PhysicalWAtkMax = 20,   //(weapon) Max. P. Atk 
        RangedWATKMax = 21,     //(weapon) Max. P. R Atk
        MagicalWATKMax = 22,    //(weapon) Max. M Atk
        PhysicalATK = 23,       //P. Atk 
        RangedATK = 24,         //P. R Atk 
        MagicalATK = 217,       //M Atk   
        FireATK = 25,           //or 15 Fire Attack power +
        IceATK = 26,            //or 16 Ice attack power +
        LightingATK = 27,       //or 17 Lightning Attack power +21
        HolyATK = 28,           //or 18 Holy Attack power +
        DarkATK = 29,           //or 19 Dark Attack power +
        PhysicalHit = 31,       //or 21 Melee P. Attack Hitrate
        RangedHit = 32,         //or 22 P. R Attack Hitrate
        MagicalHit = 33,        //or 23 Magic Hitrate 
        PhysicalFlee = 34,      //or 24 //P. Attack Evasion 
        RangedFlee = 35,        //or 25 //P. R Evasion rate
        MagicalFlee = 36,       //or 26 //Magic Evasion
        PhysicalCri = 37,       //or 27 //P. Skill Hitrate
        RangedCri = 38,         //or 28 //P. R Skill Hitrate
        MagicalCri = 39,        //or 29 //Magic Skill Hitrate
        //CoolDownTime = 37,    //P. Attack Skill cooldown time +4 (seen used on a stick & sword) not sure if it belongs here though)
        STR = 40,               //STR + 1
        DEX = 41,               //DEX + 1
        INT = 42,               //INT + 1 
        CON = 43,               //CON + 1   
        LUK = 44,               //LUK +1
        HPMax = 45,             //Max. P. STR   //or 38 = Max. P. STR
        SPMax = 46,             //Max. SP 
        OxygenMax = 47,         //Max. Breath capacity
        HP = 48,                //P. STR
        SP = 49,                //Magic Power
        Oxygen = 50,            //Breath
        LP = 51,
        HPRecover = 52,         //HP Recovery quantity
        SPRecover = 53,         //SP Recovery quantity
        FireResist = 55,        //Fire Resistance
        IceResist = 56,         //Ice Resistance
        WindResist = 57,        //Thunder Resistance
        HolyResist = 58,        //Holy Resist
        DarkResist = 59,        //Dark Resistance (also soul or gost)
        //SoulResist = 60,          //not consistant
        //GostResist = 61,          //not consistant
        MoveSpeed = 62,         //Walk Speed
        CastingTime = 64,       //Cast time Decrease
        Def = 67,               //P. Defence
        ASPD = 68,              //Attack Speed  
        ShieldBlockrate = 69,   //Shield Blockrate
        MagicBlock = 70,        //Magic Blockrate
        PhysicalBlock = 71,     //P. Blockrate
        //PhysicalBlock2 = 72,  //Seen used in desc as P. Blockrate or Magic Blockrate (Guess: might be for (arrows) range?)
        DropRate = 73,          //Item drop rate
        CannotMove = 80,
        //NoSkill = 115,        //115 is not an Item bonus (may be a requirement)
        //Hate = 157,           //157 is not an Item bonus (may be a requirement)
        PhysicalCri201 = 252,   //P. Skill Hitrate
        AdvancedStance
    }*/
}
