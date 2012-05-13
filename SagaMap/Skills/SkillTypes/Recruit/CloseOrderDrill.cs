using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class CloseOrderDrill
    {
        const SkillIDs baseID = SkillIDs.CloseOrderDrill;
         public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
         {
             args.damage = 0;
             args.isCritical = 0;
             SkillHandler.ApplyAddition(sActor, new Additions.Recruit.CloseOrderDrill(args.skillID, sActor));
         }

    }
}
