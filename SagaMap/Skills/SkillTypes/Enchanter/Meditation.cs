using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;

namespace SagaMap.Skills.SkillTypes
{
    public static class Meditation
    {
        const SkillIDs baseID = SkillIDs.Meditation;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            byte level;
            ActorPC pc = (ActorPC)sActor;
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Tasks.PassiveSkillStatus ss;
            level = (byte)(args.skillID - baseID + 1);
            
            args.damage = 0;
            args.isCritical =  Map.SkillArgs.AttackResult.Nodamage;
            if (dActor.type == ActorType.PC)
            {
                ActorPC targetpc = (ActorPC)dActor;

                if (targetpc.Tasks.ContainsKey("Meditation"))
                {
                    ss = (PassiveSkillStatus)targetpc.Tasks["Meditation"];
                    targetpc.BattleStatus.fireresist -= CalcValue(ss.level);
                    targetpc.BattleStatus.iceresist -= CalcValue(ss.level);
                    targetpc.BattleStatus.windresist -= CalcValue(ss.level);
                    targetpc.BattleStatus.holyresist -= CalcValue(ss.level);
                    targetpc.BattleStatus.darkresist -= CalcValue(ss.level);                    
                    ss.level = level;
                    targetpc.BattleStatus.fireresist += CalcValue(level);
                    targetpc.BattleStatus.iceresist += CalcValue(level);
                    targetpc.BattleStatus.windresist += CalcValue(level);
                    targetpc.BattleStatus.holyresist += CalcValue(level);
                    targetpc.BattleStatus.darkresist += CalcValue(level);
                    if (dActor.type == ActorType.PC)
                    {
                        eh = (ActorEventHandlers.PC_EventHandler)dActor.e;
                        eh.C.SendBattleStatus();
                    }
                }
                else
                {
                    ss = new PassiveSkillStatus(level);
                    ss.DeactFunc += Deactivate;
                    ss.client = dActor;
                    targetpc.Tasks.Add("Meditation", ss);
                    targetpc.BattleStatus.fireresist += CalcValue(level);
                    targetpc.BattleStatus.iceresist += CalcValue(level);
                    targetpc.BattleStatus.windresist += CalcValue(level);
                    targetpc.BattleStatus.holyresist += CalcValue(level);
                    targetpc.BattleStatus.darkresist += CalcValue(level);
                    if (dActor.type == ActorType.PC)
                    {
                        eh = (ActorEventHandlers.PC_EventHandler)dActor.e;
                        eh.C.SendBattleStatus();
                    }
                }
            }
        }

        private static int CalcValue(byte level)
        {
            return 5 + 2 * level;
        }

        private static void Deactivate(Actor actor)
        {
            Tasks.PassiveSkillStatus ss;
            ActorEventHandlers.PC_EventHandler eh;
            ss = (PassiveSkillStatus)actor.Tasks["Meditation"];
            actor.BattleStatus.fireresist -= CalcValue(ss.level);
            actor.BattleStatus.iceresist -= CalcValue(ss.level);
            actor.BattleStatus.windresist -= CalcValue(ss.level);
            actor.BattleStatus.holyresist -= CalcValue(ss.level);
            actor.BattleStatus.darkresist -= CalcValue(ss.level);
           
            if (actor.type == ActorType.PC)
            {
                ActorPC targetpc = (ActorPC)actor;
                eh = (ActorEventHandlers.PC_EventHandler)actor.e;
                SkillHandler.CalcHPSP(ref targetpc);
                eh.C.SendCharStatus(0);
            }
        }

    }


}
