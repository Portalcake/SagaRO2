using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class SharpenedBlades
    {
        const SkillIDs baseID = SkillIDs.SharpenedBlades;
         public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
         {
             args.damage = 0;
             args.isCritical = 0;
             bool ifActivate = (SagaDB.Items.WeaponFactory.GetActiveWeapon((ActorPC)sActor).type == 1);
             SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultPassiveSkill(args.skillID, dActor, "SharpenedBlades", ifActivate));                      
         }
        
    }
}
