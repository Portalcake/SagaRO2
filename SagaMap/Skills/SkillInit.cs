using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
        public static void Initialize()
        {
            SkillFactory.Start("DB/skillDB.xml");
            InitGlobal();
            InitNovice();//initialize the skillhandlers for Novice
            InitSwordman();
            InitRecuit();
            InitThief();
            InitEnchanter();
            Init12_();
            Logger.ShowInfo("SkillHandler sucessfully initialized",null);
        }

        private static void InitGlobal()
        {
            SkillCommands.Add(SkillIDs.NormanBareHandAttack, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.NormanShortSwordAttack, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.NormanSwordStickAttack, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.NormanSwordStickAttack2, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.NormanLongSwordAttack, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.NormanDampflintAttack, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.NormanDampflintAttack2, new SkillCommand(SkillTypes.NormalAttack.Proc));
            SkillCommands.Add(SkillIDs.PromiseStone, new SkillCommand(SkillTypes.PromiseStone.Proc));
        }

        private static void InitNovice()
        {
            AddSub(SkillIDs.ShortSwordMastery, 11, new SkillCommand(SkillTypes.ShortSwordMastery.Proc));
            AddSub(SkillIDs.MartialArts, 11, new SkillCommand(SkillTypes.MartialArts.Proc));            
            AddSub(SkillIDs.Tension, 10, new SkillCommand(SkillTypes.Tension.Proc));
            AddSub(SkillIDs.Integritas, 10, new SkillCommand(SkillTypes.Integritas.Proc));
            AddSub(SkillIDs.QuickBlow, 20, new SkillCommand(SkillTypes.QuickBlow.Proc));
            AddSub(SkillIDs.ImprovedCombo, 11, new SkillCommand(SkillTypes.ImprovedCombo.Proc));
            AddSub(SkillIDs.InsightStrike, 11, new SkillCommand(SkillTypes.InsightStrike.Proc));
            AddSub(SkillIDs.PowerStrike, 10, new SkillCommand(SkillTypes.PowerStrike.Proc));
            AddSub(SkillIDs.AppealSympathy, 11, new SkillCommand(SkillTypes.AppealSympathy.Proc));
            AddSub(SkillIDs.ActDead, 10, new SkillCommand(SkillTypes.ActDead.Proc));
            AddSub(SkillIDs.WhiteShortSwordMastery, 11, new SkillCommand(SkillTypes.WhiteShortSwordMastery.Proc));            
        }

        private static void InitSwordman()
        {
            AddSub(SkillIDs.LongSwordMastery, 11, new SkillCommand(SkillTypes.LongSwordMastery.Proc));
            AddSub(SkillIDs.ArtOfWarrior, 11, new SkillCommand(SkillTypes.ArtOfWarrior.Proc));
            AddSub(SkillIDs.DefenceMastery, 11, new SkillCommand(SkillTypes.DefenceMastery.Proc));
            AddSub(SkillIDs.StrengthenRecovery, 6, new SkillCommand(SkillTypes.StrengthenRecovery.Proc));
            AddSub(SkillIDs.Provoke, 10, new SkillCommand(SkillTypes.Provoke.Proc));
            AddSub(SkillIDs.QuickenAttack, 5, new SkillCommand(SkillTypes.QuickenAttack.Proc));
            AddSub(SkillIDs.MockingBlow, 6, new SkillCommand(SkillTypes.MockingBlow.Proc));
            AddSub(SkillIDs.DefensiveStance, 3, new SkillCommand(SkillTypes.DefensiveStance.Proc)); 
            AddSub(SkillIDs.InternalInjury, 11, new SkillCommand(SkillTypes.InternalInjury.Proc));
            AddSub(SkillIDs.CourageousAssault, 11, new SkillCommand(SkillTypes.CourageousAssault.Proc));
            AddSub(SkillIDs.ChargeAttack, 11, new SkillCommand(SkillTypes.ChargeAttack.Proc));
            AddSub(SkillIDs.MagnumBreak, 11, new SkillCommand(SkillTypes.MagnumBreak.Proc));
            AddSub(SkillIDs.Poking, 5, new SkillCommand(SkillTypes.Poking.Proc));
            AddSub(SkillIDs.ShieldCharge, 11, new SkillCommand(SkillTypes.ShieldCharge.Proc));
            AddSub(SkillIDs.ShieldBlock, 11, new SkillCommand(SkillTypes.ShieldBlock.Proc));
        }

        private static void InitRecuit()
        {
            AddSub(SkillIDs.DampRifleMastery, 11, new SkillCommand(SkillTypes.DampRifleMastery.Proc));
            AddSub(SkillIDs.CloseOrderDrill, 11, new SkillCommand(SkillTypes.CloseOrderDrill.Proc));
            AddSub(SkillIDs.StrongMind, 15, new SkillCommand(SkillTypes.StrongMind.Proc));
            AddSub(SkillIDs.PredatorFocus, 15, new SkillCommand(SkillTypes.PredatorFocus.Proc));
            AddSub(SkillIDs.Tracking, 10, new SkillCommand(SkillTypes.Tracking.Proc));
            AddSub(SkillIDs.Designation, 11, new SkillCommand(SkillTypes.Designation.Proc));
            AddSub(SkillIDs.FreezingShot, 11, new SkillCommand(SkillTypes.FreezingShot.Proc));
            AddSub(SkillIDs.LuringShot, 10, new SkillCommand(SkillTypes.LuringShot.Proc));
            AddSub(SkillIDs.PiercingShot, 10, new SkillCommand(SkillTypes.PiercingShot.Proc));
            AddSub(SkillIDs.RotatingFireShot, 6, new SkillCommand(SkillTypes.RotatingFireShot.Proc));
            AddSub(SkillIDs.AimingShot, 6, new SkillCommand(SkillTypes.AimingShot.Proc));
            AddSub(SkillIDs.Disarm, 15, new SkillCommand(SkillTypes.Disarm.Proc));
            AddSub(SkillIDs.BayonetStance, 3, new SkillCommand(SkillTypes.BayonetStance.Proc));
            AddSub(SkillIDs.FocusShot, 10, new SkillCommand(SkillTypes.FocusShot.Proc));
            AddSub(SkillIDs.FlinteSlam, 15, new SkillCommand(SkillTypes.FlinteSlam.Proc));
            AddSub(SkillIDs.PolleoShot, 11, new SkillCommand(SkillTypes.PolleoShot.Proc));
            AddSub(SkillIDs.FinalBlow, 10, new SkillCommand(SkillTypes.FinalBlow.Proc));
            AddSub(SkillIDs.FirePractice, 11, new SkillCommand(SkillTypes.FirePractice.Proc));            
        }

        private static void InitThief()
        {
            AddSub(SkillIDs.SharpenedBlades, 11, new SkillCommand(SkillTypes.SharpenedBlades.Proc));
            AddSub(SkillIDs.FatalContract, 11, new SkillCommand(SkillTypes.FatalContract.Proc));
            AddSub(SkillIDs.VenomCoat, 6, new SkillCommand(SkillTypes.VenomCoat.Proc));
            AddSub(SkillIDs.ShadowStep, 11, new SkillCommand(SkillTypes.ShadowStep.Proc));
            AddSub(SkillIDs.Disguise, 11, new SkillCommand(SkillTypes.Disguise.Proc));
            AddSub(SkillIDs.RapidRun, 10, new SkillCommand(SkillTypes.RapidRun.Proc));
            AddSub(SkillIDs.ArmorSmash, 5, new SkillCommand(SkillTypes.ArmorSmash.Proc));
            AddSub(SkillIDs.StripArmor, 11, new SkillCommand(SkillTypes.StripArmor.Proc));
            AddSub(SkillIDs.ManhoodBreaker, 6, new SkillCommand(SkillTypes.ManhoodBreaker.Proc));
        }

        private static void InitEnchanter()
        {
            AddSub(SkillIDs.SwordStick, 11, new SkillCommand(SkillTypes.SwordStickMastery.Proc));
            AddSub(SkillIDs.MentalTraining, 11, new SkillCommand(SkillTypes.MentalTraining.Proc));
            AddSub(SkillIDs.Meditation, 5, new SkillCommand(SkillTypes.Meditation.Proc));
            AddSub(SkillIDs.MentalPower, 11, new SkillCommand(SkillTypes.MentalPower.Proc));
            AddSub(SkillIDs.Heal, 11, new SkillCommand(SkillTypes.Heal.Proc));
            AddSub(SkillIDs.WeaponBlessing, 6, new SkillCommand(SkillTypes.WeaponBlessing.Proc));
            AddSub(SkillIDs.ArmorBlessing, 6, new SkillCommand(SkillTypes.ArmorBlessing.Proc));
            AddSub(SkillIDs.FireBolt, 11, new SkillCommand(SkillTypes.FireBolt.Proc));
            AddSub(SkillIDs.IceBolt, 11, new SkillCommand(SkillTypes.IceBolt.Proc));
            AddSub(SkillIDs.SummonLightning, 11, new SkillCommand(SkillTypes.SummonLightning.Proc));
            AddSub(SkillIDs.SwordstickStance, 3, new SkillCommand(SkillTypes.SwordstickStance.Proc));
            AddSub(SkillIDs.SinisterStrike, 5, new SkillCommand(SkillTypes.SinisterStrike.Proc));           
            
        }

        private static void AddSub(SkillIDs baseskill, int levels, SkillCommand func)
        {
            for (int i = 0; i < levels; i++)
            {
                SkillCommands.Add((SkillIDs)((int)baseskill + i), func);
            }
        }

        private static void Init12_()
        {
            SkillCommands.Add(SkillIDs.OpenBox, new SkillCommand(SkillTypes.OpenBox.Proc));

        }
    }
}
