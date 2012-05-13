using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class Tracking
    {
        const SkillIDs baseID = SkillIDs.Tracking;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                byte level = (byte)(args.skillID - baseID + 1);
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
                SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultBuff(args.skillID, dActor, "Tracking", 900000, Addition.AdditionType.Buff));     
            }            
        }


    }
}
