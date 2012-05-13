using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class Heal
    {
        const SkillIDs baseID = SkillIDs.Heal;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            ActorPC pc = (ActorPC)sActor;
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
            if (sActor.type == ActorType.PC)
            {
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Heal;
                ActorPC targetPC;
                args.damage = CalcDamage(sActor, dActor, args);
                if (dActor.type == ActorType.PC)
                {
                    targetPC = (ActorPC)dActor;
                    eh = (SagaMap.ActorEventHandlers.PC_EventHandler)targetPC.e;
                    targetPC.HP += (ushort)args.damage;
                    eh.C.SendSkillEffect(SkillFactory.GetSkill((uint)args.skillID).addition, SkillEffects.HP, args.damage);
                    if (targetPC.HP > targetPC.maxHP) targetPC.HP = targetPC.maxHP;
                    eh.C.SendCharStatus(0);
                }
            }            
            
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            ActorPC pc = (ActorPC)sActor;
            byte level = (byte)(args.skillID - baseID + 1);

            return (uint)(150 * level);
        }

        

    }
}
