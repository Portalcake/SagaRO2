using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class BayonetStance
    {
        const SkillIDs baseID = SkillIDs.BayonetStance;
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
                SkillHandler.SetSkillFailed(ref args);
                return;
            }
            args.damage = 0;
            args.isCritical =  Map.SkillArgs.AttackResult.Nodamage;// This skill is not for attacking
            if (!pc.BattleStatus.Additions.ContainsKey("BayonetStance"))
            {
                SkillHandler.ApplyAddition(pc, new Additions.Global.DefaultStance(args.skillID, pc, "BayonetStance"));
                args.failed = true;
            }
            else
            {
                args.failed = false;
                SkillHandler.RemoveAddition(pc, pc.BattleStatus.Additions["BayonetStance"]);
            }
        }

    }
}
