using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Tasks;
using SagaMap.Scripting;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
        public enum AttackElements { NEUTRAL, FIRE, ICE, WIND, CURSE, SPIRIT, GHOST, HOLY, DARK, NATRAL  }//just for now
        public enum AttackType { Magical, Physical,Ranged }

        public static uint PhysicalAttack(ref Actor sActor, ref Actor dActor, uint damage, AttackElements element,ref Map.SkillArgs args)
        {
            //Real damage calculation
            if (element != AttackElements.NATRAL)
            {
                if (damage > (uint.MaxValue / 2)) damage = 0;
                if (args.isCritical == Map.SkillArgs.AttackResult.Critical)
                {
                    damage = damage * 2;
                    args.damage = damage;
                }
                float reduced;
                if (dActor.BattleStatus.def < 1000)
                {
                    reduced = ((float)(dActor.BattleStatus.def) / 1000) * damage;
                    damage -= (uint)reduced;
                    args.damage = damage;
                }
                else
                {
                    damage = 0;
                    args.damage = 0;
                    args.isCritical = Map.SkillArgs.AttackResult.Block;
                }
            }
            //Shield Block: Damage reduction
            if (sActor.BattleStatus.Additions.ContainsKey("ShieldBlock"))
            {
                damage = (uint)(damage * 0.7);
            }
            //Shiel Block: Attack reflection
            if (dActor.BattleStatus.Additions.ContainsKey("ShieldBlock"))
            {
                Addition addition = dActor.BattleStatus.Additions["ShieldBlock"];
                
                Map.SkillArgs newarg = new Map.SkillArgs(args.skillType, args.isCritical, (uint)SkillIDs.ShieldBlock, sActor.id, 0);
                Map map;
                newarg.damage = (uint)(damage * (0.07 + 0.03));
                if (MapManager.Instance.GetMap(dActor.mapID, out map))
                {
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, newarg, dActor, true);
                    SkillAttack(ref dActor, ref sActor, newarg.damage, ref newarg);
                }   
            }
            SkillAttack(ref sActor, ref dActor, damage, ref args);
            return damage;
        }

        public static uint MagicalAttack(ref Actor sActor, ref Actor dActor, uint damage, AttackElements element, ref Map.SkillArgs args)
        {
            //TODO:
            //Damage calculation and magical resists.
            if (args.isCritical == Map.SkillArgs.AttackResult.Critical)
            {
                damage = damage * 2;
                args.damage = damage;
            }                
            SkillAttack(ref sActor, ref dActor, damage, ref args);
            return damage;
        }

        private static void SkillAttack(ref Actor sActor, ref Actor dActor, uint damage, ref Map.SkillArgs args)
        {
            ActorPC targetPC;
            ActorNPC targetNPC;
            if (dActor.stance == SagaLib.Global.STANCE.DIE)
                return;
            switch (dActor.type)
            {
                case ActorType.NPC:
                    targetNPC = (ActorNPC)dActor;
                    if (targetNPC.HP > damage) targetNPC.HP -= (ushort)damage; else targetNPC.HP = 0;
                    Mob mob = (Mob)targetNPC.e;
                    mob.BeenAttacked(sActor, args);
                    TimeSpan tmp = mob.timeSignature.time - DateTime.Now;
                    if (tmp.Minutes > 5 || tmp.Minutes < 0 || mob.timeSignature.actorID == 0 || mob.timeSignature.actorID == sActor.id)
                    {
                        mob.timeSignature.actorID = sActor.id;
                        mob.timeSignature.time = DateTime.Now;
                    }
                    if (targetNPC.HP == 0)
                    {
                        targetNPC.e.OnDie();
                        if (sActor.type == ActorType.PC)
                        {
                            targetPC = (ActorPC)sActor;
                            Quest.QuestsManager.UpdateEnemyInfo(targetPC, targetNPC.npcType);
                        }
                        mob.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, null, targetNPC, true);

                        //EXP calculation
                        foreach (uint i in mob.Damage.Keys)
                        {
                            targetPC = (ActorPC)mob.Map.GetActor(i);
                            if (targetPC == null) continue;

							ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)targetPC.e;

							// Apply player's part of the experience (determined by the percentage of HP deducted)
							ExperienceManager.Instance.ApplyExp(targetPC, targetNPC, (float)mob.Damage[i] / (float)targetNPC.maxHP);

                            eh.C.SendCharStatus(36); 

							// Check experience (will send level up if relevant)
                            ExperienceManager.Instance.CheckExp(eh.C, ExperienceManager.LevelType.CLEVEL);
                            ExperienceManager.Instance.CheckExp(eh.C, ExperienceManager.LevelType.JLEVEL);
                            ExperienceManager.Instance.CheckExp(eh.C, ExperienceManager.LevelType.WLEVEL);

							eh.C.UpdateWeaponInfo(SagaMap.Packets.Server.WeaponAdjust.Function.EXP,
												  SagaDB.Items.WeaponFactory.GetActiveWeapon(eh.C.Char).exp);
                        }
                        mob.Damage.Clear();
                    }
                    break;
                case ActorType.PC:
                    targetPC = (ActorPC)dActor;
                    if (targetPC.HP > damage) targetPC.HP -= (ushort)damage; else targetPC.HP = 0;
                    EquiptLoseDurability(targetPC);
                    if (targetPC.HP == 0)
                    {
                        ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)targetPC.e;
                        targetPC.e.OnDie();
                        eh.C.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, null, targetPC, true);

                    }

                    break;
            }
        }

        private static void EquiptLoseDurability(ActorPC pc)
        {
            if (SagaLib.Global.Random.Next(0, 99) < 10) LoseDura(pc, SagaDB.Items.EQUIP_SLOT.BODY);
            if (SagaLib.Global.Random.Next(0, 99) < 10) LoseDura(pc, SagaDB.Items.EQUIP_SLOT.HANDS);
            if (SagaLib.Global.Random.Next(0, 99) < 10) LoseDura(pc, SagaDB.Items.EQUIP_SLOT.PANTS);
            if (SagaLib.Global.Random.Next(0, 99) < 10) LoseDura(pc, SagaDB.Items.EQUIP_SLOT.LEGS);            
        }

        public static void EquiptLoseDurabilityOnDeath(ActorPC pc)
        {
            LoseDura(pc, SagaDB.Items.EQUIP_SLOT.BODY, 0.1f);
            LoseDura(pc, SagaDB.Items.EQUIP_SLOT.HANDS, 0.1f);
            LoseDura(pc, SagaDB.Items.EQUIP_SLOT.PANTS, 0.1f);
            LoseDura(pc, SagaDB.Items.EQUIP_SLOT.LEGS, 0.1f);
        }

        private static void LoseDura(ActorPC pc, SagaDB.Items.EQUIP_SLOT slot, float percentage)
        {
            Packets.Server.ItemAdjust p = new SagaMap.Packets.Server.ItemAdjust();
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if (pc.inv.EquipList.ContainsKey(slot))
                if (pc.inv.EquipList[slot].durability > 0)
                {
                    ushort lose = (ushort)((float)pc.inv.EquipList[slot].maxDur * percentage);
                    if (pc.inv.EquipList[slot].durability > lose)
                        pc.inv.EquipList[slot].durability -= lose;
                    else
                        pc.inv.EquipList[slot].durability = 0;
                    p.SetContainer(1);
                    p.SetFunction(SagaMap.Packets.Server.ItemAdjust.Function.Durability);
                    p.SetSlot((byte)slot);
                    p.SetValue(pc.inv.EquipList[slot].durability);
                    eh.C.netIO.SendPacket(p, eh.C.SessionID);
                    if (pc.inv.EquipList[slot].durability == 0)
                    {
                        pc.inv.EquipList[slot].active = 0;
                        p = new SagaMap.Packets.Server.ItemAdjust();
                        p.SetContainer(1);
                        p.SetSlot((byte)slot);
                        p.SetFunction(SagaMap.Packets.Server.ItemAdjust.Function.Active);
                        p.SetValue(0);
                        eh.C.netIO.SendPacket(p, eh.C.SessionID);
                        Bonus.BonusHandler.Instance.EquiqItem(pc, pc.inv.EquipList[slot], true);
                        SkillHandler.CalcHPSP(ref pc);
                        eh.C.SendCharStatus(0);
                        eh.C.SendExtStats();
                        eh.C.SendBattleStatus();
                        MapServer.charDB.UpdateItem(pc, pc.inv.EquipList[slot]);
                    }
                }
        }

        private static void LoseDura(ActorPC pc, SagaDB.Items.EQUIP_SLOT slot)
        {
            Packets.Server.ItemAdjust p = new SagaMap.Packets.Server.ItemAdjust();
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if (pc.inv.EquipList.ContainsKey(slot))
                if (pc.inv.EquipList[slot].durability > 0)
                {
                    pc.inv.EquipList[slot].durability--;
                    p.SetContainer(1);
                    p.SetFunction(SagaMap.Packets.Server.ItemAdjust.Function.Durability);
                    p.SetSlot((byte)slot);
                    p.SetValue(pc.inv.EquipList[slot].durability);
                    eh.C.netIO.SendPacket(p, eh.C.SessionID);
                    if (pc.inv.EquipList[slot].durability == 0)
                    {
                        pc.inv.EquipList[slot].active = 0;
                        p = new SagaMap.Packets.Server.ItemAdjust();
                        p.SetContainer(1);
                        p.SetSlot((byte)slot);
                        p.SetFunction(SagaMap.Packets.Server.ItemAdjust.Function.Active);
                        p.SetValue(0);
                        eh.C.netIO.SendPacket(p, eh.C.SessionID);
                        Bonus.BonusHandler.Instance.EquiqItem(pc, pc.inv.EquipList[slot], true);
                        SkillHandler.CalcHPSP(ref pc);
                        eh.C.SendCharStatus(0);
                        eh.C.SendExtStats();
                        eh.C.SendBattleStatus();
                        MapServer.charDB.UpdateItem(pc, pc.inv.EquipList[slot]);
                    }
                }

        }
       
    }
}
