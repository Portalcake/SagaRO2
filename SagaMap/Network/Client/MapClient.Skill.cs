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

        #region "0x09"
        // 0x09 Packets =========================================
        public void OnSkillCast(Packets.Client.SkillCast p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            // Attack
            if (p.GetSkillType() != 9)
            {
                Map.SkillArgs sArgs;
                Actor aActor = this.map.GetActor(p.GetTargetActorID());
                Actor sActor = (Actor)this.Char;
                sArgs = new Map.SkillArgs(p.GetSkillType(), 0, p.GetSkillID(), p.GetTargetActorID(), 0);
                sArgs.casting = true;
                if (aActor == null)
                {
                    sArgs.isCritical = Map.SkillArgs.AttackResult.Miss;
                    sArgs.failed = true;
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, sArgs, this.Char, true);
                    return;
                }
                //cast skill
                if (aActor.type == ActorType.NPC)
                {
                    Map.ActorSelArgs arg = new Map.ActorSelArgs(aActor.id);
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
                }
                if (aActor.type == ActorType.PC && aActor.id != this.Char.id)
                {
                    Map.ActorSelArgs arg = new Map.ActorSelArgs(aActor.id);
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
                }

                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, sArgs, this.Char, true);
            }

        }

        public void OnSkillCastCancel(Packets.Client.SkillCastCancel p)
        {
            Map.SkillArgs sArgs;
            sArgs = new Map.SkillArgs(p.GetSkillType(), 0, p.GetSkillID(), this.Char.id, 0);
            sArgs.castcancel = true;
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, sArgs, this.Char, true);

        }

        public void OnUseOffensiveSkill(Packets.Client.UseOffensiveSkill p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            // Attack
            try
            {
                if (p.GetSkillType() != 9)
                {
                    Map.SkillArgs sArgs;
                    Actor aActor = this.map.GetActor(p.GetTargetActorID());
                    Actor sActor = (Actor)this.Char;
                    sArgs = new Map.SkillArgs(p.GetSkillType(), 0, p.GetSkillID(), p.GetTargetActorID(), 0);
                    //cast skill
                    if (aActor == null)
                    {
                        sArgs.isCritical = Map.SkillArgs.AttackResult.Miss;
                        sArgs.failed = true;
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, sArgs, this.Char, true);
                        return;
                    }
                    SkillHandler.CastSkill(ref sActor, ref aActor, ref sArgs);
                    if (aActor.type == ActorType.NPC)
                    {
                        Map.ActorSelArgs arg = new Map.ActorSelArgs(aActor.id);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
                    }
                    if (aActor.type == ActorType.PC && aActor.id != this.Char.id)
                    {
                        Map.ActorSelArgs arg = new Map.ActorSelArgs(aActor.id);
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ACTOR_SELECTION, arg, this.Char, true);
                    }


                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, sArgs, this.Char, true);
                }

                // Emoticon
                if (p.GetSkillType() == 9)
                {
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, new Map.SkillArgs(p.GetSkillType(), 0, p.GetSkillID(), p.GetTargetActorID(), (uint)Global.Random.Next()), this.Char, true);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }

        public void OnSkillToggle(Packets.Client.SkillToggle p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            // Attack
            if (p.GetSkillType() != 9)
            {
                Map.SkillArgs sArgs;
                Actor aActor = (Actor)this.Char;
                Actor sActor = (Actor)this.Char;
                sArgs = new Map.SkillArgs(p.GetSkillType(), 0, p.GetSkillID(), sActor.id, 0);
                //cast skill
                SkillHandler.CastSkill(ref sActor, ref aActor, ref sArgs);
                Packets.Server.SkillToggle p1 = new SagaMap.Packets.Server.SkillToggle();
                p1.SetSkillType(p.GetSkillType());
                p1.SetSkillID(p.GetSkillID());
                p1.SetToggle(sArgs.failed);
                this.netIO.SendPacket(p1, this.SessionID);
                sArgs.failed = false;
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, sArgs, this.Char, true);
            }

        }
        //09 0A 
        public void OnItemToggle(SagaMap.Packets.Client.ItemToggle p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            Actor target;
            Map.SkillArgs args;
            Actor sActor = (Actor)this.Char;
            int id;
            byte index = p.GetIndex();
            target = this.map.GetActor(p.GetTargetID());
            args = new Map.SkillArgs(p.GetSkillType(), 0, p.GetSkillID(), p.GetTargetID(), 0);
            SkillHandler.CastSkill(ref sActor, ref target, ref args);
            Packets.Server.ItemActive packet = new SagaMap.Packets.Server.ItemActive();
            packet.SetSkillType(p.GetSkillType());
            packet.SetContainer(p.GetContainer());
            packet.SetIndex(p.GetIndex());
            packet.SetSkillID(p.GetSkillID());
            packet.SetSActor(this.Char.id);
            packet.SetDActor(p.GetTargetID());
            packet.SetValue(args.damage);
            this.netIO.SendPacket(packet, this.SessionID); ;
            this.SendCharStatus(0);
            Item item = this.Char.inv.GetItem((CONTAINER_TYPE)p.GetContainer(), index);
            id = item.id;
            this.map.RemoveItemFromActorPC(this.Char, index, id, 1, ITEM_UPDATE_REASON.PURCHASED);
        }

        //09 0C 
        public void OnSkillAddSpecial(SagaMap.Packets.Client.SkillAddSpecial p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            Actor sActor = (Actor)this.Char;
            Item item;
            byte amount;
            int id;
            byte index = p.GetIndex();
            DeleteItemResult res;
            SkillHandler.SkillAddResault res2;
            item = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, index);
            id = item.id;
            res2 = SkillHandler.SkillAddSpecial(ref this.Char, item.skillID);
            Packets.Server.SkillLearned p3 = new SagaMap.Packets.Server.SkillLearned();
            if (res2 != SkillHandler.SkillAddResault.OK)
            {
                p3.SetResult(res2);
                this.netIO.SendPacket(p3, this.SessionID);
                return;
            }
            else
            {
                p3.SetResult(res2);
            }
            SkillHandler.SendAddSkill(this.Char, item.skillID, 0);
            this.netIO.SendPacket(p3, this.SessionID);
            res = this.Char.inv.DeleteItem(CONTAINER_TYPE.INVENTORY, index, id, 1, out amount);
            switch (res)
            {
                case DeleteItemResult.NOT_ALL_DELETED:
                    Packets.Server.UpdateItem p2 = new SagaMap.Packets.Server.UpdateItem();
                    p2.SetContainer(CONTAINER_TYPE.INVENTORY);
                    p2.SetItemIndex(index);
                    p2.SetAmount(amount);
                    p2.SetUpdateType(SagaMap.Packets.Server.ITEM_UPDATE_TYPE.AMOUNT);
                    p2.SetUpdateReason(ITEM_UPDATE_REASON.PURCHASED);
                    this.netIO.SendPacket(p2, this.SessionID);
                    MapServer.charDB.UpdateItem(this.Char, item);
                    break;
                case DeleteItemResult.ALL_DELETED:
                    Packets.Server.DeleteItem delI = new SagaMap.Packets.Server.DeleteItem();
                    delI.SetContainer(CONTAINER_TYPE.INVENTORY);
                    delI.SetAmount(1);
                    delI.SetIndex(index);
                    this.netIO.SendPacket(delI, this.SessionID);
                    MapServer.charDB.DeleteItem(this.Char, item);
                    break;
            }
        }
        //09 0D
        public void OnSetSpecialSkill(Packets.Client.SetSpecialSkill p)
        {
            if (this.Char.InactiveSkills.ContainsKey(p.GetSkillID()))
            {
                uint id = p.GetSkillID();
                SkillInfo info;
                info = Char.InactiveSkills[id];
                info.slot = p.GetSlot();
                Char.SpecialSkills.Add(id, info);
                Char.InactiveSkills.Remove(id);
                MapServer.charDB.UpdateSkill(this.Char, SkillType.Special, info);
                Packets.Server.SetSpecialSkill p1 = new SagaMap.Packets.Server.SetSpecialSkill();
                p1.SetSkill(id);
                p1.SetSlot(p.GetSlot());
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }
        //09 0E
        public void OnRemoveSpecialSkill(Packets.Client.RemoveSpecialSkill p)
        {
            if (this.Char.SpecialSkills.ContainsKey(p.GetSkillID()))
            {
                uint id = p.GetSkillID();
                SkillInfo info;
                info = Char.SpecialSkills[id];
                info.slot = 0;
                Char.InactiveSkills.Add(id, info);
                Char.SpecialSkills.Remove(id);
                Packets.Server.RemoveSpecialSkill p1 = new SagaMap.Packets.Server.RemoveSpecialSkill();
                p1.SetSkill(id);
                this.netIO.SendPacket(p1, this.SessionID);
            }
        }

        public void OnWantSetSpeciality(Packets.Client.WantSetSpeciality p)
        {
            Packets.Server.SetSpecialityConfirm p2 = new SagaMap.Packets.Server.SetSpecialityConfirm();
            this.netIO.SendPacket(p2, this.SessionID); ;
        }
        #endregion

        public void SendSkillEffect(uint addition, SkillEffects effect, uint amount)
        {
            Packets.Server.SkillEffect p = new SagaMap.Packets.Server.SkillEffect();
            p.SetActorID(this.Char.id);
            p.SetU1(1);
            p.SetU2(addition);
            p.SetFunction((byte)effect);
            p.SetAmount(amount);
            this.netIO.SendPacket(p, this.SessionID);
        }

    }
}
