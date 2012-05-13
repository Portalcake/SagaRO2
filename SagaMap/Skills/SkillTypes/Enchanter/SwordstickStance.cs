using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class SwordstickStance
    {
        const SkillIDs baseID = SkillIDs.SwordstickStance;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            ActorPC pc = (ActorPC)sActor;
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            if (sActor.type == ActorType.PC)
            {
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }                
            }
            else//currently cannot be cast on player
            {
                args.damage = 0;
                args.isCritical =  Map.SkillArgs.AttackResult.Miss;
                args.failed = false;
                return;
            }
            args.damage = 0;
            args.isCritical =  Map.SkillArgs.AttackResult.Nodamage;// This skill is not for attacking
            byte level = (byte)(args.skillID - baseID + 1);
            if (!pc.Tasks.ContainsKey("Swordstick Stance"))
            {
                Tasks.PassiveSkillStatus t = new SagaMap.Tasks.PassiveSkillStatus(level);
                t.DeactFunc += Deactivate;
                args.failed = true;
                pc.Tasks.Add("Swordstick Stance", t);
                SkillHandler.AddStatusIcon(pc, (uint)args.skillID, 0);                
            }
            else
            {
                args.failed = false;
                pc.Tasks.Remove("Swordstick Stance");
                SkillHandler.RemoveStatusIcon(pc, (uint)args.skillID);                
            }
        }
        
        private static void Deactivate(Actor actor)
        {
            Tasks.PassiveSkillStatus ss;
            ss = (Tasks.PassiveSkillStatus)actor.Tasks["Swordstick Stance"];
            SkillHandler.RemoveStatusIcon(actor, (uint)(baseID + ss.level - 1));
        }
        

    }
}
