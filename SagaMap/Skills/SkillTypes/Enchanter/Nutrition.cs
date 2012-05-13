using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;

namespace SagaMap.Skills.SkillTypes
{
    public static class Nutrition
    {
        /*
        const SkillIDs baseID = SkillIDs.Nutrition;
        public static void Proc(ref Actor sActor, ref Actor dActor, ref Map.SkillArgs args)
        {
            byte level;
            ActorPC pc = (ActorPC)sActor;
            ActorPC targetpc = (ActorPC)dActor;
            ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)pc.e;
            Tasks.PassiveSkillStatus ss;
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
            args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
            SkillHandler.AddSkillEXP(ref pc, (uint)args.skillID, 3);
            
            if (targetpc.Tasks.ContainsKey("Nutrition"))
            {
                ss = (PassiveSkillStatus)targetpc.Tasks["Nutrition"];
                ss.level = level;
                SkillHandler.RemoveStatusIcon(dActor, (uint)args.skillID);
                ss.Deactivate();
                ss.dueTime = 60;
                ss.period = 5000;
                SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 300000);
                ss.Activate();
            }
            else
            {
                ss = new PassiveSkillStatus(level);
                ss.dueTime = 60;
                ss.period = 5000;
                
                ss.client = dActor;
                ss.Func = new PassiveSkillStatus.CallBackFunc(Callback);
                targetpc.Tasks.Add("Nutrition", ss);
                SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 300000);
                ss.Activate();
            }
        }

        public static void Callback(Actor client)
        {
            Tasks.PassiveSkillStatus ss;
            ss = (PassiveSkillStatus)client.Tasks["Nutrition"];
            if (ss.dueTime > 0)
            {
                if (client.type == ActorType.PC)
                {
                    ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)client.e;
                    eh.C.Char.HP += (ushort)(ss.level * 5);
                    if (eh.C.Char.HP > eh.C.Char.maxHP) eh.C.Char.HP = eh.C.Char.maxHP;
                    eh.C.SendCharStatus();
                }
                ss.dueTime--;
            }
            else
            {
                SkillHandler.RemoveStatusIcon(client, (uint)(50600 + ss.level));
                ss.Deactivate();
                client.Tasks.Remove("Nutrition");
                ss = null;
            }
        }
        */
    }


}
