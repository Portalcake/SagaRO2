using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
        internal static void CalcDef(ref Actor actor)
        {
            ActorPC pc;
            ActorNPC npc;
            switch (actor.type)
            {
                case ActorType.PC:
                    pc = (ActorPC)actor;
                    CalcDefPC(ref pc);
                    break;
                case ActorType.NPC:
                    npc = (ActorNPC)actor;
                    CalcDefNPC(ref npc);
                    break;
            }
        }

        private static void CalcDefPC(ref ActorPC pc)
        {
            pc.BattleStatus.def = pc.BattleStatus.defbonus + pc.BattleStatus.defskill + pc.str + pc.BattleStatus.strbonus;
        }

        private static void CalcDefNPC(ref ActorNPC npc)
        {

        }

        internal static void CalcMDef(ref Actor actor)
        {
            ActorPC pc;
            ActorNPC npc;
            switch (actor.type)
            {
                case ActorType.PC:
                    pc = (ActorPC)actor;
                    CalcMDefPC(ref pc);
                    break;
                case ActorType.NPC:
                    npc = (ActorNPC)actor;
                    CalcMDefNPC(ref npc);
                    break;
            }
        }

        private static void CalcMDefPC(ref ActorPC pc)
        {

        }

        private static void CalcMDefNPC(ref ActorNPC npc)
        {

        }

    }
}
