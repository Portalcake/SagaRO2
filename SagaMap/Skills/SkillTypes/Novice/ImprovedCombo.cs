using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class ImprovedCombo
    {
        const SkillIDs baseID = SkillIDs.ImprovedCombo;
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
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Physical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    args.damage = CalcDamage(sActor, dActor, args);
                }
            }
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            ActorNPC npc = (ActorNPC)dActor;
            ActorPC pc = (ActorPC)sActor;
            byte level = (byte)(args.skillID - baseID + 1);
            int delta = Math.Abs((int)((npc.level - pc.cLevel)));
            if (delta <= 20)
                return (uint)(sActor.BattleStatus.atk + (14 + level * 6) * delta);
            else
                return (uint)(sActor.BattleStatus.atk + (14 + level * 6) * 20);
        }

        

    }
}
