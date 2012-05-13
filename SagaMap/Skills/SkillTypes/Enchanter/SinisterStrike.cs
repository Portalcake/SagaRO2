using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class SinisterStrike
    {
        const SkillIDs baseID = SkillIDs.SinisterStrike;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.SP < SkillFactory.GetSkill((uint)args.skillID).sp)
                {
                    args.damage = 0;
                    args.isCritical =  Map.SkillArgs.AttackResult.Miss;
                    args.failed = true;
                    return;
                }
                else
                {
                    ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
                    pc.SP -= (ushort)SkillFactory.GetSkill((uint)args.skillID).sp;
                    pc.LP += 1;
                    if (pc.LP > 5) pc.LP = 5;
                    eh.C.SendCharStatus(0);
                }
            }
            args.damage = 0;
            args.isCritical = SkillHandler.CalcCrit(sActor,dActor, args, SkillHandler.AttackType.Physical);
            if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
            {
                ActorPC targetPC = (ActorPC)sActor;
                args.damage = CalcDamage(sActor, dActor, args);                
            }
            if (args.damage <= 0) args.damage = 1;
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.HOLY, ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            return (uint)(sActor.BattleStatus.atk + 10 + 5 * level);
        }

   }
}
