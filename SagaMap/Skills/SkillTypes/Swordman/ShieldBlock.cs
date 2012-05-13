using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class ShieldBlock
    {
        public const SkillIDs baseID = SkillIDs.ShieldBlock;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            ActorPC pc = (ActorPC)sActor;
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            if (sActor.type == ActorType.PC)
            {
                if (!SkillHandler.CheckSkillSP(pc, args.skillID) || pc.LP < 3)
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                pc.LP -= 3;
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Nodamage;// This skill is not for attacking
                SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultBuff(args.skillID, dActor, "ShieldBlock", 300000, Addition.AdditionType.Buff));     
                
            }
        }
    }
}
