using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class ManhoodBreaker
    {
        const SkillIDs baseID = SkillIDs.ManhoodBreaker;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                SkillHandler.GainLP(pc, args.skillID);
                
            }
            args.damage = 0;
            args.isCritical = SkillHandler.CalcCrit(sActor,dActor, args, SkillHandler.AttackType.Physical);
            if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
            {
                ActorPC targetPC = (ActorPC)sActor;
                args.damage = CalcDamage(sActor, dActor, args);                
            }
            if (args.damage <= 0) args.damage = 1;
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            return (uint)(sActor.BattleStatus.atk + SkillHandler.GetSkillAtkBonus(args.skillID));
        }

   }
}
