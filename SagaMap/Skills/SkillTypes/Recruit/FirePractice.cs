using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;

namespace SagaMap.Skills.SkillTypes
{
    public static class FirePractice
    {
        const SkillIDs baseID = SkillIDs.FirePractice;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            ActorPC pc = (ActorPC)sActor;
            args.damage = 0;
            args.isCritical = 0;
            bool ifActivate = (SagaDB.Items.WeaponFactory.GetActiveWeapon(pc).type == 4);
            SkillHandler.ApplyAddition(dActor, new Additions.Global.DefaultPassiveSkill(args.skillID, dActor, "FirePractice", ifActivate));
        }
    }

}
