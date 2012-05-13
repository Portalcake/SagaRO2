using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class PolleoShot
    {
        const SkillIDs baseID = SkillIDs.PolleoShot;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (!SkillHandler.CheckSkillSP(pc, args.skillID) || pc.LP == 0)
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Physical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    byte level = (byte)(args.skillID - baseID + 1);            
                    args.damage = CalcDamage(sActor, level, pc.LP);                    
                }
                pc.LP = 0;
            }
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor, byte level, byte LP)
        {
            switch (LP)
            {
                case 1:
                    return (uint)(sActor.BattleStatus.atk + 9 + 1 * level);
                case 2:
                    return (uint)(sActor.BattleStatus.atk + 19 + 2 * level);
                case 3:
                    return (uint)(sActor.BattleStatus.atk + 30 + 3 * level);
                case 4:
                    return (uint)(sActor.BattleStatus.atk + 42 + 4 * level);
                case 5:
                    return (uint)(sActor.BattleStatus.atk + 55 + 5 * level);
            }
            return (uint)(sActor.BattleStatus.atk);
        }

   }
}
