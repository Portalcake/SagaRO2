using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class PromiseStone
    {
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type != ActorType.PC)
            {
                SkillHandler.SetSkillFailed(ref args);
                return;
            }
            ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)sActor.e;
            args.damage = 0;
            args.isCritical =  Map.SkillArgs.AttackResult.Nodamage;//not for damage      
            eh.C.OnUsePromiseStone(sActor.mapID, sActor.x, sActor.y, sActor.z);
        }
    }
}
