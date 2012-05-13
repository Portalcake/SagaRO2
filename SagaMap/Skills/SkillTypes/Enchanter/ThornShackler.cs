using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class ThornShackler
    {
        /*
        const SkillIDs baseID = SkillIDs.ThornShackler;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.SP < SkillFactory.GetSkill((uint)args.skillID).sp)
                {
                    args.damage = 0;
                    args.isCritical = Map.SkillArgs.AttackResult.Miss;
                    args.failed = true;
                    return;
                }
                else
                {
                    ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
                    pc.SP -= (ushort)SkillFactory.GetSkill((uint)args.skillID).sp;
                    eh.C.SendCharStatus();
                }
            }
            args.damage = 0;
            args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
            if (sActor.type == ActorType.PC)
            {
                ActorPC targetPC = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)targetPC.e;
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss)
                {
                    SkillHandler.AddSkillEXP(ref targetPC, (uint)args.skillID, 3);
                }
            }
            if (dActor.type == ActorType.NPC)//currently only can be casted on NPCs(Mob)
            {
                ActorNPC npc = (ActorNPC)dActor;
                Mob mob = (Mob)dActor.e;
                if (!npc.Tasks.ContainsKey("Thorn Shackler") && (Global.Random.Next(0, 99) < 75))
                {
                    Tasks.PassiveSkillStatus ss = new SagaMap.Tasks.PassiveSkillStatus(level);
                    ss.dueTime = 90000;//90 seconds
                    ss.period = 180000;
                    ss.client = dActor;
                    ss.Func = new SagaMap.Tasks.PassiveSkillStatus.CallBackFunc(Callback);
                    npc.Tasks.Add("Thorn Shackler", ss);
                    ss.Activate();
                    SkillHandler.AddStatusIcon(dActor, (uint)(51500 + ss.level), (uint)ss.dueTime);
                }
            }
            SkillHandler.MagicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.SPIRIT, ref args);
        }

       

        public static void Callback(Actor client)
        {
            Tasks.PassiveSkillStatus ss;
            ss = (Tasks.PassiveSkillStatus)client.Tasks["Thorn Shackler"];
            SkillHandler.RemoveStatusIcon(client, (uint)(51500 + ss.level));

            ss.Deactivate();
            client.Tasks.Remove("Thorn Shackler");
            ss = null;

        }
        */
    }
}
