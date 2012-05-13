using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
        internal static void CalcHit(ref Actor actor)
        {
            ActorPC pc;
            ActorNPC npc;
            switch (actor.type)
            {
                case ActorType.PC:
                    pc = (ActorPC)actor;
                    CalcHitPC(ref pc);
                    break;
                case ActorType.NPC:
                    npc = (ActorNPC)actor;
                    CalcHitNPC(ref npc);
                    break;
            }
        }

        private static void CalcHitPC(ref ActorPC pc)
        {
            pc.BattleStatus.hit = pc.dex + pc.BattleStatus.hitskill;
            if (pc.BattleStatus.hit == 0) pc.BattleStatus.hit = 1;//hit must be greater than 0
        }

        private static void CalcHitNPC(ref ActorNPC npc)
        {
            if (npc.BattleStatus.hit == 0) npc.BattleStatus.hit = 1; //hit must be greater than 0
        }
    }
}
