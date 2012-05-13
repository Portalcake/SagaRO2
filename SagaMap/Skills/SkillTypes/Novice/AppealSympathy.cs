using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Skills.SkillTypes
{
    public static class AppealSympathy
    {
        const SkillIDs baseID = SkillIDs.AppealSympathy;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                Mob mob = (Mob)dActor.e;
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Nodamage;// This skill is not for attacking
                if (!mob.Hate.ContainsKey(pc.id))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                SkillHandler.AddSkillEXP(ref pc, (uint)args.skillID, 3);
                ushort value = GetHateReduction(args);
                if (mob.Hate[pc.id] > value) mob.Hate[pc.id] -= (byte)value;
                else mob.Hate[pc.id] = 0;
            }

            //TODO:
            //20% Sadness status on enemy
        }

        private static ushort GetHateReduction(Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            return (ushort)(38 + level * 2);
        }
        

    }
}
