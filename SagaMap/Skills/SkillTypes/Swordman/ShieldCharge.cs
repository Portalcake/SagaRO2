using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
using SagaMap.Bonus;
namespace SagaMap.Skills.SkillTypes
{
    public static class ShieldCharge
    {
        const SkillIDs baseID = SkillIDs.ShieldCharge;
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                byte level = (byte)(args.skillID - baseID + 1);
                if (!SkillHandler.CheckSkillSP(pc, args.skillID))
                {
                    SkillHandler.SetSkillFailed(ref args);
                    return;
                }
                args.damage = 0;
                args.isCritical = SkillHandler.CalcCrit(sActor, dActor, args, SkillHandler.AttackType.Physical);
                if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
                {
                    args.damage = CalcDamage(sActor, dActor, args);
                    int perc = 0;
                    perc = 51 + level * 4;
                    if (Global.Random.Next(0, 99) <= perc)
                    {
                        Tasks.PassiveSkillStatus ss;
                        switch (SkillHandler.AddPassiveStatus(dActor, "Stunned", 255, out ss, new SagaMap.Tasks.PassiveSkillStatus.CallBackFunc(Callback), new SagaMap.Tasks.PassiveSkillStatus.DeactivateFunc(Deactivate)))
                        {
                            case PassiveStatusAddResult.Updated:
                                ss.dueTime = 3000;
                                ss.period = 3000;
                                SkillHandler.RemoveStatusIcon(dActor, (uint)(baseID + ss.level - 1));
                                if (ss.Activated()) ss.Deactivate();
                                ss.Activate();
                                ss.level = level;
                                SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 3000);
                                break;
                            case PassiveStatusAddResult.OK:
                                ss.dueTime = 3000;
                                ss.period = 3000;
                                ss.level = level;
                                ss.Activate();
                                SkillHandler.AddStatusIcon(dActor, (uint)args.skillID, 3000);
                                break;
                        }
                    }
                }
            }
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static void Callback(Actor client)
        {
            try
            {
                Tasks.PassiveSkillStatus ss = (Tasks.PassiveSkillStatus)client.Tasks["Stunned"];
                ss.Deactivate();
                client.Tasks.Remove("Stunned");                
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
                Tasks.PassiveSkillStatus ss = (Tasks.PassiveSkillStatus)client.Tasks["Stunned"];
                SkillHandler.RemoveStatusIcon(client, (uint)(baseID + ss.level - 1));
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        private static uint CalcDamage(Actor sActor,Actor dActor,Map.SkillArgs args)
        {
            return (uint)(sActor.BattleStatus.atk + SkillHandler.GetSkillAtkBonus(args.skillID));
        }

   }
}
