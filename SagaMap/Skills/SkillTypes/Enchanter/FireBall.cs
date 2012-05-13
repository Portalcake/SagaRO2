using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class FireBall
    {
        /*
        const SkillIDs baseID = SkillIDs.FireBall;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.SP < SkillFactory.GetSkill((uint)args.skillID).sp || pc.LP < 3)
                {
                    args.damage = 0;
                    args.isCritical = Map.SkillArgs.AttackResult.Miss;
                    args.failed = true;
                    return;
                }
                else
                {
                    ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
                    pc.SP -= (ushort)SkillFactory.GetSkill((uint)args.skillID).sp;
                    eh.C.SendCharStatus();
                }
            }
            args.damage = 0;
            args.isCritical = SkillHandler.CalcCrit(sActor,dActor, args, SkillHandler.AttackType.Magical);
            if (sActor.type == ActorType.PC)
            {
                ActorPC targetPC = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)targetPC.e; 
                if (args.isCritical !=  Map.SkillArgs.AttackResult.Miss)
                {
                    args.damage = CalcDamage(sActor, dActor, targetPC.LP, args);
                    SkillHandler.AddSkillEXP(ref targetPC, (uint)args.skillID, 3);
                }
                targetPC.LP = 0;
                eh.C.SendCharStatus();
            }
            if (args.damage <= 0) args.damage = 1;
            if (args.isCritical ==  Map.SkillArgs.AttackResult.Critical) args.damage =(uint)( args.damage * 1.5f);//if Critical then double the damage
            SkillHandler.MagicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.FIRE,ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,byte LP, Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            return (uint)(sActor.BattleStatus.matk * 5 +  LP * 10);
        }
        */
   }
}
