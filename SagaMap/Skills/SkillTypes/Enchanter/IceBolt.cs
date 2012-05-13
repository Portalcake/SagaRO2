using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class IceBolt
    {
        const SkillIDs baseID = SkillIDs.IceBolt;
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
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Magical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    uint MDamage;
                    uint IceDamage;
                    args.damage = CalcDamage(sActor, dActor, args);
                    MDamage = SkillHandler.MagicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args);
                    args.damage = CalcIceDamage(sActor, dActor, args);
                    IceDamage = SkillHandler.MagicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.ICE, ref args);
                    args.damage = MDamage + IceDamage;
                }
            }            
        }

        private static uint CalcDamage(Actor sActor, Actor dActor, Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            return (uint)(sActor.BattleStatus.matk * (0.87F + (0.03F * (float)level)));
        }

        private static uint CalcIceDamage(Actor sActor, Actor dActor, Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            return (uint)(40 * level);
        }


   }
}
