using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class DefensiveStance
    {
        const SkillIDs baseID = SkillIDs.DefensiveStance;
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
            if (!pc.BattleStatus.Additions.ContainsKey("DefensiveStance"))
            {
                SkillHandler.ApplyAddition(pc, new Additions.Global.DefaultStance(args.skillID, pc, "DefensiveStance"));
                args.failed = true;
            }
            else
            {
                args.failed = false;
                SkillHandler.RemoveAddition(pc, pc.BattleStatus.Additions["DefensiveStance"]);
            }
        }
    }
}
