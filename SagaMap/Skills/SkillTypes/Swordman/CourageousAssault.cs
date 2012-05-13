using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class CourageousAssault
    {
        const SkillIDs baseID = SkillIDs.CourageousAssault;
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
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Physical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    args.damage = CalcDamage(sActor, dActor, args);
                }
                pc.LP = 0;                
            }
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            ActorPC pc = (ActorPC)sActor;
            int dmg = 0;
            switch (pc.LP)
            {
                case 1:
                    dmg = 7 + level;
                    break;
                case 2:
                    dmg = 13 + level * 2;
                    break;
                case 3:
                    dmg = 21 + level * 3;
                    break;
                case 4:
                    dmg = 29 + level * 4;
                    break;
                case 5:
                    dmg = 37 + level * 5;
                    break;
            }
            return (uint)(sActor.BattleStatus.atk + dmg);
        }

   }
}
