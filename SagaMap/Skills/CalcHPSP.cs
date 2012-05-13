using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaDB.Items;

using SagaLib;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
        public static void CalcHPSP(ref ActorPC pc)
        {
            pc.BattleStatus.hpBasic = (int)(200 + ((pc.cLevel - 1) * 40) + (pc.str * 10) + GetJobHPBonus(pc.job));
            pc.maxHP = (ushort)(pc.BattleStatus.hpBasic + pc.BattleStatus.hpskill + pc.BattleStatus.hpbonus);
            if (pc.HP > pc.maxHP) pc.HP = pc.maxHP;
            pc.BattleStatus.spBasic = 100 + GetJobSPBonus(pc.job);
            if (pc.job == JobType.ENCHANTER)
            {
                pc.maxSP = (ushort)(pc.BattleStatus.spBasic + pc.BattleStatus.spskill + pc.BattleStatus.spbonus);
            }
            else
                pc.maxSP = (ushort)(pc.BattleStatus.spBasic + pc.BattleStatus.spskill + pc.BattleStatus.spbonus);
            if (pc.SP > pc.maxSP) pc.SP = pc.maxSP;
        }

        private static short GetJobHPBonus(JobType job)
        {
            switch (job)
            {
                case JobType.ENCHANTER :
                    return 0;
                case JobType.SWORDMAN :
                    return 0;
            }
            return 0;
        }

        private static ushort GetJobSPBonus(JobType job)
        {
            switch (job)
            {
                case JobType.ENCHANTER:
                    return 0;
                default :
                    return 0;                    
            }            
        }
    }
}
