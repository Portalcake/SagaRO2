using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
        internal static void CalcFlee(ref Actor actor)
        {
            ActorPC pc;
            ActorNPC npc;
            switch (actor.type)
            {
                case ActorType.PC:
                    pc = (ActorPC)actor;
                    CalcFleePC(ref pc);
                    break;
                case ActorType.NPC:
                    npc = (ActorNPC)actor;
                    CalcFleeNPC(ref npc);
                    break;
            }
        }

        private static void CalcFleePC(ref ActorPC pc)
        {
            pc.BattleStatus.flee = 120;
            pc.BattleStatus.rflee = 120;
            pc.BattleStatus.mflee = 120;
        }

        private static void CalcFleeNPC(ref ActorNPC npc)
        {
            return;
        }
    }
}
