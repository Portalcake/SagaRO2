using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaDB;
using SagaDB.Actors;
using SagaDB.Items;
using SagaDB.Mail;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skills;

namespace SagaMap
{
    [Serializable]
    public partial class MapClient : SagaLib.Client
    {
        #region "Fields"
        /// <summary>
        /// The <see cref="ActorPC"/> associated with this session.
        /// </summary>
        public ActorPC Char;

        public PC Pc;

        public enum SESSION_STATE
        {
            NOT_IDENTIFIED, IDENTIFIED, LOADING_MAP, MAP_LOADED, LOGGEDOFF
        }
        public SESSION_STATE state;

        /// <summary>
        /// The <see cref="Map"/> associated with this session.
        /// </summary>
        public Map map;


        /// <summary>
        /// The time of the last heartbeat answer from the client.
        /// </summary>
        public long lastHeartbeat, lastHeartbeatRequest;

        private Tasks.SystemTasks.CheckHeartbeat taskHeartbeat;

        public bool initialized = false;

        //Promise stone
        private byte promisemap;
        private float promiseX;
        private float promiseY;
        private float promiseZ;

        //quests related
        private bool QuestConfirm = false;
        private List<Packets.Client.RepaireInfo> RepaireList;
        private byte Repaireread;
        private uint completingquest;
        public Delegate RewardFunc;
        public List<Item> RewardChoice;

        //public Dictionary<uint, uint> QuestMobItem;
        public Dictionary<Item, byte> TradeItems;
        public int TradeMoney;

        public Party.Party Party;
        #endregion

        #region "Constructor"
        public MapClient( Socket mSock, Dictionary<ushort, Packet> mCommandTable ,uint session)
        {
            this.SessionID = session;
            this.netIO = new NetIO(mSock, mCommandTable, this, MapClientManager.Instance);
            this.state = SESSION_STATE.NOT_IDENTIFIED;

            this.lastHeartbeatRequest = 0;
            this.taskHeartbeat = new SagaMap.Tasks.SystemTasks.CheckHeartbeat( this );

            if( this.netIO.sock.Connected ) this.OnConnect();
        }

        public MapClient(uint session)
        {
            this.SessionID = session;
            this.state = SESSION_STATE.NOT_IDENTIFIED;
            this.lastHeartbeatRequest = 0;
            this.taskHeartbeat = new SagaMap.Tasks.SystemTasks.CheckHeartbeat(this);

        }
        #endregion

        public override string ToString()
        {
            try
            {
                if (this.Char != null) return this.Char.name;
                   else
                    return "MapClient";
            }
            catch (Exception)
            {
                return "MapClient";
            }
        }

        #region "On Disconnect"
        public override void OnDisconnect()
        {
            try
            {
                MapClientManager.Instance.OnClientDisconnect(this);
                if (this.state == SESSION_STATE.LOGGEDOFF || this.Char == null) return;
                //Respawn people who disconnect on death
                if (this.Char.stance == Global.STANCE.DIE)
                {
                    this.Char.mapID = this.Char.save_map;
                    this.Char.x = this.Char.save_x;
                    this.Char.y = this.Char.save_y;
                    this.Char.z = this.Char.save_z;
                }
                Logger.ShowInfo("Player:" + this.Char.name + " logged off");
                this.state = SESSION_STATE.LOGGEDOFF;
                Logger.ShowInfo("Total Player count:" + MapClientManager.Instance.Players.Count.ToString());
                this.taskHeartbeat.Deactivate();
                foreach( MultiRunTask i in this.Char.Tasks.Values )
                {
                    try
                    {
                        i.Deactivate();
                    }
                    catch( Exception )
                    {
                    }
                }
                this.Char.Tasks.Clear();
                switch( this.state )
                {
                    case SESSION_STATE.LOADING_MAP:
                    case SESSION_STATE.LOGGEDOFF:
                    case SESSION_STATE.MAP_LOADED:
                        this.map.DeleteActor( this.Char );
                        this.Char.Online = 0;
                        MapServer.charDB.SaveChar( this.Char );                        
                        break;
                };
                this.state = SESSION_STATE.LOGGEDOFF;
                if (this.Party != null) this.Party.RemoveMember(this);
                
            }
            catch (Exception ex) { Logger.ShowError(ex); }

        }
        #endregion

        #region "Methods for chat/char status/Ext stats"
        /// <summary>
        /// Send a message to the client
        /// </summary>
        /// <param name="from">Who to display the message from</param>
        /// <param name="text">The text to send to the client</param>
        public void SendMessage( string from, string text )
        {
            while (text != "")
            {
                string tmp;
                if (text.Length < 62)
                {
                    tmp = text;
                    text = "";
                }
                else
                {
                    tmp = text.Substring(0, 62);
                    text = text.Substring(62, text.Length - 62);
                }
                Packets.Server.SendChat sendPacket = new Packets.Server.SendChat(tmp.Length);
                sendPacket.SetName(from);
                sendPacket.SetMessage(tmp);
                sendPacket.SetMessageType(0);
                this.netIO.SendPacket(sendPacket, this.SessionID);;
            }
        }

        /// <summary>
        /// Send a message to the client
        /// </summary>
        /// <param name="from">Who to display the message from</param>
        /// <param name="text">The text to send to the client</param>
        /// <param name="type">Type: NORMAL, PARTY, YELL, SYSTEM_MESSAGE, CHANEL, SYSTEM_MESSAGE_RED</param>
        public void SendMessage( string from, string text, Packets.Server.SendChat.MESSAGE_TYPE type )
        {
            while (text != "")
            {
                string tmp;
                if (text.Length < 62)
                {
                    tmp = text;
                    text = "";
                }
                else
                {
                    tmp = text.Substring(0, 62);
                    text = text.Substring(62, text.Length - 62);
                }
                Packets.Server.SendChat sendPacket = new Packets.Server.SendChat(tmp.Length);
                sendPacket.SetName(from);
                sendPacket.SetMessage(tmp);
                sendPacket.SetMessageType(type);
                this.netIO.SendPacket(sendPacket, this.SessionID);;
            }
        }

        /// <summary>
        /// Send all the status to the client.
        /// Send: Job, EXP, HP/SP, LC/LP and basic info on all other party members
        /// </summary>
		/// <param name="visiblefield">0 for none, 4 for jexp, 32 for cexp, 36 for both</param>
        public void SendCharStatus(ushort visiblefield)
        {
            try
            {
                if (this.state != SESSION_STATE.MAP_LOADED) return;                
                Packets.Server.CharStatus sendPacket = new Packets.Server.CharStatus();
                sendPacket.SetJob(this.Char.job);
                sendPacket.SetExp(this.Char.cExp, this.Char.jExp);
                sendPacket.SetHPSP(this.Char.HP, this.Char.maxHP, this.Char.SP, this.Char.maxSP);
                sendPacket.SetLCLP(this.Char.LC, this.Char.maxLC, this.Char.LP, this.Char.maxLP);
				sendPacket.SetVisibleField(visiblefield);
                if (this.Party != null)
                {
                    foreach (MapClient client in this.Party.Members)
                    {
                        Packets.Server.PartyMemberHPInfo p1 = new SagaMap.Packets.Server.PartyMemberHPInfo();
                        p1.SetIndex((byte)(this.Party.Members.IndexOf(this) + 1));
                        p1.SetActorID(this.Char.id);
                        p1.SetHP(this.Char.HP);
                        p1.SetMaxHP(this.Char.maxHP);
                        Packets.Server.PartyMemberSPInfo p2 = new SagaMap.Packets.Server.PartyMemberSPInfo();
                        p2.SetIndex((byte)(this.Party.Members.IndexOf(this) + 1));
                        p2.SetActorID(this.Char.id);
                        p2.SetSP(this.Char.SP);
                        p2.SetMaxSP(this.Char.maxSP);
                        client.netIO.SendPacket(p1, client.SessionID);
                        client.netIO.SendPacket(p2, client.SessionID);

                    }
                }
                this.netIO.SendPacket(sendPacket, this.SessionID);;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// Send the stats to the client
        /// TODO: Add stat bonuses for jobs.
        /// </summary>
        public void SendExtStats()
        {
            Packets.Server.CharExtStatus extStats = new SagaMap.Packets.Server.CharExtStatus();
            extStats.SetStatsBase2(this.Char.str, this.Char.dex, this.Char.intel, this.Char.con, this.Char.luk);
            extStats.SetStatsBonus((byte)this.Char.BattleStatus.strbonus, (byte)this.Char.BattleStatus.dexbonus, (byte)this.Char.BattleStatus.intbonus, (byte)this.Char.BattleStatus.conbonus, (byte)this.Char.BattleStatus.lukbonus); // temp soloution
            extStats.SetStatsBase1(0, 0, 0, 0, 0); // NOTE temp sloution
			extStats.SetStatsJob(0, 0, 0, 0, 0); // NOTE temp soloution;
            extStats.SetStatPoints(this.Char.stpoints);
            this.netIO.SendPacket(extStats, this.SessionID);
        }
        #endregion

        #region "methods for inventory/weapon/equipment/skill/zeny/quest"

        /// <summary>
        /// Send invetory slot count.
        /// Default value 254
        /// </summary>
        public void SendMaxInvSlots()
        {
            Packets.Server.SetInventorySlotCount p1 = new SagaMap.Packets.Server.SetInventorySlotCount();
            p1.SetSlotCount((byte)this.Char.inv.invMaxSlots);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        /// <summary>
        /// Get a list of the inventory items and send it to the client.
        /// </summary>
        public void SendInventoryList()
        {
            List<Item> inv = this.Char.inv.GetInventoryList();
            Packets.Server.ListInventory p1 = new SagaMap.Packets.Server.ListInventory((byte)inv.Count);
            //p1.SetSortType();
            p1.SetItems(inv);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        /// <summary>
        /// Get a list of the equipment (which is equiped, not just the items).
        /// </summary>
        public void SendEquipList()
        {
            Dictionary<EQUIP_SLOT, Item> equip = this.Char.inv.EquipList;
            Packets.Server.ListEquipment p1 = new SagaMap.Packets.Server.ListEquipment();
            p1.SetEquipment(equip);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        /// <summary>
        /// Send a list of weapons currently in use
        /// </summary>
        public void SendWeaponList()
        {
            Packets.Server.WeaponList packet = new SagaMap.Packets.Server.WeaponList();
            packet.SetWeapon(this.Char.Weapons);
            this.netIO.SendPacket(packet, this.SessionID); ;
        }

        /// <summary>
        /// Make an update to the weapon
        /// </summary>
        /// <param name="type">Level, EXP, Durability</param>
        /// <param name="value">value to add</param>
        public void UpdateWeaponInfo(Packets.Server.WeaponAdjust.Function type, uint value)
        {
            Packets.Server.WeaponAdjust p = new SagaMap.Packets.Server.WeaponAdjust();
            p.SetFunction(type);
            p.SetValue(value);
            this.netIO.SendPacket(p, this.SessionID);
        }

        /// <summary>
        /// Send a shortcut list to the client aka 'Quick Page'.
        /// </summary>
        public void SendShortcutsList()
        {
            Packets.Server.SendShortcutList packet2 = new SagaMap.Packets.Server.SendShortcutList();
            packet2.SetQuickPageType(0, 0);
            packet2.SetEntries(this.Char.ShorcutIDs);
            this.netIO.SendPacket(packet2, this.SessionID); ;
        }

        /// <summary>
        /// Add EXP to a skill and update the client.
        /// </summary>
        /// <param name="id">Skill Id to update</param>
        /// <param name="exp">Ammount of EXP</param>
        public void UpdateSkillEXP(uint id, uint exp)
        {
            if (!this.Char.BattleSkills.ContainsKey(id)) return;
            SkillInfo info = this.Char.BattleSkills[id];
            info.exp = exp;
            Packets.Server.SkillEXP p = new SagaMap.Packets.Server.SkillEXP();
            p.SetSkillID(id);
            p.SetEXP(exp);
            this.netIO.SendPacket(p, this.SessionID);
        }

        /// <summary>
        /// Send the skill list to the client.
        /// Skills sorted by Battle, Living and Special.
        /// </summary>
        public void SendSkillList()
        {
            Packets.Server.BattleSkill bskill = new Packets.Server.BattleSkill(this.Char.BattleSkills.Count);
            Packets.Server.BattleSkill.SkillInfo[] skills = new SagaMap.Packets.Server.BattleSkill.SkillInfo[this.Char.BattleSkills.Count];
            int j = 0;
            foreach (uint i in this.Char.BattleSkills.Keys)
            {
                skills[j].skillID = i;
                skills[j].exp = this.Char.BattleSkills[i].exp;
                j++;
            }
            bskill.SetSkills(skills);
            if (this.Char.BattleSkills.Count != 0) this.netIO.SendPacket(bskill, this.SessionID);
            Packets.Server.LivingSkill lskill = new Packets.Server.LivingSkill(this.Char.LivingSkills.Count);
            Packets.Server.LivingSkill.SkillInfo[] skills2 = new SagaMap.Packets.Server.LivingSkill.SkillInfo[this.Char.LivingSkills.Count];
            j = 0;
            foreach (uint i in this.Char.LivingSkills.Keys)
            {
                skills2[j].skillID = i;
                skills2[j].exp = this.Char.LivingSkills[i].exp;
                j++;
            }
            lskill.SetSkills(skills2);
            if (this.Char.LivingSkills.Count != 0) this.netIO.SendPacket(lskill, this.SessionID);
            Packets.Server.SpecialSkill sskill = new Packets.Server.SpecialSkill(this.Char.SpecialSkills.Count);
            Packets.Server.SpecialSkill.SkillInfo[] skills3 = new SagaMap.Packets.Server.SpecialSkill.SkillInfo[this.Char.SpecialSkills.Count];
            j = 0;
            foreach (uint i in this.Char.SpecialSkills.Keys)
            {
                skills3[j].skillID = i;
                skills3[j].exp = this.Char.SpecialSkills[i].exp;
                skills3[j].slot = this.Char.SpecialSkills[i].slot;
                j++;
                Packets.Server.SetSpecialSkill p1 = new SagaMap.Packets.Server.SetSpecialSkill();
                p1.SetSkill(i);
                p1.SetSlot(this.Char.SpecialSkills[i].slot);
                this.netIO.SendPacket(p1, this.SessionID);
            }
            sskill.SetSkills(skills3);
            if (this.Char.SpecialSkills.Count != 0) this.netIO.SendPacket(sskill, this.SessionID);
        }

        /// <summary>
        /// Update the Zeny of the client.
        /// One value for Zeny and Rufi. '12000' = 1 Zeni 2000 Rufi.
        /// </summary>
        public void SendZeny()
        {
            Packets.Server.SendZeny p1 = new SagaMap.Packets.Server.SendZeny();
            p1.SetZeny(this.Char.zeny);
            this.netIO.SendPacket(p1, this.SessionID);
        }

        /// <summary>
        /// Send the Quest information of a client
        /// </summary>
        public void SendQuestInfo()
        {
            List<SagaDB.Quest.Quest> quests = new List<SagaDB.Quest.Quest>();
            SagaDB.Quest.Quest q = Quest.QuestsManager.GetActiveQuest(this.Char);

            if (q != null)
            {
                quests.Add(q);
            }
            q = Quest.QuestsManager.GetActivePersonalQuest(this.Char);
            if (q != null)
            {
                quests.Add(q);
            }
            Packets.Server.QuestInfo p1 = new SagaMap.Packets.Server.QuestInfo();
            p1.SetQuestInfo(quests);
            this.netIO.SendPacket(p1, this.SessionID); ;
        }

        /// <summary>
        /// Update the Battle Status in client's equipment window
        /// </summary>
        public void SendBattleStatus()
        {
            Packets.Server.BattleStats p = new SagaMap.Packets.Server.BattleStats();
            p.SetCurseResist((ushort)this.Char.BattleStatus.curseresist);
            p.SetDarkResist((ushort)this.Char.BattleStatus.darkresist);
            p.SetDef((ushort)(this.Char.BattleStatus.defbonus + this.Char.BattleStatus.defskill));
            p.SetFireResist((ushort)this.Char.BattleStatus.fireresist);
            p.SetGhostResist((ushort)this.Char.BattleStatus.ghostresist);
            p.SetHolyResist((ushort)this.Char.BattleStatus.holyresist);
            p.SetIceResist((ushort)this.Char.BattleStatus.iceresist);
            p.SetMagicalFlee((ushort)(this.Char.BattleStatus.mflee + this.Char.BattleStatus.mfleebonus + this.Char.BattleStatus.mfleeskill));
            Weapon w = SagaDB.Items.WeaponFactory.GetActiveWeapon(this.Char);
            ushort minatk = (ushort)(((this.Char.intel + this.Char.BattleStatus.intbonus) * 3) + WeaponFactory.GetWeaponInfo((byte)w.type, (byte)w.level).minmatk + this.Char.BattleStatus.matkbonus + this.Char.BattleStatus.matkskill + this.Char.BattleStatus.matkminbonus);
            ushort maxatk = (ushort)(((this.Char.intel + this.Char.BattleStatus.intbonus) * 6) + WeaponFactory.GetWeaponInfo((byte)w.type, (byte)w.level).maxmatk + this.Char.BattleStatus.matkbonus + this.Char.BattleStatus.matkskill + this.Char.BattleStatus.matkmaxbonus);
            p.SetMagicAtk(maxatk);
            p.SetMagicBlue(minatk);
            minatk = (ushort)(((this.Char.str + this.Char.BattleStatus.strbonus)) + WeaponFactory.GetWeaponInfo((byte)w.type, (byte)w.level).minatk + this.Char.BattleStatus.atkbonus + this.Char.BattleStatus.atkskill + this.Char.BattleStatus.atkminbonus);
            maxatk = (ushort)(((this.Char.str + this.Char.BattleStatus.strbonus) * 2) + WeaponFactory.GetWeaponInfo((byte)w.type, (byte)w.level).maxatk + this.Char.BattleStatus.atkbonus + this.Char.BattleStatus.atkskill + this.Char.BattleStatus.atkmaxbonus);
            p.SetPhysicalAtk(maxatk);
            p.SetPhysicalBlue(minatk);
            p.SetPhysicalFlee((ushort)(this.Char.BattleStatus.flee + this.Char.BattleStatus.fleebonus + this.Char.BattleStatus.fleeskill));
            minatk = (ushort)((this.Char.con + this.Char.BattleStatus.conbonus) * 2 + WeaponFactory.GetWeaponInfo((byte)w.type, (byte)w.level).minrangeatk + this.Char.BattleStatus.ratkbonus + this.Char.BattleStatus.ratkskill + this.Char.BattleStatus.ratkminbonus);
            maxatk = (ushort)((this.Char.con + this.Char.BattleStatus.conbonus) * 4 + WeaponFactory.GetWeaponInfo((byte)w.type, (byte)w.level).maxrangeatk + this.Char.BattleStatus.ratkbonus + this.Char.BattleStatus.ratkskill + this.Char.BattleStatus.ratkmaxbonus);
            p.SetPhysicalRangeAtk(maxatk);
            p.SetRangeBlue(minatk);
            p.SetSpiritResist((ushort)this.Char.BattleStatus.spiritresist);
            p.SetWindResist((ushort)this.Char.BattleStatus.windresist);
            p.SetRangedFlee((ushort)(this.Char.BattleStatus.rflee + this.Char.BattleStatus.rfleebonus + +this.Char.BattleStatus.rfleeskill));
            this.netIO.SendPacket(p, this.SessionID);
        }
        #endregion


        // 0xFE Packets =========================================

        // FE 00
        public void OnHeartbeat( Packets.Client.Heartbeat p )
        {
            this.lastHeartbeat = DateTime.Now.Ticks;
        }

        public void RequestHeartbeat()
        {
            Packets.Server.RequestHeartbeat sendPacket = new SagaMap.Packets.Server.RequestHeartbeat();
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void CheckWeaponEXP()
        {
            Weapon weapon = WeaponFactory.GetActiveWeapon(this.Char);
            if (weapon == null) return;
			uint maxexp = ExperienceManager.Instance.GetExpForLevel(weapon.level, ExperienceManager.LevelType.WLEVEL);
			uint minexp = ExperienceManager.Instance.GetExpForLevel((uint)(weapon.level - 1), ExperienceManager.LevelType.WLEVEL);
            if (weapon.exp > maxexp || weapon.exp < minexp)
                weapon.exp = minexp;
        }



    }
}
