using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;

namespace SagaMap.Skills.SkillTypes
{
    public static class Training
    {
        /*
        const SkillIDs baseID = SkillIDs.Training;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            byte level;
            ActorPC pc = (ActorPC)sActor;
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Tasks.PassiveSkillStatus ss;
            Tasks.Regeneration ss2;
            level = (byte)(args.skillID - baseID + 1);
            if (sActor.type == ActorType.PC)
            {
                if (pc.SP < SkillFactory.GetSkill((uint)args.skillID).sp)
                {
                    args.damage = 0;
                    args.isCritical =  Map.SkillArgs.AttackResult.Miss;
                    args.failed = true;
                    return;
                }
                else
                {
                    pc.SP -= (ushort)SkillFactory.GetSkill((uint)args.skillID).sp;
                }
            }
            args.damage = 0;
            args.isCritical =  Map.SkillArgs.AttackResult.Nodamage;
            SkillHandler.AddSkillEXP(ref pc, (uint)args.skillID, 3);
            if (dActor.type == ActorType.PC)
            {
                ActorPC targetpc = (ActorPC)dActor;

                if (targetpc.Tasks.ContainsKey("Training"))
                {
                    ss = (PassiveSkillStatus)targetpc.Tasks["Training"];
                    ss2 = (Regeneration)targetpc.Tasks["RegenerationHP"];
                    ss2.hp -= (ushort)(3 + level * 2);
                    targetpc.BattleStatus.hpskill -= (short)CalcHP(targetpc, args);
                    ss.level = level;
                    SkillHandler.RemoveStatusIcon(dActor, (uint)args.skillID);
                    ss.Deactivate();
                    ss.dueTime = 1000;
                    ss.period = 1800000;
                    targetpc.BattleStatus.hpskill += (short)CalcHP(targetpc, args);
                    ss2.hp += (ushort)(3 + level * 2);
                    SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 1800000);
                    ss.Activate();
                    if (dActor.type == ActorType.PC)
                    {
                        eh = (ActorEventHandlers.PC_EventHandler)dActor.e;
                        SkillHandler.CalcHPSP(ref targetpc);
                        eh.C.SendCharStatus();
                    }
                }
                else
                {
                    ss = new PassiveSkillStatus(level);
                    ss2 = (Regeneration)targetpc.Tasks["RegenerationHP"];
                    ss2.hp += (ushort)(3 + level * 2);
                    ss.dueTime = 1000;
                    ss.period = 1800000;

                    ss.client = dActor;
                    ss.Func = new PassiveSkillStatus.CallBackFunc(Callback);
                    targetpc.Tasks.Add("Training", ss);
                    targetpc.BattleStatus.hpskill += CalcHP(targetpc, args);
                    SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 1800000);
                    ss.Activate();
                    if (dActor.type == ActorType.PC)
                    {
                        eh = (ActorEventHandlers.PC_EventHandler)dActor.e;
                        SkillHandler.CalcHPSP(ref targetpc);
                        eh.C.SendCharStatus();
                    }
                }
            }
        }

        public static void Callback(Actor client)
        {
            Tasks.PassiveSkillStatus ss;
                
            ss = (PassiveSkillStatus)client.Tasks["Training"];
            if (ss.dueTime > 0)
            {
                ss.dueTime = 0;
            }
            else
            {
                SkillHandler.RemoveStatusIcon(client, (uint)(50500 + ss.level));
                if (client.type == ActorType.PC)
                {
                    Tasks.Regeneration ss2 = (Regeneration)client.Tasks["RegenerationHP"];
                    ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)client.e;
                    ActorPC targetpc = (ActorPC)client;
                    ss2.hp -= (ushort)(3 + ss.level * 2);
                    targetpc.BattleStatus.hpskill -= CalcHP(targetpc, new SagaMap.Map.SkillArgs(0, 0, (uint)(50500 + ss.level), 0, 0));
                    SkillHandler.CalcHPSP(ref targetpc);
                    eh.C.SendCharStatus();
                }
                ss.Deactivate();
                client.Tasks.Remove("Training");
                ss = null;
            }
        }

        private static short CalcHP(ActorPC pc, Map.SkillArgs args)
        {
            byte level = (byte)(args.skillID - baseID + 1);
            float perc = (float)(4 + level) / 100; ;
            return (short)(pc.maxHP * perc);
        }    

        */

    }


}
