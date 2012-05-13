using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Bonus;

namespace SagaMap.Skills.SkillTypes
{
    public static class WeaponBlessing
    {
        const SkillIDs baseID = SkillIDs.WeaponBlessing;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                byte level = (byte)(args.skillID - baseID + 1);
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = Map.SkillArgs.AttackResult.Nodamage;
                Tasks.PassiveSkillStatus ss;
                switch (SkillHandler.AddPassiveStatus(dActor, "WeaponBlessing", 255, out ss, new SagaMap.Tasks.PassiveSkillStatus.CallBackFunc(Callback), new SagaMap.Tasks.PassiveSkillStatus.DeactivateFunc(Deactivate)))
                {
                    case PassiveStatusAddResult.Updated:
                        ss.dueTime = 900000;
                        ss.period = 900000;
                        SkillHandler.RemoveStatusIcon(dActor, (uint)(baseID + ss.level - 1));
                        if (ss.Activated()) ss.Deactivate();
                        ss.Activate();
                        BonusHandler.Instance.SkillAddAddition(dActor, (uint)args.skillID, false);                    
                        ss.level = level;
                        SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 900000);
                        break;
                    case PassiveStatusAddResult.OK:
                        ss.dueTime = 900000;
                        ss.period = 900000;
                        ss.level = level;
                        ss.Activate();
                        BonusHandler.Instance.SkillAddAddition(dActor, (uint)args.skillID, false);
                        SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 900000);
                        break;
                }
                if (dActor.type == ActorType.PC)
                {
                    eh = (SagaMap.ActorEventHandlers.PC_EventHandler)dActor.e;
                    eh.C.SendBattleStatus();
                }
            }            
            
        }

        private static void Callback(Actor client)
        {
            try
            {
                Tasks.PassiveSkillStatus ss = (Tasks.PassiveSkillStatus)client.Tasks["WeaponBlessing"];
                ss.Deactivate();
                client.Tasks.Remove("WeaponBlessing");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        private static void Deactivate(Actor client)
        {
            try
            {
                Tasks.PassiveSkillStatus ss = (Tasks.PassiveSkillStatus)client.Tasks["WeaponBlessing"];
                SkillHandler.RemoveStatusIcon(client, (uint)(baseID + ss.level - 1));
                BonusHandler.Instance.SkillAddAddition(client, (uint)(baseID + ss.level - 1), true);
                if (client.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)client;
                    ActorEventHandlers.PC_EventHandler eh = (SagaMap.ActorEventHandlers.PC_EventHandler)pc.e;
                    eh.C.SendBattleStatus();
                }     
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

    }
}
