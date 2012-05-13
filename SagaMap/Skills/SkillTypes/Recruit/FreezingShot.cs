using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class FreezingShot
    {
        const SkillIDs baseID = SkillIDs.FreezingShot;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (!SkillHandler.CheckSkillSP(pc, args.skillID) || pc.LP < 1)
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                pc.LP -= 1;
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Physical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    args.damage = CalcDamage(sActor, dActor, args);
                    if (Global.Random.Next(0, 99) > 65)
                    {
                        SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultBuff(1004901, dActor, "Frozen", 5000, Addition.AdditionType.Debuff));
                    }
                }
            }
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            return (uint)(sActor.BattleStatus.ratk + SkillHandler.GetSkillRAtkBonus(args.skillID));
        }
   }
}
