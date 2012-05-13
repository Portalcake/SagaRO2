using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaLib;
namespace SagaMap.Skills.SkillTypes
{
    public static class NormalAttack
    {
        public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
        {
            args.damage = 0;
            args.isCritical = SkillHandler.CalcCrit(sActor,dActor, args, SkillHandler.AttackType.Physical);
            if (args.isCritical != Map.SkillArgs.AttackResult.Miss && args.isCritical != Map.SkillArgs.AttackResult.Block)
            {
                args.damage = CalcDamage(sActor, dActor, args);
                if (sActor.type == ActorType.PC)
                {
                    ActorPC targetPC = (ActorPC)sActor;
                    ActorEventHandlers.PC_EventHandler eh = (ActorEventHandlers.PC_EventHandler)targetPC.e;
                    try
                    {
                        switch (args.skillID)
                        {
                            case SkillIDs.NormanShortSwordAttack:
                                SkillHandler.AddPassiveSkillEXP(targetPC, "ShortSwordMastery", 1406900, 3);
                                break;
                            case SkillIDs.NormanLongSwordAttack :
                                SkillHandler.AddPassiveSkillEXP(targetPC, "LongSwordMastery", 1416900, 3);
                                SkillHandler.AddPassiveSkillEXP(targetPC, "ArtOfWarrior", 1419100, 3);                                
                                break;
                            case SkillIDs.NormanDampflintAttack :
                                SkillHandler.AddPassiveSkillEXP(targetPC, "DampRifleMastery", 1426900, 3);
                                SkillHandler.AddPassiveSkillEXP(targetPC, "FirePractice", 1429100, 3);                               
                                break;
                            case SkillIDs.NormanDampflintAttack2:
                                SkillHandler.AddPassiveSkillEXP(targetPC, "DampRifleMastery", 1426900, 3);
                                SkillHandler.AddPassiveSkillEXP(targetPC, "CloseOrderDrill", 1427100, 3);                                
                                break;                            
                            case SkillIDs.NormanSwordStickAttack :
                            case SkillIDs.NormanSwordStickAttack2:
                                SkillHandler.AddPassiveSkillEXP(targetPC, "ShortSwordMastery", 1406900, 3);
                                break;
                        }
                    }
                    catch (Exception){ }
                    SkillHandler.WeaponLoseDura(targetPC, 1);                        
                }
            }
            SkillHandler.PhysicalAttack(ref sActor, ref dActor, args.damage, SkillHandler.AttackElements.NEUTRAL, ref args); 
        }

        private static uint CalcDamage(Actor sActor, Actor dActor, Map.SkillArgs args)
        {
            if (args.skillID == SkillIDs.NormanDampflintAttack)
                return (uint)sActor.BattleStatus.ratk;
            return (uint)sActor.BattleStatus.atk;
        }

       
    }
}
