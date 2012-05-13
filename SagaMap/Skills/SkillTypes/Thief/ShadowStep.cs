using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;

using SagaMap.Tasks;
namespace SagaMap.Skills.SkillTypes
{
    public static class ShadowStep
    {
        const SkillIDs baseID = SkillIDs.ShadowStep;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (!SkillHandler.CheckSkillSP((ActorPC)dActor, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
                SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultBuff(args.skillID, dActor, "ShadowStep", 300000, Addition.AdditionType.Buff));
            }           
        }       
   }
}
