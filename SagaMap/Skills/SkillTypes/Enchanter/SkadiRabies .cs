using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class SkadiRabies
    {
        /*const SkillIDs baseID = SkillIDs.SkadiRabies;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.SP < SkillFactory.GetSkill((uint)args.skillID).sp || pc.LP < 2)
                {
                    args.damage = 0;
                    args.isCritical =  Map.SkillArgs.AttackResult.Miss;
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
            args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Magical);
            if (sActor.type == ActorType.PC)
            {
                ActorPC targetPC = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)targetPC.e;
                if (args.isCritical !=  Map.SkillArgs.AttackResult.Miss)
                {
                    args.damage = CalcDamage(sActor, dActor, targetPC.LP, args);
                    SkillHandler.AddSkillEXP(ref targetPC, (uint)args.skillID, 3);
                }
                targetPC.LP = 0;
                eh.C.SendCharStatus();
            }
            if (dActor.type == ActorType.NPC)//currently only can be casted on NPCs(Mob)
            {
                ActorNPC npc = (ActorNPC)dActor;
                Mob mob = (Mob)dActor.e;
                if (!npc.Tasks.ContainsKey("Freezing") && (Global.Random.Next(0, 99) < 75))
                {
                    Tasks.PassiveSkillStatus ss = new SagaMap.Tasks.PassiveSkillStatus(level);
                    ss.dueTime = 60000;
                    ss.period = 180000;
                    ss.client = dActor;
                    ss.Func = new SagaMap.Tasks.PassiveSkillStatus.CallBackFunc(Callback);
                    npc.Tasks.Add("Freezing", ss);
                    ss.Activate();
                    SkillHandler.AddStatusIcon(dActor, (uint)(51200 + ss.level), (uint)ss.dueTime);
                }
            }
            if (args.damage <= 0) args.damage = 1;
            if (args.isCritical ==  Map.SkillArgs.AttackResult.Critical) args.damage = (uint)(args.damage * 1.5f);//if Critical then double the damage
            SkillHandler.MagicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.ICE,ref args);
        }

        private static uint CalcDamage(Actor sActor, Actor dActor, byte LP, Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            return (uint)(sActor.BattleStatus.matk * 3 + LP * 10);
        }

        public static void Callback(Actor client)
        {
            Tasks.PassiveSkillStatus ss;
            ss = (Tasks.PassiveSkillStatus)client.Tasks["Freezing"];
            SkillHandler.RemoveStatusIcon(client, (uint)(51200 + ss.level));

            ss.Deactivate();
            client.Tasks.Remove("Freezing");
            ss = null;

        }
        */
    }
}
