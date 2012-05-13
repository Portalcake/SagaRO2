using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
     public static class WhiteShortSwordMastery
    {
         const SkillIDs baseID = SkillIDs.WhiteShortSwordMastery;
         public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
         {
             byte level;
             ActorPC pc=(ActorPC)sActor;
             Tasks.PassiveSkillStatus ss;
             args.damage = 0;
             args.isCritical = 0;
             level = (byte)(args.skillID - baseID + 1);
             switch (SkillHandler.AddPassiveStatus(pc, "WhiteShortSwordMastery", 1, out ss, new PassiveSkillStatus.DeactivateFunc(Deactivate)))
             {
                 case PassiveStatusAddResult.WeaponMissMatch:
                     if (ss != null)
                     {
                         BonusHandler.Instance.SkillAddAddition(pc, (uint)(baseID + ss.level - 1), true);
                     }
                     break;
                 case PassiveStatusAddResult.Updated:
                     BonusHandler.Instance.SkillAddAddition(pc, (uint)(baseID + ss.level - 1), true);
                     BonusHandler.Instance.SkillAddAddition(pc, (uint)args.skillID, false);
                     ss.level = level;             
                     break;
                 case PassiveStatusAddResult.OK:
                     ss.level = level;
                     BonusHandler.Instance.SkillAddAddition(pc, (uint)args.skillID, false);
                     break;
             }             
         }

         private static void Deactivate(Actor actor)
         {
             Tasks.PassiveSkillStatus ss;
             ss = (PassiveSkillStatus)actor.Tasks["WhiteShortSwordMastery"];
             BonusHandler.Instance.SkillAddAddition(actor, (uint)(baseID + ss.level - 1), true);                     
         }
        
    }
}
