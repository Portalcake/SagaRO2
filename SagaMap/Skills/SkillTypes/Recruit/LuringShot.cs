using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Skills.SkillTypes
{
    public static class LuringShot
    {
        const SkillIDs baseID = SkillIDs.LuringShot;
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
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Physical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    if (dActor.type == ActorType.NPC)
                    {
                        ActorNPC npc = (ActorNPC)dActor;
                        if (npc.npcType >= 10000 && npc.npcType < 50000)
                        {
                            Mob mob = (Mob)npc.e;
                            if (mob.Hate.ContainsKey(sActor.id))
                                mob.Hate[sActor.id] = 65535;
                            else
                                mob.Hate.Add(sActor.id, 65535);
                        }
                    }
                }
            }
        }

   }
}
