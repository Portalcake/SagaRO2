using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actors;

using SagaMap.Tasks;

namespace SagaMap.Skills.SkillTypes
{
    public static class ChakraaBreath
    {
        const SkillIDs baseID = SkillIDs.ChakraaBreath;
         public static void Proc(ref Actor sActor, ref Actor dActor,ref Map.SkillArgs args)
         {
             byte level;
             ActorPC pc=(ActorPC)sActor;
             Tasks.PassiveSkillStatus ss;
             Tasks.Regeneration rege =(Tasks.Regeneration) pc.Tasks["RegenerationSP"];
             args.damage = 0;
             args.isCritical = 0;
             level = (byte)(args.skillID - baseID + 1);
             if (!pc.BattleSkills.ContainsKey((uint)args.skillID))
             {
                 if (pc.Tasks.ContainsKey("ChakraaBreath"))
                 {
                     ss = (PassiveSkillStatus)pc.Tasks["ChakraaBreath"];
                    rege.sp -= (ushort)GetLevelValue(level);
                     pc.Tasks.Remove("ChakraaBreath");
                 } 
                 return;//if current weapon is not Short Sword
             }
             if (pc.Tasks.ContainsKey("ChakraaBreath"))
             {
                 ss = (PassiveSkillStatus)pc.Tasks["ChakraaBreath"];
                 rege.sp -= (ushort)GetLevelValue(ss.level);
                 ss.level = level;
                 rege.sp += (ushort)GetLevelValue(level);
                 
             }
             else
             {
                 ss = new PassiveSkillStatus(level);
                 rege.sp += (ushort)GetLevelValue(level);
                 pc.Tasks.Add("ChakraaBreath", ss);
                
             }
         }

        private static byte GetLevelValue(byte level)
        {
            if (level == 6) return 10; else return level;
        }
        
    }
}
