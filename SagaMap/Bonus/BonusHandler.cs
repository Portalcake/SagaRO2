using System;
using System.Collections.Generic;
using System.Text;

using SagaMap;
using SagaDB;
using SagaDB.Items;
using SagaDB.Actors;
namespace SagaMap.Bonus
{
    public class BonusHandler
    {
        private enum BonusType
        {
            Equipment,
            Skill,
        }
        public void CalcEquipBonus(ActorPC pc)
        {
            ClearBonus(pc);
            Actor actor = (Actor)pc;
            Skills.SkillHandler.CalcBattleStatus(ref actor);
            foreach (Item i in pc.inv.EquipList.Values)
            {
                if (i.durability > 0) EquiqItem(pc, i, false);
            }
        }

        private void ClearBonus(ActorPC pc)
        {
            if (pc.BattleStatus == null) pc.BattleStatus = new BattleStatus();
            pc.BattleStatus.atkbonus = 0;
            pc.BattleStatus.conbonus = 0;
            pc.BattleStatus.cribonus = 0;
            pc.BattleStatus.defbonus = 0;
            pc.BattleStatus.dexbonus = 0;
            pc.BattleStatus.fleebonus = 0;
            pc.BattleStatus.hitbonus = 0;
            pc.BattleStatus.hpbonus = 0;
            pc.BattleStatus.intbonus = 0;
            pc.BattleStatus.lukbonus = 0;
            pc.BattleStatus.matkbonus = 0;
            pc.BattleStatus.mcribonus = 0;
            pc.BattleStatus.mfleebonus = 0;
            pc.BattleStatus.mhitbonus = 0;
            pc.BattleStatus.ratkbonus = 0;
            pc.BattleStatus.rcribonus = 0;
            pc.BattleStatus.rfleebonus = 0;
            pc.BattleStatus.rhitbonus = 0;
            pc.BattleStatus.spbonus = 0;
            pc.BattleStatus.strbonus = 0;
            pc.BattleStatus.hpbonus = 0;
            pc.BattleStatus.spbonus = 0;
            pc.BattleStatus.hpregbonus = 0;
            pc.BattleStatus.spregbonus = 0;
            pc.BattleStatus.speedbonus = 0;
        }

        public void EquiqItem(ActorPC pc, Item item, bool unequip)
        {
            if (item.Bonus == null) return;
            if (item.durability == 0) return;
            foreach (SagaDB.Items.Bonus i in item.Bonus)
            {
                AddBonus(pc, i, BonusType.Equipment, unequip, (uint)item.id);
            }
        }

        public void SkillAddAddition(Actor pc, uint id, bool deactivate)
        {
            Skills.Skill info = Skills.SkillFactory.GetSkill(id);
            AddAddition(pc, info.addition, deactivate);
        }

        public void AddAddition(Actor pc, uint id, bool deactivate)
        {
            List<SagaDB.Items.Bonus> bonus = AdditionFactory.GetBonus(id);
            foreach (SagaDB.Items.Bonus i in bonus)
            {
                AddBonus(pc, i, BonusType.Skill, deactivate, id);
            }
        }

        private void AddBonus(Actor actor, SagaDB.Items.Bonus i, BonusType type, bool unequip, uint id)
        {
            bool isRate = false;
            ActorPC pc = null;
            ActorEventHandlers.PC_EventHandler eh = null;
            if (actor.type == ActorType.PC)
            {
                pc = (ActorPC)actor;
                eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            }
            if (i.Value > 20000 || i.Value < -20000)
            {
                isRate = true;
            }
            switch (i.Effect)
            {
                case ADDITION_BONUS.STR:
                    if (actor.type != ActorType.PC || type != BonusType.Equipment) return;
                    pc.BattleStatus.strbonus += AddInt(i.Value, isRate, pc.str, unequip);
                    break;
                case ADDITION_BONUS.DEX:
                    if (actor.type != ActorType.PC || type != BonusType.Equipment) return;
                    pc.BattleStatus.dexbonus += AddInt(i.Value, isRate, pc.dex, unequip);
                    break;
                case ADDITION_BONUS.CON:
                    if (actor.type != ActorType.PC || type != BonusType.Equipment) return;
                    pc.BattleStatus.conbonus += AddInt(i.Value, isRate, pc.con, unequip);
                    break;
                case ADDITION_BONUS.INT:
                    if (actor.type != ActorType.PC || type != BonusType.Equipment) return;
                    pc.BattleStatus.intbonus += AddInt(i.Value, isRate, pc.intel, unequip);
                    break;
                case ADDITION_BONUS.LUK:
                    if (actor.type != ActorType.PC || type != BonusType.Equipment) return;
                    pc.BattleStatus.lukbonus += AddInt(i.Value, isRate, pc.luk, unequip);
                    break;
                case ADDITION_BONUS.PhysicalDef:
                    if (type == BonusType.Equipment)
                        actor.BattleStatus.defbonus += AddInt(i.Value, isRate, actor.BattleStatus.def, unequip);
                    if (type == BonusType.Skill)
                        actor.BattleStatus.defskill += AddInt(i.Value, isRate, actor.BattleStatus.def, unequip);
                    break;
                case ADDITION_BONUS.HPMax:
                    if (actor.type != ActorType.PC) return;
                    if (type == BonusType.Equipment)
                        pc.BattleStatus.hpbonus += AddInt(i.Value, isRate, pc.BattleStatus.hpBasic, unequip);
                    if (type == BonusType.Skill)
                        pc.BattleStatus.hpskill += (short)AddInt(i.Value, isRate, pc.BattleStatus.hpBasic, unequip);
                    Skills.SkillHandler.CalcHPSP(ref pc);
                    break;
                case ADDITION_BONUS.SPMax:
                    if (actor.type != ActorType.PC) return;
                    if (type == BonusType.Equipment)
                        pc.BattleStatus.spbonus += AddInt(i.Value, isRate, pc.BattleStatus.spBasic, unequip);
                    if (type == BonusType.Skill)
                        pc.BattleStatus.spskill += (short)AddInt(i.Value, isRate, pc.BattleStatus.spBasic, unequip);
                    Skills.SkillHandler.CalcHPSP(ref pc);
                    break;
                case ADDITION_BONUS.HPRecover:
                    if (pc.Tasks.ContainsKey("RegenerationHP"))
                    {
                        Tasks.Regeneration hp = (Tasks.Regeneration)pc.Tasks["RegenerationHP"];
                        if (type == BonusType.Equipment)
                            pc.BattleStatus.hpregbonus += AddInt(i.Value, isRate, hp.hp, unequip);
                        if (type == BonusType.Skill)
                            pc.BattleStatus.hpregskill += AddInt(i.Value, isRate, hp.hp, unequip);
                    }
                    break;
                case ADDITION_BONUS.SPRecover:
                    if (pc.Tasks.ContainsKey("RegenerationSP"))
                    {
                        Tasks.Regeneration sp = (Tasks.Regeneration)pc.Tasks["RegenerationSP"];
                        if (type == BonusType.Equipment)
                            pc.BattleStatus.spregbonus += AddInt(i.Value, isRate, sp.hp, unequip);
                        if (type == BonusType.Skill)
                            pc.BattleStatus.spregskill += AddInt(i.Value, isRate, sp.hp, unequip);
                    }
                    break;
                case ADDITION_BONUS.PhysicalFlee:
                    if (type == BonusType.Equipment)
                        actor.BattleStatus.fleebonus += AddInt(i.Value, isRate, actor.BattleStatus.flee, unequip);
                    if (type == BonusType.Skill)
                        actor.BattleStatus.fleeskill += AddInt(i.Value, isRate, actor.BattleStatus.flee, unequip);
                    break;
                case ADDITION_BONUS.RangedFlee:
                    if (type == BonusType.Equipment)
                        actor.BattleStatus.rfleebonus += AddInt(i.Value, isRate, actor.BattleStatus.rflee, unequip);
                    if (type == BonusType.Skill)
                        actor.BattleStatus.rfleeskill += AddInt(i.Value, isRate, actor.BattleStatus.rflee, unequip);
                    break;
                case ADDITION_BONUS.MagicalFlee:
                    if (type == BonusType.Equipment)
                        actor.BattleStatus.mfleebonus += AddInt(i.Value, isRate, actor.BattleStatus.mflee, unequip);
                    if (type == BonusType.Skill)
                        actor.BattleStatus.mfleeskill += AddInt(i.Value, isRate, actor.BattleStatus.mflee, unequip);
                    break;
                case ADDITION_BONUS.PhysicalATK:
                    int min, max;
                    if (actor.type != ActorType.PC) return;
                    Skills.SkillHandler.GetPCATK(pc, out min, out max);
                    if (type == BonusType.Equipment)
                        actor.BattleStatus.atkbonus += AddInt(i.Value, isRate, max, unequip);
                    if (type == BonusType.Skill)
                        actor.BattleStatus.atkskill += AddInt(i.Value, isRate, max, unequip);
                    Skills.SkillHandler.CalcAtk(ref actor);
                    break;
                case ADDITION_BONUS.MaxWDamage:
                case ADDITION_BONUS.AtkMax:
                    if (type == BonusType.Skill)
                        actor.BattleStatus.atkmaxbonus += AddInt(i.Value, isRate, 0, unequip);
                    break;
                case ADDITION_BONUS.MinWDamage:
                case ADDITION_BONUS.AtkMin:
                    if (type == BonusType.Skill)
                        actor.BattleStatus.atkminbonus += AddInt(i.Value, isRate, 0, unequip);
                    break;
                case ADDITION_BONUS.MaxMWDamage:
                case ADDITION_BONUS.MagicalAtkMax:
                    if (type == BonusType.Skill)
                        actor.BattleStatus.matkmaxbonus += AddInt(i.Value, isRate, 0, unequip);
                    break;
                case ADDITION_BONUS.MinMWDamage:
                case ADDITION_BONUS.MagicalAtkMin:
                    if (type == BonusType.Skill)
                        actor.BattleStatus.matkminbonus += AddInt(i.Value, isRate, 0, unequip);
                    break;
                case ADDITION_BONUS.MaxRWDamage:
                case ADDITION_BONUS.RangedAtkMax:
                    if (type == BonusType.Skill)
                        actor.BattleStatus.ratkmaxbonus += AddInt(i.Value, isRate, 0, unequip);
                    break;
                case ADDITION_BONUS.WalkSpeed:
                    if (actor.type == ActorType.PC)
                    {
                        if (type == BonusType.Skill)
                            actor.BattleStatus.speedskill += AddInt(i.Value, isRate, 500, unequip);
                        else
                            actor.BattleStatus.speedbonus += AddInt(i.Value, isRate, 500, unequip);
                    }
                    else
                    {
                        Scripting.Mob mob = (Scripting.Mob)actor.e;
                        mob.WalkBonus += AddInt(i.Value, isRate, mob.mWalkSpeed, unequip);
                        mob.RunBonus += AddInt(i.Value, isRate, mob.mRunSpeed, unequip);
                    }
                    break;

                case ADDITION_BONUS.ItemHPRecover:
                    if (actor.type == ActorType.PC)
                    {
                        int value = SagaLib.Global.Random.Next(i.Value, (int)(i.Value * (1.5)));
                        pc.HP = (ushort)(pc.HP + value);
                        if (pc.HP > pc.maxHP) pc.HP = pc.maxHP;
                        eh.C.SendCharStatus(0);
                        eh.C.SendSkillEffect(id, SagaMap.Skills.SkillEffects.HP, (uint)value);
                    }
                    break;
                case ADDITION_BONUS.ItemSPRecover:
                    if (actor.type == ActorType.PC)
                    {
                        int value = SagaLib.Global.Random.Next(i.Value, (int)(i.Value * (1.5)));
                        pc.HP = (ushort)(pc.SP + value);
                        if (pc.SP > pc.maxHP) pc.SP = pc.maxSP;
                        eh.C.SendCharStatus(0);
                        eh.C.SendSkillEffect(id, SagaMap.Skills.SkillEffects.SP, (uint)value);
                    }
                    break;
            }
        }

        private int AddInt(int value, bool isRate, int rateBase,bool unequip)
        {
            if (isRate == true)
            {
                if (value > 0) value -= 20000; else value += 20000;
                if (unequip)
                    return -((rateBase * value) / 1000);
                else
                    return ((rateBase * value) / 1000);                
            }
            else
            {
                if (unequip)
                    return -value;
                else
                    return value;
            }
        }

       

        public static BonusHandler Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly BonusHandler instance = new BonusHandler();
        }
    }
}
