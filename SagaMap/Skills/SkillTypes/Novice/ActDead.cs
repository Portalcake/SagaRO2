using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class ActDead
    {
        const SkillIDs baseID = SkillIDs.ActDead;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            ActorPC pc = (ActorPC)sActor;
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            if (sActor.type == ActorType.PC)
            {
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Nodamage;// This skill is not for attacking
                byte level = (byte)(args.skillID - baseID + 1);
                SkillHandler.AddSkillEXP(ref pc, (uint)args.skillID, 3);
                if (!pc.Tasks.ContainsKey("ActDead"))
                {
                    Tasks.PassiveSkillStatus t = new SagaMap.Tasks.PassiveSkillStatus(level);
                    args.failed = true;
                    pc.Tasks.Add("ActDead", t);
                    SkillHandler.AddStatusIcon(pc, (uint)args.skillID, 0);
                }
                else
                {
                    args.failed = false;
                    pc.Tasks.Remove("ActDead");
                    SkillHandler.RemoveStatusIcon(pc, (uint)args.skillID);
                }
            }
        }
    }
}
