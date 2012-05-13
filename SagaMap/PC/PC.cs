using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skills;
namespace SagaMap
{
    public class PC
    {
        private MapClient client;

        public PC(MapClient client)
        {
            this.client = client;
        }

        public ActorPC Char
        {
            get
            {
                return client.Char;
            }
        }

        public void OnMapLoaded()
        {
            this.Char.invisble = false;
            this.client.map.OnActorVisibilityChange(this.Char);
            this.client.map.SendTimeWeatherToActor(this.Char);
            this.Char.e.OnMapLoaded();
            this.client.map.SendVisibleActorsToActor(this.Char);
            this.client.SendMaxInvSlots();
            this.client.SendInventoryList();
            this.client.SendEquipList();
            this.client.SendWeaponList();
            this.client.SendSkillList();
            //this.client.SendShortcutsList();
            this.client.SendZeny();
            Quest.QuestsManager.SendNavPoint(this.Char);
            if (!this.client.initialized)
            {
                this.client.SendQuestInfo();
                Scenario.SetCurrentScenario(this.Char, 2, this.Char.Scenario);
                Quest.QuestsManager.UpdateQuestItem(this.Char, true);
                if (Config.Instance.MessageOfTheDay != null)
                {
                    foreach (string i in Config.Instance.MessageOfTheDay)
                    {
                        this.client.SendMessage("Saga", i, SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE_RED);
                    }
                }
                this.client.CheckNewMail();
                this.client.initialized = true;
            }
            if (MapServer.ScriptManager.Scenario != null)
                MapServer.ScriptManager.Scenario.OnChangeMap(this.Char, Char.mapID);
            else
            {
                Logger.ShowDebug("MapServer.ScriptManager.Scenario==null, maybe scenario script is missing?", null);
            }

            if (this.Char.stance == Global.STANCE.DIE)
            {
                this.Char.stance = Global.STANCE.REBORN;
                this.client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, null, this.Char, true);
            }
            if (this.Char.Tasks.ContainsKey("AutoSave") == false)
            {
                Tasks.AutoSave newtask = new SagaMap.Tasks.AutoSave(this.client);
                this.Char.Tasks.Add("AutoSave", newtask);
                newtask.Activate();
            }
            if (this.Char.Tasks.ContainsKey("RegenerationHP") == false)
            {
                Tasks.Regeneration newtask = new SagaMap.Tasks.Regeneration(this.client, 2, 0, 1000);
                this.Char.Tasks.Add("RegenerationHP", newtask);
                newtask.Activate();
            }
            if (this.Char.Tasks.ContainsKey("RegenerationSP") == false)
            {
                Tasks.Regeneration newtask = new SagaMap.Tasks.Regeneration(this.client, 0, 35, 4000);
                this.Char.Tasks.Add("RegenerationSP", newtask);
                newtask.Activate();
            }
            if (this.Char.Tasks.ContainsKey("LPReduction") == false)
            {
                Tasks.LPReduction newtask = new SagaMap.Tasks.LPReduction(this.client);
                this.Char.Tasks.Add("LPReduction", newtask);
                newtask.Activate();
            }
            Bonus.BonusHandler.Instance.CalcEquipBonus(this.Char);
            SkillHandler.CastPassivSkill(ref this.client.Char);
            SkillHandler.CalcHPSP(ref this.client.Char);
            this.client.SendBattleStatus();
            this.Char.LC = this.Char.maxLC;
            this.client.SendCharStatus(0);
            this.client.SendExtStats();
            SkillHandler.SendAllStatusIcons(this.Char, this.Char);
            //if (this.client.Party != null)
            //{
            //    this.client.Party.UpdateMemberInfo(this.client);
            //}
            //this.SendMessage("Saga", "Welcome to Saga!", Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
            Packets.Server.ShowMapInfo p = new SagaMap.Packets.Server.ShowMapInfo();
            p.SetMapInfo(this.Char.MapInfo);
            this.client.netIO.SendPacket(p, client.SessionID);
        }

        public void OnJobChange(byte NewJob,byte ChangeWeapon, ushort postfix)
        {
            //removing passive status

            #region Old Addition handling
            List<string> dellist = new List<string>();
            foreach (string i in this.Char.Tasks.Keys)
            {
                try
                {
                    if (i == "AutoSave" || i == "RegenerationHP" || i == "RegenerationSP" || i == "LPReduction")
                        continue;
                    MultiRunTask task = this.Char.Tasks[i];
                    task.Deactivate();
                    dellist.Add(i);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            foreach (string i in dellist)
            {
                this.Char.Tasks.Remove(i);
            }
            #endregion

            #region New Addition Handling
            Addition[] additionlist = new Addition[this.Char.BattleStatus.Additions.Count];
            this.Char.BattleStatus.Additions.Values.CopyTo(additionlist, 0);
            foreach (Addition i in additionlist)
            {
                if (i.Activated)
                    i.AdditionEnd();
            }
            this.Char.BattleStatus.Additions.Clear();
            #endregion

            if (!this.Char.JobLevels.ContainsKey(this.Char.job))
            {
                this.Char.JobLevels.Add(this.Char.job, (byte)this.Char.jLevel);
                MapServer.charDB.NewJobLevel(this.Char, this.Char.job, (byte)this.Char.jLevel);
            }
            else
            {
                this.Char.JobLevels[this.Char.job] = (byte)this.Char.jLevel;
                MapServer.charDB.UpdateJobLevel(this.Char, this.Char.job, (byte)this.Char.jLevel);
            }
            this.Char.job = (JobType)NewJob;
            if (!this.Char.JobLevels.ContainsKey(this.Char.job))
            {
                this.Char.JobLevels.Add(this.Char.job, 1);
                MapServer.charDB.NewJobLevel(this.Char, this.Char.job, 1);
            }
            this.Char.jLevel = this.Char.JobLevels[this.Char.job];
            this.Char.jExp = ExperienceManager.Instance.GetExpForLevel(this.Char.jLevel - 1, ExperienceManager.LevelType.JLEVEL);

            Weapon weapon = WeaponFactory.GetActiveWeapon(this.Char);                
            //Change weapon
            if (ChangeWeapon == 1)
            {
                WeaponInfo info;
                switch (this.Char.job)
                {
                    case JobType.ENCHANTER:
                        weapon.type = (ushort)WeaponType.SWORD_STICK;
                        weapon.augeSkillID = 150029;
                        break;
                    case JobType.SWORDMAN:
                        weapon.type = (ushort)WeaponType.LONG_SWORD;
                        weapon.augeSkillID = 150015;
                        break;
                    case JobType.THIEF:
                        weapon.type = (ushort)WeaponType.SHORT_SWORD;
                        weapon.augeSkillID = 150001;
                        break;
                    case JobType.RECRUIT:
                        weapon.type = (ushort)WeaponType.DAMPTFLINTE;
                        weapon.augeSkillID = 150043;
                        break;
                    case JobType.CLOWN:
                        weapon.type = (ushort)WeaponType.SHORT_SWORD;
                        weapon.augeSkillID = 150001;
                        break;
                    case JobType.NOVICE:
                        weapon.type = (ushort)WeaponType.SHORT_SWORD;
                        weapon.augeSkillID = 150001;
                        break;
                }
                info = WeaponFactory.GetWeaponInfo((byte)weapon.type, weapon.level);
                if (weapon.durability > info.maxdurability) weapon.durability = (ushort)info.maxdurability;
            }
            /*this.Char.str = 5;
            this.Char.dex = 3;
            this.Char.con = 3;
            this.Char.intel = 2;
            this.Char.luk = 0;
            this.Char.stpoints = (byte)(2 * (this.Char.cLevel - 1));*/
            SkillHandler.SkillResetOnJobChange(this.Char);
            SkillHandler.CalcHPSP(ref this.client.Char);
            this.client.SendCharStatus(0);
            this.client.SendBattleStatus();
            this.client.SendExtStats();
            Packets.Server.WeaponTypeChange p = new SagaMap.Packets.Server.WeaponTypeChange();
            p.SetType(weapon.type);
            p.SetWeaponAuge(weapon.augeSkillID);
            p.SetPostFix(postfix);
            this.client.netIO.SendPacket(p, client.SessionID);
            //if (this.checkTwoHanded())  //this is not working
                this.MakeInactive(EQUIP_SLOT.LEFT_HAND);
            Packets.Server.ChangedJob p2 = new SagaMap.Packets.Server.ChangedJob();
            p2.SetJob(NewJob);
            p2.SetUnknown(this.Char.id);
            this.client.netIO.SendPacket(p2, client.SessionID);
            if (this.Char.CurTarget != null)
            {
                Npc npc = (Npc)this.Char.CurTarget.e;
                npc.NPCChat(this.Char, 1758);
            }
            else
            {
                Logger.ShowDebug(this.Char.name + "->CurTarget == null ", null);
            }
            if (this.client.Party != null)
                this.client.Party.UpdateMemberInfo(this.client);
        }

        public bool checkTwoHanded()
        {
            if ((this.Char.inv.EquipList[EQUIP_SLOT.LEFT_HAND].equiped) && (this.Char.inv.EquipList[EQUIP_SLOT.RIGHT_HAND].equiped) && (this.Char.inv.EquipList[EQUIP_SLOT.LEFT_HAND].id == this.Char.inv.EquipList[EQUIP_SLOT.RIGHT_HAND].id))
                return true;
            else
                return false;
        }

        public void MakeInactive(EQUIP_SLOT slot)
        {
            Item item;
            if (!this.Char.inv.EquipList.ContainsKey(slot)) return;
            item = this.Char.inv.EquipList[slot];
            if (item.active == 0) return;
            item.active = 0;
            Bonus.BonusHandler.Instance.EquiqItem(this.Char, item, true);             
            Packets.Server.ItemAdjust p = new SagaMap.Packets.Server.ItemAdjust();
            p.SetContainer(1);
            p.SetSlot((byte)slot);
            p.SetFunction(SagaMap.Packets.Server.ItemAdjust.Function.Active);
            p.SetValue(0);
            this.client.netIO.SendPacket(p, client.SessionID);
        }

        public void OnStatUpdate(ushort str, ushort dex, ushort intel, ushort con, byte pointsleft)
        {
            bool cheat_detected = false;

            // check wheter all new stats are bigger or at least equal to their old values
            int deltaSTR = str - this.Char.str;
            if (deltaSTR < 0) cheat_detected = true;

            int deltaDEX = dex - this.Char.dex;
            if (deltaDEX < 0) cheat_detected = true;

            int deltaINTEL = intel - this.Char.intel;
            if (deltaINTEL < 0) cheat_detected = true;

            int deltaCON = con - this.Char.con;
            if (deltaCON < 0) cheat_detected = true;

            // check wheter the player tries to use more stpoints then he does have
            int delta = deltaSTR + deltaDEX + deltaINTEL + deltaCON;
            if (this.Char.stpoints < delta) cheat_detected = true;

            /// verify the value of p.PointsLeft()
            if ((this.Char.stpoints - delta) != pointsleft) cheat_detected = true;

            if (cheat_detected)
            {
                this.client.SendMessage("Saga", "Stop cheating!");
                return;
            }

            this.Char.str = (byte)str;
            this.Char.dex = (byte)dex;
            this.Char.intel = (byte)intel;
            this.Char.con = (byte)con;
            this.Char.stpoints = pointsleft;

            SkillHandler.CalcHPSP(ref this.client.Char);
        }

        public void OnHomePoint()
        {
            if (this.Char.stance != Global.STANCE.DIE || this.Char.HP != 0) return;
            if (this.Char.save_map == 0)
            {
                this.Char.save_map = 3;
                this.Char.save_x = -4811.951f;
                this.Char.save_y = 15936.05f;
                this.Char.save_z = 3894f;
            }
            Map map;
            MapManager.Instance.GetMap(this.Char.save_map, out map);
            this.Char.stance = Global.STANCE.REBORN;
            Packets.Server.ActorChangeState p1 = new SagaMap.Packets.Server.ActorChangeState();
            p1.SetActorID(this.Char.id);
            p1.SetBattleState(false);
            p1.SetStance(Global.STANCE.REBORN);
            if (this.Char.Tasks.ContainsKey("AutoSave") == false)
            {
                Tasks.AutoSave newtask = new SagaMap.Tasks.AutoSave(this.client);
                this.Char.Tasks.Add("AutoSave", newtask);
                newtask.Activate();
            }
            if (this.Char.Tasks.ContainsKey("RegenerationHP") == false)
            {
                Tasks.Regeneration newtask = new SagaMap.Tasks.Regeneration(this.client, 2, 0, 1000);
                this.Char.Tasks.Add("RegenerationHP", newtask);
                newtask.Activate();
            }
            if (this.Char.Tasks.ContainsKey("RegenerationSP") == false)
            {
                Tasks.Regeneration newtask = new SagaMap.Tasks.Regeneration(this.client, 0, 35, 4000);
                this.Char.Tasks.Add("RegenerationSP", newtask);
                newtask.Activate();
            }
            if (this.Char.Tasks.ContainsKey("LPReduction") == false)
            {
                Tasks.LPReduction newtask = new SagaMap.Tasks.LPReduction(this.client);
                this.Char.Tasks.Add("LPReduction", newtask);
                newtask.Activate();
            }
            this.Char.BattleStatus = new BattleStatus();
            this.Char.speed = 500;
            Bonus.BonusHandler.Instance.CalcEquipBonus(this.Char);
            SkillHandler.CastPassivSkill(ref this.client.Char);
            this.client.SendBattleStatus();
            this.client.SendCharStatus(0);
            SkillHandler.SendAllStatusIcons(this.Char, this.Char);            
            this.client.netIO.SendPacket(p1, client.SessionID);
        }

        public void OnChat(bool Atcommand,SagaMap.Packets.Client.GetChat.MESSAGE_TYPE type,string message)
        {
            if (Atcommand)
                if (AtCommand.Instance.ProcessCommand(this.client, message)) return;
            switch (type)
            {
                case SagaMap.Packets.Client.GetChat.MESSAGE_TYPE.YELL:
                    this.client.map.SendEventToAllActors(Map.TOALL_EVENT_TYPE.CHAT, new Map.ChatArgs(SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.YELL, message), this.Char, true);
                    break;
                case SagaMap.Packets.Client.GetChat.MESSAGE_TYPE.PARTY:
                    if (this.client.Party != null)
                    {
                        foreach (MapClient client in this.client.Party.Members)
                        {
                            client.SendMessage(this.Char.name, message, SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.PARTY);
                        }
                    }
                    break;
                case SagaMap.Packets.Client.GetChat.MESSAGE_TYPE.CHANEL:
                    foreach (Client i in MapClientManager.Instance.Clients().Values)
                    {
                        MapClient client;
                        if (i.GetType() != typeof(MapClient)) continue;
                        client = (MapClient)i;
                        if (client.Char == null) continue;
                        client.SendMessage(this.Char.name, message, SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.CHANEL);
                    }
                    break;
                default:
                    this.client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, new Map.ChatArgs(SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.NORMAL, message), this.Char, true);
                    break;
            }
        }

        public void OnChangeStatus(byte state, byte stance)
        {
            if (this.Char.Tasks.ContainsKey("RegenerationHP") && this.Char.Tasks.ContainsKey("RegenerationSP"))
            {
                try
                {
                    if (this.Char.stance != Global.STANCE.SIT && stance == (byte)Global.STANCE.SIT)
                    {
                        this.Char.BattleStatus.hpregbonus += 2;
                        this.Char.BattleStatus.spregbonus += 10;
                    }
                    if (this.Char.stance == Global.STANCE.SIT && stance != (byte)Global.STANCE.SIT)
                    {
                        this.Char.BattleStatus.hpregbonus -= 2;
                        this.Char.BattleStatus.spregbonus -= 10;
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            this.Char.state = state;
            this.Char.stance = (Global.STANCE)stance;
            
        }

        public void OnUseMap(byte map, byte value)
        {
            if (this.Char.MapInfo.ContainsKey(map))
            {
                this.Char.MapInfo[map] |= (byte)(128 / (Math.Pow(2, value - 1)));
                MapServer.charDB.UpdateMapInfo(this.Char, map, this.Char.MapInfo[map]);
            }
            else
            {
                this.Char.MapInfo.Add(map, (byte)(128 / (Math.Pow(2, value - 1))));
                MapServer.charDB.NewMapInfo(this.Char, map, this.Char.MapInfo[map]);
            }
        }

    }
}
