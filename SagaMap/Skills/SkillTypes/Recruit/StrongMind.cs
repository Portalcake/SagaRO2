using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class StrongMind
    {
        const SkillIDs baseID = SkillIDs.StrongMind;
         public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
         {
             ActorPC pc=(ActorPC)sActor;
             args.damage = 0;
             args.isCritical = 0;
             SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultPassiveSkill(args.skillID, dActor, "StrongMind", true));
         }

    }
}
