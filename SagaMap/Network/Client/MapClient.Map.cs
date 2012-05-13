//#define Preview_Version
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
    public partial class MapClient
    {

        #region "0x03"
        // 0x03 Packets =========================================

        /// <summary>
        /// Receive the packet from the client that demands for the mapid.
        /// </summary>
        public void OnSendDemandMapID(SagaMap.Packets.Client.SendDemandMapID p)
        {
            if (this.state != SESSION_STATE.IDENTIFIED) return;
            if (this.netIO == null)
            {
                MapClient client = null;
                for (uint i = 0xFFFFFFFF; i > 0xFFFFFF00; i--)
                {
                    if (MapClientManager.Instance.Clients().ContainsKey(i))
                    {
                        if (MapClientManager.Instance.Clients()[i].GetType() == typeof(MapClient))
                        {
                            Logger.ShowWarning("NetIO==null,fixed");
                            client = (MapClient)MapClientManager.Instance.Clients()[i];
                            i = 1;
                        }
                    }
                }
                this.netIO = client.netIO;
            }
            Packets.Server.SendStart sendPacket = new SagaMap.Packets.Server.SendStart();
            sendPacket.SetMapID(this.Char.mapID);
            sendPacket.SetLocation(this.Char.x, this.Char.y, this.Char.z);
            this.netIO.SendPacket(sendPacket, this.SessionID);

            this.state = SESSION_STATE.LOADING_MAP;
        }

        /// <summary>
        /// Get a packet from the client indicated it's done loading.
        /// </summary>
        public void OnSendMapLoaded(SagaMap.Packets.Client.SendMapLoaded p)
        {
            if (this.state != SESSION_STATE.LOADING_MAP) return;
            Pc.OnMapLoaded();
            Packets.Server.ReturnMapList p2 = new SagaMap.Packets.Server.ReturnMapList();
            p2.SetFromMap(this.Char.mapID);
            if (this.promisemap != 0)
                p2.SetToMap(this.promisemap);
            else
            {
                if (this.Char.save_map != 0)
                    p2.SetToMap(this.Char.save_map);
                else
                    p2.SetToMap(3);
            }
            this.netIO.SendPacket(p2, this.SessionID); ;
            this.state = SESSION_STATE.MAP_LOADED;
        }

        /// <summary>
        /// Receive the movement vector from the client. 
        /// Standard running speed is 500, or 250 for running backwards.
        /// </summary>
        public void OnSendMoveStart(SagaMap.Packets.Client.SendMoveStart p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (this.Char.speed == 0) this.Char.speed = 500;
            if (p.GetMoveType() == 1)
            {
                this.map.MoveActor(Map.MOVE_TYPE.START, this.Char, p.GetPos(), p.GetYaw(), p.GetAcceleration(), p.GetDelayTime(), (ushort)(this.Char.speed + this.Char.BattleStatus.speedbonus + this.Char.BattleStatus.speedskill));
            }
            else//if backwards
            {
                this.map.MoveActor(Map.MOVE_TYPE.START, this.Char, p.GetPos(), p.GetYaw(), p.GetAcceleration(), p.GetDelayTime(), (ushort)((this.Char.speed / 2) + this.Char.BattleStatus.speedbonus + this.Char.BattleStatus.speedskill));
            }
        }

        /// <summary>
        /// Receive the signal that a character stopped moving. 
        /// </summary>
        /// <param name="p"></param>
        public void OnSendMoveStop(SagaMap.Packets.Client.SendMoveStop p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            this.map.MoveActor(Map.MOVE_TYPE.STOP, this.Char, p.GetPos(), p.GetYaw(), null, p.GetDelayTime(), p.GetSpeed());
            if( this.Party != null )
            {
                this.Party.SendPosition(this);
            }
        }

        /// <summary>
        /// Receive the yaw from the client when the user rotates the screen.
        /// Should be used to calculate the direction to the monster for attacking
        /// </summary>
        public void OnSendYaw(SagaMap.Packets.Client.SendYaw p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            this.Char.yaw = p.GetYaw();
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.YAW_UPDATE, null, this.Char, false);
        }

        /// <summary>
        /// Receive the client jumping. If the client was sitting, remove the Regeneration bonus.
        /// Broardcast the jump to all actors.
        /// </summary>
        public void OnSendJump(SagaMap.Packets.Client.SendJump p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (this.Char.Tasks.ContainsKey("RegenerationHP") && this.Char.Tasks.ContainsKey("RegenerationSP"))
            {
                try
                {
                    Tasks.Regeneration hp = (Tasks.Regeneration)this.Char.Tasks["RegenerationHP"];
                    Tasks.Regeneration sp = (Tasks.Regeneration)this.Char.Tasks["RegenerationSP"];
                    if (this.Char.stance == Global.STANCE.SIT)
                    {
                        hp.hp -= 10;
                        sp.sp -= 10;
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            this.Char.stance = Global.STANCE.JUMP;
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, null, this.Char, false);
        }

        /// <summary>
        /// The character is going underwater, activate the oxygen task.
        /// </summary>
        public void OnDiveDown()
        {
            Packets.Server.Dive p = new SagaMap.Packets.Server.Dive();
            p.SetDirection(SagaMap.Packets.Server.Dive.Direction.DOWN);
            p.SetOxygen((uint)this.Char.LC);
            this.netIO.SendPacket(p, this.SessionID); ;
            Tasks.OxygenUsage O2;
            if (!this.Char.Tasks.ContainsKey("Oxygen Usage"))
            {
                O2 = new SagaMap.Tasks.OxygenUsage(this);
                this.Char.Tasks.Add("Oxygen Usage", O2);
            }
            else
                O2 = (Tasks.OxygenUsage)this.Char.Tasks["Oxygen Usage"];
            O2.diving = true;
            if (!O2.Activated())
                O2.Activate();
        }

        public void OnDiveUp()
        {
            Packets.Server.Dive p = new SagaMap.Packets.Server.Dive();
            p.SetDirection(SagaMap.Packets.Server.Dive.Direction.UP);
            p.SetOxygen((uint)this.Char.LC);
            this.netIO.SendPacket(p, this.SessionID); ;
            Tasks.OxygenUsage O2;
            if (!this.Char.Tasks.ContainsKey("Oxygen Usage"))
            {
                O2 = new SagaMap.Tasks.OxygenUsage(this);
                this.Char.Tasks.Add("Oxygen Usage", O2);
            }
            else
                O2 = (Tasks.OxygenUsage)this.Char.Tasks["Oxygen Usage"];
            O2.diving = false;
            if (!O2.Activated())
                O2.Activate();
        }

        /// <summary>
        /// When oxygen runs out, damage the char for 10% of health per second.
        /// </summary>
        public void OxygenTakeDamage()
        {
            uint damage = (uint)(this.Char.maxHP / 10);
            Actor pc = (Actor)this.Char;
            SagaMap.Map.SkillArgs args = new Map.SkillArgs();
            Skills.SkillHandler.PhysicalAttack(ref pc, ref pc, damage, SkillHandler.AttackElements.NEUTRAL, ref args);
            this.SendCharStatus(0);
            Packets.Server.TakeDamage p1 = new SagaMap.Packets.Server.TakeDamage();
            if (this.Char.HP == 0)
                p1.SetReason(SagaMap.Packets.Server.TakeDamage.REASON.OXYGEN_DEAD);
            else
                p1.SetReason(SagaMap.Packets.Server.TakeDamage.REASON.OXYGEN);
            p1.SetDamage(damage);
            this.netIO.SendPacket(p1, this.SessionID); ;
        }

        /// <summary>
        /// Receive the fall from the client.
        /// Damage the client for an 8th of the units fallen?
        /// Send the client the result of the fall.
        /// </summary>
        public void OnSendFall(Packets.Client.SendFall p)
        {
            uint value = p.GetValue();
            value /= 8;
            Actor pc = (Actor)this.Char;
            SagaMap.Map.SkillArgs args = new Map.SkillArgs();
            Skills.SkillHandler.PhysicalAttack(ref pc, ref pc, value, SkillHandler.AttackElements.NEUTRAL, ref args);
            this.SendCharStatus(0);
            Packets.Server.TakeDamage p1 = new SagaMap.Packets.Server.TakeDamage();
            if (this.Char.HP == 0)
                p1.SetReason(SagaMap.Packets.Server.TakeDamage.REASON.FALLING_DEAD);
            else if (this.Char.HP < (this.Char.maxHP / 10))
                p1.SetReason(SagaMap.Packets.Server.TakeDamage.REASON.FALLING_SURVIVE);
            else
                p1.SetReason(SagaMap.Packets.Server.TakeDamage.REASON.FALLING);
            p1.SetDamage(value);
            this.netIO.SendPacket(p1, this.SessionID); ;
        }

        /// <summary>
        /// Update the state of the client and send to all other actors
        /// </summary>
        public void OnSendChangeState(SagaMap.Packets.Client.SendChangeState p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            byte state, stance;
            state = p.GetState();
            stance = p.GetStance();
            this.Pc.OnChangeStatus(state, stance);
            SagaMap.Map.SkillArgs arg = new Map.SkillArgs();
            arg.targetActorID = p.GetTargetActor();

            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATE, arg, this.Char, true);
        }

        /// <summary>
        /// Receive a portal usage from the client, check the portal in the PortalManager
        /// Raise a warning if the portal isn't found.
        /// <seealso cref="PortalManager.GetPortal"/>
        /// </summary>
        public void OnSendUsePortal(SagaMap.Packets.Client.SendUsePortal p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            PortalManager.PortalInfo portal;
            portal = PortalManager.GetPortal(p.GetPortalID(), this.Char.mapID);
            if (portal.m_mapID != -1)
            {
                if (this.Char.mapID == portal.m_mapID)//if the destination is current map,there is no need to change the map.
                    this.map.TeleportActor(this.Char, portal.m_x, portal.m_y, portal.m_z);
                else
                    this.map.SendActorToMap(this.Char, Convert.ToByte(portal.m_mapID), portal.m_x, portal.m_y, portal.m_z);
            }
            else
            {
                if (this.Char.GMLevel >= 2)
                    this.SendMessage("Saga", "Cannot find Portal with toID:" + p.GetPortalID() + " and fromID:" + this.Char.mapID + " in database", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
                else
                    this.SendMessage("Saga", "This Portal is not implemented yet!", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);

                Logger.ShowWarning("Cannot find Portal with toID:" + p.GetPortalID() + " and fromID:" + this.Char.mapID + " in database");
            }
        }

        /// <summary>
        /// Call the job change method from the PC class.
        /// <see cref="Pc.OnJobChange"/>
        /// </summary>
        public void OnJobChange(Packets.Client.JobChange p)
        {
            Pc.OnJobChange(p.GetJob(), p.GetChangeWeapon(), p.GetPostFix());
        }

        /// <summary>
        /// Receive the stat update from the client.
        /// The input is checked in the PC class to make sure the client doesn't try and add points they don't have.
        /// Send all the updates to the client.
        /// <seealso cref="Pc.OnStatUpdate"/>
        /// </summary>
        public void OnStatUpdate(SagaMap.Packets.Client.GetStatUpdate p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            ushort str = p.GetStr();
            ushort dex = p.GetDex();
            ushort intel = p.GetIntel();
            ushort con = p.GetCon();
            byte pointsleft = p.PointsLeft();

            Pc.OnStatUpdate(str, dex, intel, con, pointsleft);

            this.SendExtStats();
            this.SendCharStatus(0);
            this.SendBattleStatus();
        }

        /// <summary>
        /// If the client is dead, send back to the home point.
        /// If the type is 1, send back to a save point at the Cattleya's co=ords.
        /// Remove some durability from the items as a dying penalty.
        /// </summary>
        public void OnSendHomePoint(SagaMap.Packets.Client.SendHomePoint p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (this.Char.stance != Global.STANCE.DIE || this.Char.HP != 0) return;

            Pc.OnHomePoint();

            if (p.GetType() == 1 && this.map.CattleyaMapID != 0)
            {
                float z;
                if (this.map.HasHeightMap())
                {
                    z = this.map.GetHeight(this.map.CattleyaX, this.map.CattleyaY);
                }
                else
                {
                    z = this.map.CattleyaZ;
                }
                this.map.SendActorToMap(this.Char, (byte)this.map.CattleyaMapID, this.map.CattleyaX, this.map.CattleyaY, z);
                SkillHandler.EquiptLoseDurabilityOnDeath(this.Char);
            }
            else
                this.map.SendActorToMap(this.Char, this.Char.save_map, this.Char.save_x, this.Char.save_y, this.Char.save_z);

        }

        public void OnJobChangeCosts(SagaMap.Packets.Client.JobChangeCosts p)
        {
            /*
             * TODO Add correct caluclating formulla here
             * Formulla should be something related to the jlvl of the character
             * Something along the lines of 500 + jvl * 200.
             * 
             * p.GetJob(); Get the job, use this to determine the selected job
             */

            Packets.Server.ChangeJobCosts p1 = new SagaMap.Packets.Server.ChangeJobCosts();
            p1.SetZeny(500);
            this.netIO.SendPacket(p1, this.SessionID);
        }


        #endregion        

    }
}
