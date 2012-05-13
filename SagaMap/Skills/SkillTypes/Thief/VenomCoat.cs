using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;

using SagaMap.Tasks;
namespace SagaMap.Skills.SkillTypes
{
    public static class VenomCoat
    {
        const SkillIDs baseID = SkillIDs.VenomCoat;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.SP < SkillFactory.GetSkill((uint)args.skillID).sp)
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
                    pc.LP += 1;
                    if (pc.LP > 5) pc.LP = 5;
                    eh.C.SendCharStatus(0);
                }
            }
            args.damage = 0;
            args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
            byte level = (byte)(args.skillID - baseID + 1);       
            ActorPC targetpc = (ActorPC)dActor;
            Tasks.PassiveSkillStatus ss;
            
            if (targetpc.Tasks.ContainsKey("VenomCoat"))
            {
                ss = (PassiveSkillStatus)targetpc.Tasks["VenomCoat"];
                ss.level = level;
                SkillHandler.RemoveStatusIcon(dActor, (uint)1457400 + ss.level);
                targetpc.BattleStatus.atkskill -= CalcDamage(ss.level);
                ss.Deactivate();
                ss.dueTime = 1;
                ss.period = 600000;
                targetpc.BattleStatus.atkskill += CalcDamage(level);
                SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 600000);
                ss.Activate();
            }
            else
            {
                ss = new PassiveSkillStatus(level);
                ss.dueTime = 1;
                ss.period = 600000;

                ss.client = dActor;
                ss.Func = new PassiveSkillStatus.CallBackFunc(Callback);
                targetpc.Tasks.Add("VenomCoat", ss);
                targetpc.BattleStatus.atkskill += CalcDamage(level);
                SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 600000);
                ss.Activate();
            }
        }

        private static int CalcDamage(byte level)
        {
            return (15 + 5 * level);
        }

        public static void Callback(Actor client)
        {
            try
            {
                Tasks.PassiveSkillStatus ss;
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)client.e;
                ss = (PassiveSkillStatus)client.Tasks["VenomCoat"];
                if (ss.dueTime > 0)
                {
                    if (client.type == ActorType.PC)
                    {
                        eh.C.SendBattleStatus();
                    }
                    ss.dueTime--;
                }
                else
                {
                    SkillHandler.RemoveStatusIcon(client, (uint)(1457400 + ss.level));
                    client.BattleStatus.atkskill -= CalcDamage(ss.level);
                    eh.C.SendBattleStatus();
                    ss.Deactivate();
                    client.Tasks.Remove("VenomCoat");
                    ss = null;
                }
            }
            catch (Exception) { }
        }
   }
}
