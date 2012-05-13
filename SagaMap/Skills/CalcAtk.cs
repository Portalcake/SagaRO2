using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaDB.Items;
using SagaMap.Scripting;
using SagaLib;

namespace SagaMap.Skills
{
    partial class SkillHandler
    {
       internal static void CalcAtk(ref Actor actor)
        {
            ActorPC pc;
            ActorNPC npc;
            switch (actor.type)
            {
                case ActorType.PC :
                    pc = (ActorPC)actor;
                    CalcAtkPC(ref pc);
                    CalcRAtkPC(ref pc);
                    break;
                case ActorType.NPC :
                    npc = (ActorNPC)actor;
                    CalcAtkNPC(ref npc);
                    break;
            }
        }

       public static void GetPCATK(ActorPC pc, out int min, out int max)
       {
           WeaponInfo weapon = null;
           Weapon activeweapon;
           activeweapon = SagaDB.Items.WeaponFactory.GetActiveWeapon(pc);
           if (activeweapon != null) weapon = WeaponFactory.GetWeaponInfo((byte)activeweapon.type, activeweapon.level);
           if (weapon != null)
           {
               min = (int)weapon.minatk + pc.str;
               max = (int)weapon.maxatk + ((pc.str + pc.BattleStatus.strbonus) * 2);
               if (min > max) min = max;               
           }
           else
           {
               min = pc.str + pc.BattleStatus.strbonus;
               max = (pc.str + pc.BattleStatus.strbonus) * 2;
               if (min > max) min = max;               
           }   
       }

        private static void CalcAtkPC(ref ActorPC pc)
        {
            WeaponInfo weapon=null;
            Weapon activeweapon;
            uint weapondmg = 0;
            activeweapon = SagaDB.Items.WeaponFactory.GetActiveWeapon(pc);
            if (activeweapon != null) weapon = WeaponFactory.GetWeaponInfo((byte)activeweapon.type, activeweapon.level);
            if (weapon != null)
            {
                int min, max;
                min = (int)weapon.minatk + pc.str + pc.BattleStatus.strbonus + pc.BattleStatus.atkminbonus;
                max = (int)weapon.maxatk + ((pc.str + pc.BattleStatus.strbonus) * 2) + pc.BattleStatus.atkmaxbonus;
                if (min > max) min = max;
                weapondmg = (uint)Global.Random.Next(min, max);
            }
            else
            {
                int min, max;
                min = pc.str + pc.BattleStatus.strbonus + pc.BattleStatus.atkminbonus;
                max = (pc.str + pc.BattleStatus.strbonus) * 2 + pc.BattleStatus.atkmaxbonus;
                if (min > max) min = max;
                weapondmg = (uint)Global.Random.Next(min,max);
            }            
            pc.BattleStatus.atk = (int)weapondmg + pc.BattleStatus.atkskill + pc.BattleStatus.atkbonus;
            if (pc.BattleStatus.atk < 0) pc.BattleStatus.atk = 0;
        }

        private static void CalcRAtkPC(ref ActorPC pc)
        {
            WeaponInfo weapon = null;
            Weapon activeweapon;
            uint weapondmg = 0;
            activeweapon = SagaDB.Items.WeaponFactory.GetActiveWeapon(pc);
            if (activeweapon != null) weapon = WeaponFactory.GetWeaponInfo((byte)activeweapon.type, activeweapon.level);
            if (weapon != null)
            {
                int min, max;
                min = (int)weapon.minrangeatk + ((pc.con + pc.BattleStatus.conbonus) * 2) + pc.BattleStatus.ratkminbonus;
                max = (int)weapon.maxrangeatk + ((pc.con + pc.BattleStatus.conbonus) * 4) + pc.BattleStatus.ratkmaxbonus;
                if (min > max) min = max;
                weapondmg = (uint)Global.Random.Next(min, max);
            }
            else
            {
                int min, max;
                min = ((pc.con + pc.BattleStatus.conbonus) * 2) + pc.BattleStatus.ratkminbonus;
                max = ((pc.con + pc.BattleStatus.conbonus) * 4) + pc.BattleStatus.ratkmaxbonus;
                if (min > max) min = max;
                weapondmg = (uint)Global.Random.Next(min, max);
            }
            pc.BattleStatus.ratk = (int)weapondmg + pc.BattleStatus.ratkskill + pc.BattleStatus.ratkbonus;
        }
       
       private static void CalcAtkNPC(ref ActorNPC npc)
        {
            try
            {
                Mob mob = (Mob)npc.e;
                npc.BattleStatus.atk = Global.Random.Next((int)mob.MinAtk, (int)mob.MaxAtk) + npc.BattleStatus.atkskill;
            }
            catch (Exception) { }
        }

        internal static void CalcMAtk(ref Actor actor)
        {
            ActorPC pc;
            ActorNPC npc;
            switch (actor.type)
            {
                case ActorType.PC:
                    pc = (ActorPC)actor;
                    CalcMAtkPC(ref pc);
                    break;
                case ActorType.NPC:
                    npc = (ActorNPC)actor;
                    CalcMAtkNPC(ref npc);
                    break;
            }
        }

        private static void CalcMAtkPC(ref ActorPC pc)
        {
            WeaponInfo weapon = null;
            Weapon activeweapon;
            uint weapondmg = 0;
            activeweapon = SagaDB.Items.WeaponFactory.GetActiveWeapon(pc);
            if (activeweapon != null) weapon = WeaponFactory.GetWeaponInfo((byte)activeweapon.type, activeweapon.level);
            if (weapon != null)
            {
                int min, max;
                min = (int)weapon.minmatk + ((pc.intel + pc.BattleStatus.intbonus) * 3) + pc.BattleStatus.matkminbonus;
                max = (int)weapon.maxmatk + ((pc.intel + pc.BattleStatus.intbonus) * 6) + pc.BattleStatus.matkmaxbonus;
                if (min > max) min = max;
                weapondmg = (uint)Global.Random.Next(min, max);
            }
            else
            {
                int min, max;
                min = (pc.intel + pc.BattleStatus.intbonus) * 3 + pc.BattleStatus.matkminbonus;
                max = (pc.intel + pc.BattleStatus.intbonus) * 6 + pc.BattleStatus.matkmaxbonus;
                if (min > max) min = max;
                weapondmg = (uint)Global.Random.Next(min, max);
            }
            pc.BattleStatus.matk = (int)weapondmg + pc.BattleStatus.matkskill + pc.BattleStatus.matkbonus;
        }

        private static void CalcMAtkNPC(ref ActorNPC npc)
        {

        }
    }
}
