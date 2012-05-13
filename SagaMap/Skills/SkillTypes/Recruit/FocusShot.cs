using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Bonus;
using SagaMap.Scripting;
namespace SagaMap.Skills.SkillTypes
{
    public static class FocusShot
    {
        const SkillIDs baseID = SkillIDs.FocusShot;
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
            args.isCritical = SkillHandler.CalcCrit(sActor,dActor, args, SkillHandler.AttackType.Ranged);
            byte level = (byte)(args.skillID - baseID + 1);
            if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
            {
                ActorPC targetPC = (ActorPC)sActor;
                args.damage = CalcDamage(sActor, dActor, args);
                if (dActor.type == ActorType.NPC)//currently only can be casted on NPCs(Mob)
                {
                    ActorNPC npc = (ActorNPC)dActor;
                    Mob mob = (Mob)dActor.e;
                    if (Global.Random.Next(0, 99) < 50)
                    {
                        SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultBuff(1004801, dActor, "LowerBodyParalysis", 5000, Addition.AdditionType.Debuff));
                    }                    
                }
            }
            if (args.damage <= 0) args.damage = 1;
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            return (uint)(sActor.BattleStatus.ratk + SkillHandler.GetSkillRAtkBonus(args.skillID));
        }
   }
}
