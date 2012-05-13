using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class OpenBox
    {
        const SkillIDs baseID = SkillIDs.OpenBox;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            ActorItem item = (ActorItem)dActor;
            ActorPC pc= (ActorPC)sActor;
            MapItem eh = (MapItem)item.e;
            args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
            args.damage = 0;
            eh.OnOpen(pc);
        }


    }
}
