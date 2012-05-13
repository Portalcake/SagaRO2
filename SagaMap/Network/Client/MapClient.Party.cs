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

        #region "0x0D 0x0E"
        // 0x0D / 0x0E Packets ==================================
        /// <summary>
        /// Will be called if a player sends a party invite packet
        /// </summary>
        /// <param name="p">packet instance containing the to be invited player's name</param>
        public void OnPartyInvite(SagaMap.Packets.Client.PartyInvite p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            try
            {
                string name = p.GetName();
                if (name == this.Char.name)// Check if the player is already in a party
                {
                    this.SendPartyInviteResult(SagaMap.Packets.Server.SendPartyInviteResult.Result.ALREADY_IN_PARTY);
                }
                MapClient client = MapClientManager.Instance.GetClient(name);
                if (client == null)//Check if player with that name exists
                {
                    this.SendPartyInviteResult(SagaMap.Packets.Server.SendPartyInviteResult.Result.NOT_EXIST);
                }
                else
                {
                    if (client.Party != null)
                    {
                        this.SendPartyInviteResult(SagaMap.Packets.Server.SendPartyInviteResult.Result.ALREADY_IN_PARTY);
                    }
                    else
                    {
                        if (this.Party != null)
                        {
                            if (this.Party.Members.Count == 5)
                            {
                                this.SendPartyInviteResult(SagaMap.Packets.Server.SendPartyInviteResult.Result.MAX_MEMBER);
                                return;
                            }
                        }
                        this.Char.PartyTarget = client.Char.id;
                        this.Char.PartyStatus = SagaDB.Actors.Party.PENDING;
                        client.Char.e.OnPartyInvite(this.Char);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }

        public void OnPartyAccept(SagaMap.Packets.Client.PartyAccept p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            if (this.Party != null) return;
            try
            {
                byte status = p.GetStatus();
                MapClient target = (MapClient)MapClientManager.Instance.GetClient(this.Char.PartyTarget);
                if (target != null)
                {
                    if (status == 1)
                    {
                        target.SendPartyInviteResult(SagaMap.Packets.Server.SendPartyInviteResult.Result.OK);
                        SagaMap.Party.Party party;
                        if (target.Party == null)
                        {
                            party = new SagaMap.Party.Party();
                            party.Leader = target;
                            party.AddMember(target);
                            target.Party = party;
                        }
                        else
                        {
                            party = target.Party;
                        }
                        party.AddMember(this);
                        this.Party = party;
                        //this.Char.PartyStatus = SagaDB.Actors.Party.IN_PARTY;

                    }
                    else // They didnt accept;
                    {
                        this.Char.PartyTarget = 0;
                        target.SendPartyInviteResult(SagaMap.Packets.Server.SendPartyInviteResult.Result.DENIED);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }

        public void OnPartyInviteCancel(SagaMap.Packets.Client.InviteCancel p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            try
            {
                MapClient client = (MapClient)MapClientManager.Instance.GetClient(this.Char.PartyTarget);
                if (client == null)
                {
                    this.Char.PartyTarget = 0;
                    return;
                }
                ActorPC target = client.Char;
                if (target != null)
                {
                    target.PartyTarget = 0;
                    this.Char.PartyTarget = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }

        public void OnPartyQuit(SagaMap.Packets.Client.PartyQuit p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            if (this.Party != null)
            {
                this.Party.RemoveMember(this);
            }
        }

        public void OnPartyKick(SagaMap.Packets.Client.PartyKick p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            if (this.Party != null)
            {
                this.Party.RemoveMember(this, p.GetIndex());
            }
        }

        public void OnPartyMode(SagaMap.Packets.Client.PartyMode p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            if (this.Party != null)
            {
                this.Party.SetSharing((SagaMap.Party.Party.ELoot_Setting)p.GetLootShare(), (SagaMap.Party.Party.EXP_Setting)p.GetExpShare());
            }

            //Console.WriteLine("loot: {0} xp: {1}", loot, xp);
        }

        #endregion

    }
}
