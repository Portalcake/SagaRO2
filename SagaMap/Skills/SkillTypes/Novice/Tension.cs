using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;

namespace SagaMap.Skills.SkillTypes
{
    public static class Tension
    {
        const SkillIDs baseID = SkillIDs.Tension;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            byte level;
            ActorPC pc = (ActorPC)sActor;
            level = (byte)(args.skillID - baseID + 1);            
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            if (!SkillHandler.CheckSkillSP(pc, args.skillID))
            {
                SkillHandler.SetSkillFailed(ref args);
                return;
            }
            SkillHandler.GainLP(pc, args.skillID, (byte)((level + 1) / 2));
            args.damage = 0;
            args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
        }

    }


}
