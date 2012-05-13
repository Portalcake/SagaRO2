using System;
using System.Collections.Generic;
using System.Text;

using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skills;
using SagaMap.Tasks;

namespace SagaMap.Skills
{
    public enum SkillEffects
    {
        HP = 1,
        SP,
        LP
    }

    public enum PassiveStatusAddResult
    {
        WeaponMissMatch,
        OK,
        Updated,
    }
    public static partial class SkillHandler
    {
        public enum SkillAddResault
        {
            OK,
            FAILED=43, 
            NOT_ENOUGH_SKILL_EXP,
            PREVIOUS_SKILL_NOT_FOUND,
            ALREADY,
        }
        
        internal delegate void SkillCommand(ref Actor sActor,ref Actor dActor,ref Map.SkillArgs args);
        internal static Dictionary<SkillIDs, SkillCommand> SkillCommands = new Dictionary<SkillIDs, SkillCommand>();
        
        public static void CastSkill(ref Actor sActor,ref Actor dActor, ref Map.SkillArgs args)
        {
            SkillCommand command;
            if (sActor == null || dActor == null) return;
            if (dActor.stance == Global.STANCE.DIE)
            {
                args.failed = true;
                return;
            }
            CalcBattleStatus(ref sActor);
            CalcBattleStatus(ref dActor);
            
              if (SkillCommands.ContainsKey(args.skillID) == false)
                {
                    if (args.skillType == 8)
                    {
                        Bonus.BonusHandler.Instance.SkillAddAddition(dActor, (uint)args.skillID, false);
                    }
                    else
                    {
                        Logger.ShowWarning("SkillHandler not defined for skilltype:" + args.skillType + " skillid:" + args.skillID, null);
                        args.damage = CalcDamage(ref sActor, ref dActor, args);
                        args.isCritical = IsCritical(ref sActor, ref dActor, args);
                        PhysicalAttack(ref sActor, ref sActor, args.damage, AttackElements.NEUTRAL, ref args);
                    }
                }
                else
                {
                    try
                    {
                        command = SkillCommands[args.skillID];
                        command.Invoke(ref sActor, ref dActor, ref args);
                    }
                    catch(Exception ex)
                    {
                        Logger.ShowError("Exception while Actor:" + sActor.id + "is trying to cast:" + args.skillID, null);
                        Logger.ShowError(ex, null);
                    }
                }
           
 
      }

        public static void CastPassivSkill(ref ActorPC pc)
        {
            SkillCommand command;
            byte skilltype;
            Actor target = (Actor)pc;
            CalcBattleStatus(ref target);
            foreach (uint id in pc.BattleSkills.Keys)
            {
                skilltype = SkillFactory.GetSkill(id).skilltype;
                if (skilltype != 2) continue;

                if (SkillCommands.ContainsKey((SkillIDs)id) == true)
                {
                    Map.SkillArgs args = new Map.SkillArgs(skilltype, 0, id, 0, 0);
                    command = SkillCommands[(SkillIDs)id];
                    command.Invoke(ref target, ref target, ref args);
                }

            }
         }

        /// <summary>
        /// Calculte the battlestatus of an actor 
        /// </summary>
        /// <param name="actor"></param>
        public static void CalcBattleStatus(ref Actor actor)
        {
            if (actor.BattleStatus == null) actor.BattleStatus = new BattleStatus();
            CalcAtk(ref actor);
            CalcMAtk(ref actor);
            CalcDef(ref actor);
            CalcMDef(ref actor);
            CalcHit(ref actor);
            CalcFlee(ref actor);

        }
        /// <summary>
        /// Damage calculation for skills not defined in skill table
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="dActor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static uint CalcDamage(ref Actor sActor,ref Actor dActor,Map.SkillArgs args)
        {
            ActorPC targetPC;
            switch (args.skillID)
            {
                case SkillIDs.RedPotion :
                    targetPC = (ActorPC)sActor;
                    PCPotionHeal(ref targetPC, 20, 25, 0, 0, 10, 1500, args);
                    return 0;
                case SkillIDs.RedPotion2 :
                    targetPC = (ActorPC)sActor;
                    PCPotionHeal(ref targetPC, 30, 40, 0, 0, 10, 1500, args);
                    return 0;
                case  SkillIDs.RedPotion3 :
                    targetPC = (ActorPC)sActor;
                    PCPotionHeal(ref targetPC, 50, 70, 0, 0, 10, 1500, args);
                    return 0;
                default :
                    if (dActor.type != ActorType.NPC) return 10;
                    return (uint)Global.Random.Next(1, 10);
                    
            }
            
        }

        /// <summary>
        /// Calculate the Attack result of a attack(miss,normal,critical)
        /// </summary>
        /// <param name="sActor">Source Actor</param>
        /// <param name="dActor">Target Actor</param>
        /// <param name="args">Argument contains skill infomation</param>
        /// <param name="type">Attack type</param>
        /// <returns></returns>
        public static Map.SkillArgs.AttackResult CalcCrit(Actor sActor, Actor dActor, Map.SkillArgs args, Skills.SkillHandler.AttackType type)
        {
            int balace;
            Map.SkillArgs.AttackResult result;
            balace = (int)(sActor.BattleStatus.hit - dActor.BattleStatus.flee);
            balace = 50 + (int)((float)((float)balace / (float)(sActor.BattleStatus.hit * 2)) * 50);
            if (balace < 5) balace = 5;
            //hit rate calculation
            if (Global.Random.Next(0, 99) <= 94) result = Map.SkillArgs.AttackResult.Normal; else result = Map.SkillArgs.AttackResult.Miss;
            //block rate calculation
            if (result != Map.SkillArgs.AttackResult.Miss)
            {
                if (Global.Random.Next(0, 99) > 90) result = Map.SkillArgs.AttackResult.Block;            
            }
            ushort value = 0;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                switch (type)
                {
                    case SkillHandler.AttackType.Physical:
                        value = pc.dex;
                        break;
                    case SkillHandler.AttackType.Magical:
                    case SkillHandler.AttackType.Ranged:
                        value = pc.con;
                        break;
                }
            }
            else
                value = 10;
            if (result == Map.SkillArgs.AttackResult.Normal)
            {
                if (Global.Random.Next(0, 99) <= 1 + (value / 10)) result =  Map.SkillArgs.AttackResult.Critical;//20% for critical
            }
            return result;
        }

        public static int GetSkillAtkBonus(SkillIDs id)
        {
            Actor tmpActor = new Actor();
            tmpActor.BattleStatus = new BattleStatus();
            Bonus.BonusHandler.Instance.SkillAddAddition(tmpActor, (uint)id, false);
            int tmpBonus = 0;
            if (tmpActor.BattleStatus.atkmaxbonus > tmpActor.BattleStatus.atkminbonus)
            {
                tmpBonus = Global.Random.Next(tmpActor.BattleStatus.atkminbonus, tmpActor.BattleStatus.atkmaxbonus);
            }
            return tmpActor.BattleStatus.atkbonus + tmpActor.BattleStatus.atkskill + tmpBonus;
        }

        public static int GetSkillRAtkBonus(SkillIDs id)
        {
            Actor tmpActor = new Actor();
            tmpActor.BattleStatus = new BattleStatus();
            Bonus.BonusHandler.Instance.SkillAddAddition(tmpActor, (uint)id, false);
            int tmpBonus = 0;
            if(tmpActor.BattleStatus.ratkmaxbonus > tmpActor.BattleStatus.ratkminbonus)
            {
                tmpBonus = Global.Random.Next(tmpActor.BattleStatus.ratkminbonus, tmpActor.BattleStatus.ratkmaxbonus);
            }
            return tmpActor.BattleStatus.ratkbonus + tmpActor.BattleStatus.ratkskill + tmpBonus;
        }

        public static int GetSkillMAtkBonus(SkillIDs id)
        {
            Actor tmpActor = new Actor();
            tmpActor.BattleStatus = new BattleStatus();
            Bonus.BonusHandler.Instance.SkillAddAddition(tmpActor, (uint)id, false);
            int tmpBonus = 0;
            if (tmpActor.BattleStatus.matkmaxbonus > tmpActor.BattleStatus.matkminbonus)
            {
                tmpBonus = Global.Random.Next(tmpActor.BattleStatus.matkminbonus, tmpActor.BattleStatus.matkmaxbonus);
            }
            return tmpActor.BattleStatus.matkbonus + tmpActor.BattleStatus.matkskill + tmpBonus;
        }

        private static void PCPotionHeal(ref ActorPC targetPC,ushort hp, ushort hp2,ushort sp,ushort sp2,int lifetime,int period,Map.SkillArgs args)
        {
            MultiRunTask newtask;
            newtask = new Tasks.PotionHeal(targetPC, hp, hp2, sp, sp2, period, lifetime, (uint)args.skillID, args.skillType);
            newtask.Activate();
            AddStatusIcon(targetPC, (uint)args.skillID, 15000);
            if (targetPC.Tasks.ContainsKey("PotionHeal") == false) targetPC.Tasks.Add("PotionHeal", newtask);
                    
        }

        public static Map.SkillArgs.AttackResult IsCritical(ref Actor sActor, ref Actor dActor, Map.SkillArgs args)
        {
            //this actually not only for critical
            //0 for normal damage
            //1 for critical damage
            //2 for miss
            //6 probaly for heal
            //7 for no damage
            int test = Global.Random.Next(0, 21);
            switch (args.skillID)
            {
                case SkillIDs.Heal :
                    return  Map.SkillArgs.AttackResult.Heal;
                default :
                    if (test < 7) return 0;
                    else if (test >= 7 && test < 14) return  Map.SkillArgs.AttackResult.Critical ;
                    else if (test >= 14) return  Map.SkillArgs.AttackResult.Miss;//probably for miss
                    return 0;
            }           
        }

        /// <summary>
        /// Learn a skill
        /// </summary>
        /// <param name="sActor">Player</param>
        /// <param name="skillid">Skill to be learned</param>
        /// <returns></returns>
        public static SkillAddResault SkillAddSpecial(ref ActorPC sActor, uint skillid)
        {
            if (sActor.BattleSkills.ContainsKey(skillid)) return SkillAddResault.ALREADY;
            uint baseid = ((skillid / 100) * 100) + 1;
            if (skillid != baseid)
            {
                if (!sActor.BattleSkills.ContainsKey(skillid - 1)) return SkillAddResault.PREVIOUS_SKILL_NOT_FOUND;
                Skills.Skill nskill = Skills.SkillFactory.GetSkill(skillid - 1);
                if (sActor.BattleSkills[skillid - 1].exp != nskill.maxsxp) return SkillAddResault.NOT_ENOUGH_SKILL_EXP;
                SkillInfo info = sActor.BattleSkills[skillid - 1];
                MapServer.charDB.DeleteSkill(sActor, sActor.BattleSkills[skillid - 1]);
                info.ID = skillid;
                sActor.BattleSkills.Remove(skillid - 1);
                sActor.BattleSkills.Add(skillid, info);
                MapServer.charDB.NewSkill(sActor, SkillType.Battle, info);
                return SkillAddResault.OK;
            }
            else
            {
                SkillInfo info = new SkillInfo();
                info.ID = skillid;                
                sActor.BattleSkills.Add(skillid, info);
                MapServer.charDB.NewSkill(sActor, SkillType.Battle,info);
                CastPassivSkill(ref sActor);
                return SkillAddResault.OK;
            }
        }

        /// <summary>
        /// Add a Status Icon to an actor
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="skillid"></param>
        /// <param name="time"></param>
        public static void AddStatusIcon(Actor actor, uint skillid, uint time)
        {
            if (actor.BattleStatus.Status == null) actor.BattleStatus.Status = new List<uint>();
            actor.BattleStatus.Status.Add(skillid);
            Map map;
            if (actor.mapID == 0)
            {
                if (actor.type == ActorType.PC)
                {
                    ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)actor.e;
                    actor.mapID = (byte)eh.C.map.ID;
                }
            }
            if (!MapManager.Instance.GetMap(actor.mapID, out map)) return;
            List<Map.StatusArgs.StatusInfo> SList=new List<Map.StatusArgs.StatusInfo>();
            SList.Add(new Map.StatusArgs.StatusInfo(skillid,time));
            Map.StatusArgs args = new Map.StatusArgs(Map.StatusArgs.EventType.Add, SList);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, args, actor, true);
        }

        /// <summary>
        /// Send all status icons of a Actor to player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="actor">Actor</param>
        public static void SendAllStatusIcons(ActorPC pc, Actor actor)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            if (actor.BattleStatus == null) actor.BattleStatus = new BattleStatus();
            if (actor.BattleStatus.Status == null) actor.BattleStatus.Status = new List<uint>();
            List<Map.StatusArgs.StatusInfo> SList = new List<Map.StatusArgs.StatusInfo>();
            foreach (uint i in actor.BattleStatus.Status)
            {
                SList.Add(new Map.StatusArgs.StatusInfo(i, 0));
            }
            Map.StatusArgs args = new Map.StatusArgs(Map.StatusArgs.EventType.Add, SList);
            if (SList.Count != 0) eh.OnChangeStatus(actor, args);
        }

        /// <summary>
        /// Remove a status icon of a actor
        /// </summary>
        /// <param name="actor">Actor</param>
        /// <param name="skillid">Icon to be removed</param>
        public static void RemoveStatusIcon(Actor actor, uint skillid)
        {
            if (actor.BattleStatus.Status == null) actor.BattleStatus.Status = new List<uint>();
            if (!actor.BattleStatus.Status.Contains(skillid)) return;
            actor.BattleStatus.Status.Remove(skillid);
            Map map;
            if (actor.mapID == 0)
            {
                if (actor.type == ActorType.PC)
                {
                    ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)actor.e;
                    actor.mapID = (byte)eh.C.map.ID;
                }
            }
            if (!MapManager.Instance.GetMap(actor.mapID, out map)) return;
            List<Map.StatusArgs.StatusInfo> SList = new List<Map.StatusArgs.StatusInfo>();
            SList.Add(new Map.StatusArgs.StatusInfo(skillid, 0));
            Map.StatusArgs args = new Map.StatusArgs(Map.StatusArgs.EventType.Remove, SList);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, args, actor, true);
        }
        /// <summary>
        /// Add a skill to Skill List
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillid">Skill to be added</param>
        /// <param name="slot">Slot</param>
        public static void SendAddSkill(ActorPC pc, uint skillid, byte slot)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.SkillAdd p1 = new SagaMap.Packets.Server.SkillAdd();
            p1.SetSkill(skillid);
            p1.SetSlot(slot);
            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
        }
        /// <summary>
        /// Delete a skill from Skill list
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillid">Skill to be removed</param>
        public static void SendDeleteSkill(ActorPC pc, uint skillid)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.SkillDelete p1 = new SagaMap.Packets.Server.SkillDelete();
            p1.SetSkill(skillid);
            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
        }
        /// <summary>
        /// Delete a skill from Special skill list
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillid">Skill to be removed</param>
        public static void SendDeleteSpecial(ActorPC pc, uint skillid)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Packets.Server.RemoveSpecialSkill p1 = new SagaMap.Packets.Server.RemoveSpecialSkill();
            p1.SetSkill(skillid);
            eh.C.netIO.SendPacket(p1, eh.C.SessionID);
        }

        /// <summary>
        /// Add EXP to a Passive Skill of a player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="StatusName">Status Name</param>
        /// <param name="skillbaseID">BaseID of a certain skill</param>
        /// <param name="exp">Maximum of exp to add</param>
        public static void AddPassiveSkillEXP(ActorPC pc, string StatusName, uint skillbaseID, uint exp)
        {
            Tasks.PassiveSkillStatus task;
            if (pc.Tasks.ContainsKey(StatusName))
            {
                task = (Tasks.PassiveSkillStatus)pc.Tasks[StatusName];
                AddSkillEXP(ref pc, (uint)(skillbaseID + task.level), exp);
            }
        }

        /// <summary>
        /// Add EXP to a skill of a player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillid">Skill ID</param>
        /// <param name="exp">Maximum of exp to add</param>
        public static void AddSkillEXP(ref ActorPC pc, uint skillid, uint exp)
        {
            if (pc.BattleSkills.ContainsKey(skillid))
            {
                SkillInfo info = pc.BattleSkills[skillid];
                info.exp += (uint)Global.Random.Next(0, (int)exp);
                if (info.exp > SkillFactory.GetSkill(skillid).maxsxp) info.exp = (uint)SkillFactory.GetSkill(skillid).maxsxp;
                MapServer.charDB.UpdateSkill(pc, SkillType.Battle, info);
                ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
                eh.C.UpdateSkillEXP(skillid, info.exp);
            }
        }
        /// <summary>
        /// Decrease the Durability of the active weapon of a player
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="amount">Amount of durability to lose</param>
        public static void WeaponLoseDura(ActorPC pc, ushort amount)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            SagaDB.Items.Weapon weapon = SagaDB.Items.WeaponFactory.GetActiveWeapon(pc);
            weapon.durability -= amount;
            eh.C.UpdateWeaponInfo(SagaMap.Packets.Server.WeaponAdjust.Function.Durability, weapon.durability);
        }
        /// <summary>
        /// Check if the player has enough sp to cast skill, and decrease SP by the required amount
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillid">Skill ID</param>
        /// <returns></returns>
        public static bool CheckSkillSP(ActorPC pc, SkillIDs skillid)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Skill skill = SkillFactory.GetSkill((uint)skillid);
            if (pc.SP < skill.sp) return false;
            pc.SP -= (ushort)skill.sp;
            eh.C.SendCharStatus(0);
            return true;
        }
        /// <summary>
        /// Increases Players LP by 1 point
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillID">ID of the skill that caused the increasement of LP</param>
        public static void GainLP(ActorPC pc,SkillIDs skillID)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            pc.LP += 1;
            if (pc.LP > 5) pc.LP = 5;
            else
                eh.C.SendSkillEffect(SkillFactory.GetSkill((uint)skillID).addition, SkillEffects.LP, 1);
            eh.C.SendCharStatus(0);
        }
        /// <summary>
        /// Increase Players LP
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="skillID">ID of the skill that caused the increasement of LP</param>
        /// <param name="point">Amount</param>
        public static void GainLP(ActorPC pc, SkillIDs skillID, byte point)
        {
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            pc.LP += point;
            if (pc.LP > 5) pc.LP = 5;
            else
                eh.C.SendSkillEffect(SkillFactory.GetSkill((uint)skillID).addition, SkillEffects.LP, point);
            eh.C.SendCharStatus(0);
        }
        /// <summary>
        /// Set the skill argument to "Skill casting failed"
        /// </summary>
        /// <param name="arg"></param>
        public static void SetSkillFailed(ref Map.SkillArgs arg)
        {
            arg.damage = 0;
            arg.isCritical = Map.SkillArgs.AttackResult.Miss;
            arg.failed = true;                    
        }
        /// <summary>
        /// Add a passive status to a Actor
        /// </summary>
        /// <param name="pc">Actor</param>
        /// <param name="StatusName">Name of the Status</param>
        /// <param name="WeaponType">Weapon type</param>
        /// <param name="Status">Status instance</param>
        /// <returns>Result</returns>
        public static PassiveStatusAddResult AddPassiveStatus(Actor pc,string StatusName,byte WeaponType,out Tasks.PassiveSkillStatus Status)
        {
            return AddPassiveStatus(pc, StatusName, WeaponType, out Status, null, null);
        }
        /// <summary>
        /// Add a passive status to a Actor
        /// </summary>
        /// <param name="pc">Player</param>
        /// <param name="StatusName">Name of the Status</param>
        /// <param name="WeaponType">Weapon type</param>
        /// <param name="Status">Status instance</param>
        /// <param name="DeactFunc">Delegate to a deactivation function</param>
        /// <returns>Result</returns>
        public static PassiveStatusAddResult AddPassiveStatus(Actor pc, string StatusName, byte WeaponType, out Tasks.PassiveSkillStatus Status, Tasks.PassiveSkillStatus.DeactivateFunc DeactFunc)
        {
            return AddPassiveStatus(pc, StatusName, WeaponType, out Status, null, DeactFunc);
        }
        /// <summary>
        /// Add a passive status to a Actor
        /// </summary>
        /// <param name="pc">Actor</param>
        /// <param name="StatusName">Name of the Status</param>
        /// <param name="WeaponType">Weapon type</param>
        /// <param name="Status">Status instance</param>
        /// <param name="callback">Delegate to a callback function</param>
        /// <returns>Result</returns>
        public static PassiveStatusAddResult AddPassiveStatus(Actor pc, string StatusName, byte WeaponType, out Tasks.PassiveSkillStatus Status, Tasks.PassiveSkillStatus.CallBackFunc callback)
        {
            return AddPassiveStatus(pc, StatusName, WeaponType, out Status, callback, null);
        }
        /// <summary>
        /// Add a passive status to a Actor
        /// </summary>
        /// <param name="pc">Actor</param>
        /// <param name="StatusName">Name of the Status</param>
        /// <param name="WeaponType">Weapon type</param>
        /// <param name="Status">Status instance</param>
        /// <param name="callback">Delegate to a callback function</param>
        /// <param name="DeactFunc">Delegate to a deactivation function</param>
        /// <returns>Result</returns>
        public static PassiveStatusAddResult AddPassiveStatus(Actor pc, string StatusName, byte WeaponType, out PassiveSkillStatus Status, PassiveSkillStatus.CallBackFunc callback, PassiveSkillStatus.DeactivateFunc DeactFunc)
        {
            Status = null;
            if (pc.type == ActorType.PC)
            {
                if (SagaDB.Items.WeaponFactory.GetActiveWeapon((ActorPC)pc).type != WeaponType && WeaponType != 255)
                {
                    if (pc.Tasks.ContainsKey(StatusName))
                    {
                        Status = (PassiveSkillStatus)pc.Tasks[StatusName];
                        pc.Tasks.Remove(StatusName);
                    }
                    return PassiveStatusAddResult.WeaponMissMatch;
                }
            }
            if (pc.Tasks.ContainsKey(StatusName))
            {
                Status = (PassiveSkillStatus)pc.Tasks[StatusName];
                return PassiveStatusAddResult.Updated;
            }
            else
            {
                Status = new PassiveSkillStatus(1);
                if (DeactFunc != null) Status.DeactFunc += DeactFunc;
                if (callback != null) Status.Func += callback;
                Status.client = pc;
                pc.Tasks.Add(StatusName, Status);
                return PassiveStatusAddResult.OK;
            }
        }

        /// <summary>
        /// Apply a addition to an actor
        /// </summary>
        /// <param name="actor">Actor which the addition should be applied to</param>
        /// <param name="addition">Addition to be applied</param>
        public static void ApplyAddition(Actor actor, Addition addition)
        {
            if (actor.BattleStatus.Additions.ContainsKey(addition.Name))
            {
                Addition oldaddition = actor.BattleStatus.Additions[addition.Name];
                if (oldaddition.Activated)
                    oldaddition.AdditionEnd();
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                actor.BattleStatus.Additions.Remove(addition.Name);
                actor.BattleStatus.Additions.Add(addition.Name, addition);
            }
            else
            {
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                actor.BattleStatus.Additions.Add(addition.Name, addition);
            }
        }

        public static void RemoveAddition(Actor actor, Addition addition)
        {
            RemoveAddition(actor, addition, false);
        }

        public static void RemoveAddition(Actor actor, Addition addition, bool removeOnly)
        {
            if (actor.BattleStatus.Additions.ContainsKey(addition.Name))
            {
                actor.BattleStatus.Additions.Remove(addition.Name); 
                if (addition.Activated && !removeOnly)
                {
                    addition.AdditionEnd();                    
                }
                addition.Activated = false;                     
            }
        }

        /// <summary>
        /// Reset skills of a PC on job change. 
        /// Move all skills into Inactive Skill table and get all skills in inactive skill table for current job
        /// </summary>
        /// <param name="pc">target PC</param>
        public static void SkillResetOnJobChange(ActorPC pc)
        {
            List<uint> tmptable = new List<uint>();
            byte job = (byte)(pc.job - 1);
            foreach (uint i in pc.BattleSkills.Keys)
            {
                if (SkillFactory.GetSkill(i).reqjob[job] == 255 || SkillFactory.GetSkill(i).reqjob[job] == 0)
                {
                    tmptable.Add(i);
                }
            }
            foreach (uint i in tmptable)
            {
                if (!pc.InactiveSkills.ContainsKey(i))
                {
                    pc.InactiveSkills.Add(i, pc.BattleSkills[i]);
                    
                }
                MapServer.charDB.UpdateSkill(pc, SkillType.Inactive, pc.BattleSkills[i]);
                SendDeleteSkill(pc, i);
                pc.BattleSkills.Remove(i);
            }
            tmptable.Clear();
            foreach (uint i in pc.SpecialSkills.Keys)
            {
                tmptable.Add(i);                
            }
            foreach (uint i in tmptable)
            {
                if (!pc.InactiveSkills.ContainsKey(i)) pc.InactiveSkills.Add(i, pc.SpecialSkills[i]);
                MapServer.charDB.UpdateSkill(pc, SkillType.Inactive, pc.SpecialSkills[i]);
                SendDeleteSpecial(pc, i);
                SendDeleteSkill(pc, i);
                pc.SpecialSkills.Remove(i);
            }
            tmptable.Clear();
            foreach (uint i in pc.InactiveSkills.Keys)
            {
                if (SkillFactory.GetSkill(i).reqjob[job] != 255 && SkillFactory.GetSkill(i).reqjob[job] != 0)
                {
                    tmptable.Add(i);
                }
            }
            foreach (uint i in tmptable)
            {
                if (!pc.BattleSkills.ContainsKey(i)) pc.BattleSkills.Add(i, pc.InactiveSkills[i]);
                MapServer.charDB.UpdateSkill(pc, SkillType.Battle, pc.InactiveSkills[i]);
                SendAddSkill(pc, i, 0);                
                pc.InactiveSkills.Remove(i);
            }
            SkillInfo info = new SkillInfo();
            switch (pc.job)
            {
                case JobType.NOVICE:
                    if (!CheckSkill(pc, 1406901, 11))
                    {
                        info.ID = 1406901;
                        pc.BattleSkills.Add(1406901, info);
                        MapServer.charDB.NewSkill(pc, SkillType.Battle, info);
                        SendAddSkill(pc, 1406901, 0);
                    }
                    break;
                case JobType.SWORDMAN:
                    if (!CheckSkill(pc, 1416901, 11))
                    {
                        info.ID = 1416901;
                        pc.BattleSkills.Add(1416901, info);
                        MapServer.charDB.NewSkill(pc, SkillType.Battle, info);
                        SendAddSkill(pc, 1416901, 0);
                    }
                    break;
                case JobType.RECRUIT:
                    if (!CheckSkill(pc, 1426901, 11))
                    {
                        info.ID = 1426901;
                        pc.BattleSkills.Add(1426901, info);
                        MapServer.charDB.NewSkill(pc, SkillType.Battle, info);
                        SendAddSkill(pc, 1426901, 0);
                    }
                    break;
                case JobType.THIEF:
                    if (!CheckSkill(pc, 1406901, 11))
                    {
                        info.ID = 1406901;
                        pc.BattleSkills.Add(1406901, info);
                        MapServer.charDB.NewSkill(pc, SkillType.Battle, info);
                        SendAddSkill(pc, 1406901, 0);
                    }
                    break;
                case JobType.ENCHANTER:
                    if (!CheckSkill(pc, 1446901, 11))
                    {
                        info.ID = 1446901;
                        pc.BattleSkills.Add(1446901, info);
                        MapServer.charDB.NewSkill(pc, SkillType.Battle, info);
                        SendAddSkill(pc, 1446901, 0);
                    }
                    break;
                case JobType.CLOWN:
                    if (!CheckSkill(pc, 1406901, 11))
                    {
                        info.ID = 1406901;
                        pc.BattleSkills.Add(1406901, info);
                        MapServer.charDB.NewSkill(pc, SkillType.Battle, info);
                        SendAddSkill(pc, 1406901, 0);
                    }
                    break;
            }
            CastPassivSkill(ref pc);
        }

        private static bool CheckSkill(ActorPC pc, uint baseid, int maxlv)
        {
            for (int i = 0; i < maxlv; i++)
            {
                if (pc.BattleSkills.ContainsKey((uint)(baseid + i)))
                    return true;
            }
            return false;
        }
    }
}
