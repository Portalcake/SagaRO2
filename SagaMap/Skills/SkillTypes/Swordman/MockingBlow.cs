using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class MockingBlow
    {
        const SkillIDs baseID = SkillIDs.MockingBlow;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)sActor.e;                        
                if (!SkillHandler.CheckSkillSP(pc, args.skillID) || pc.LP < 1)
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                pc.LP -= 1;
                eh.C.SendCharStatus(0);
                Map map;
                if (dActor.mapID == 0)
                {
                    if (dActor.type == ActorType.PC)
                    {
                        eh = (SagaMap.ActorEventHandlers.PC_EventHandler)dActor.e;                        
                        dActor.mapID = (byte)eh.C.map.ID;
                    }
                }
                if (MapManager.Instance.GetMap(dActor.mapID, out map))
                {

                    foreach (Actor actor in map.GetActorsArea(dActor, 500, true))
                    {
                        Actor tmp = actor;
                        if (actor == sActor || actor.stance == Global.STANCE.DIE || actor.type != ActorType.NPC) continue;
                        args.damage = 0;
                        args.isCritical = SkillHandler.CalcCrit(sActor, actor, args, SkillHandler.AttackType.Physical);
                        ActorNPC npc = (ActorNPC)actor;
                        if (npc.npcType < 10000) continue;
                        if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                        {
                            ActorPC targetPC = (ActorPC)sActor;
                            args.damage = CalcDamage(sActor, actor, args);
                        }
                        SkillHandler.PhysicalAttack(ref sActor, ref tmp, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args);
                        if (actor != dActor)
                        {
                            args.targetActorID = actor.id;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, sActor, true);
                        }
                    }
                    args.targetActorID = dActor.id;
                }
                else
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
            }
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            return (uint)(sActor.BattleStatus.atk + SkillHandler.GetSkillAtkBonus(args.skillID));
        }

   }
}
