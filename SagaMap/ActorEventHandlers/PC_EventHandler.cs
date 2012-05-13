using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

using SagaMap.Manager;
using SagaMap.Skills;

namespace SagaMap.ActorEventHandlers
{
    public class PC_EventHandler : ActorEventHandler
    {

        private ActorPC I;
        public MapClient C;

        public PC_EventHandler(ActorPC actor, MapClient client)
        {
            this.I = actor;
            this.C = client;
        }

        public void OnCreate(bool success)
        {
            if (success)
            {
                //cb2
                /*Packets.Server.SendIdent p1 = new SagaMap.Packets.Server.SendIdent();
                p1.SetActorID(I.id);
                C.netIO.SendPacket(p1, C.SessionID);*/
                this.C.OnSendDemandMapID(new SagaMap.Packets.Client.SendDemandMapID());
            }
            else C.Disconnect();
        }

        public void OnMapLoaded()
        {
            I.state = 0;
            I.stance = Global.STANCE.STAND;
            I.trading = Trading.NOT_TRADING;

            Packets.Server.ActorPlayerInfo sendPacket = new SagaMap.Packets.Server.ActorPlayerInfo();
            sendPacket.SetActorID(I.id);
            sendPacket.SetName(I.name);
            sendPacket.SetLocation(I.x, I.y, I.z);
            sendPacket.SetYaw(I.yaw);
            sendPacket.SetRace(I.race);
            sendPacket.SetFace(I.face);
            sendPacket.SetDetails(I.details);
            sendPacket.SetSlots(I.slots);
            C.netIO.SendPacket(sendPacket, C.SessionID);

            Packets.Server.CharStatus sendPacket2 = new Packets.Server.CharStatus();
            sendPacket2.SetJob(I.job);
            sendPacket2.SetExp(I.cExp, I.jExp);
            sendPacket2.SetHPSP(I.HP, I.maxHP, I.SP, I.maxSP);
            sendPacket2.SetLCLP(I.LC, I.maxLC, I.LP, I.maxLP);
            C.netIO.SendPacket(sendPacket2, C.SessionID);

            

        }

        public void OnReSpawn()
        {
            I.HP = I.maxHP;
            I.state = 0;
            I.stance = Global.STANCE.REBORN;
            I.trading = Trading.NOT_TRADING;
        }

        public void OnDie()
        {
            I.state = 0;
            I.stance = Global.STANCE.DIE;
            foreach (MultiRunTask i in I.Tasks.Values)
            {
                try
                {
                    i.Deactivate();
                }
                catch (Exception)
                {
                }
            }
            Addition[] additionlist = new Addition[I.BattleStatus.Additions.Count];
            I.BattleStatus.Additions.Values.CopyTo(additionlist, 0);
            foreach (Addition i in additionlist)
            {
                if (i.Activated)
                    i.AdditionEnd();
            }
            I.BattleStatus.Additions.Clear();
            if (I.BattleStatus.Status != null) I.BattleStatus.Status.Clear();
            I.Tasks.Clear();            
        }

        public void OnKick()
        {
            C.Disconnect();
        }

        public void OnDelete()
        {
            C.state = MapClient.SESSION_STATE.IDENTIFIED;
        }

        public void OnActorAppears(Actor actor)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC aActor = (ActorPC)actor;
                Weapon activeweapon=null;
                //send actorPCInfo
                Packets.Server.ActorPCInfo p1 = new SagaMap.Packets.Server.ActorPCInfo();
                p1.SetActorID(aActor.id);
                p1.SetGender(aActor.sex);
                p1.SetLocation(aActor.x, aActor.y, aActor.z);
                p1.SetYaw(aActor.yaw);
                p1.SetName(aActor.name);
                p1.SetRace(aActor.race);
                p1.SetFace(aActor.face);
                p1.SetDetails(aActor.details);
                foreach (Weapon i in aActor.Weapons)
                {
                    if (i.active == 1) activeweapon = i;
                }
                p1.SetEquip(aActor.inv.GetEquipIDs(),aActor.inv.GetEquipDyes(),activeweapon);
                p1.SetJob(aActor.job);
                C.netIO.SendPacket(p1, C.SessionID);
            }
            else if (actor.type == ActorType.NPC)
            {
                ActorNPC aActor = (ActorNPC)actor;
                if (aActor.npcType >= 10000) aActor.aStats = new int[0]; 
                else
                {
                    try
                    {
                        Npc npc = (Npc)aActor.e;
                        aActor.aStats[0] = Quest.QuestsManager.GetNPCIcon(this.I, npc);
                    }
                    catch (Exception) { }
                }
                Packets.Server.ActorNPCInfo p1 = new SagaMap.Packets.Server.ActorNPCInfo((byte)aActor.aStats.Length);
                p1.SetActorID(aActor.id);
                p1.SetNPCID(aActor.npcType);
                p1.SetLocation(aActor.x, aActor.y, aActor.z);
                p1.SetYaw(aActor.yaw);
                p1.SetAdditionalStatus(aActor.aStats);
                C.netIO.SendPacket(p1, C.SessionID);
                if (aActor.npcType >= 50000)
                {
                    Ship ship = (Ship)aActor.e;
                    ship.Map.MoveActor(Map.MOVE_TYPE.START, ship.Actor, null, 0, null, 0, ship.Speed);
                }
            }
            else if (actor.type == ActorType.Item)
            {
                ActorItem aActor = (ActorItem)actor;
                Packets.Server.ActorItemInfo p1 = new SagaMap.Packets.Server.ActorItemInfo();
                p1.SetActorID(aActor.id);
                p1.SetNPCID(aActor.itemtype);
                p1.SetLocation(aActor.x, aActor.y, aActor.z);
                p1.SetYaw(aActor.yaw);
                p1.SetActive(1);
                p1.SetU1(1);
                p1.SetU2(1);
                C.netIO.SendPacket(p1, C.SessionID);
            }
            SkillHandler.SendAllStatusIcons(this.I, actor);
        }

        public void OnActorChangesState(Actor aActor, MapEventArgs args)
        {
            //send actorChangeState
            Packets.Server.ActorChangeState p1 = new SagaMap.Packets.Server.ActorChangeState();
            p1.SetActorID(aActor.id);
            p1.SetBattleState(aActor.state > 0);
            p1.SetStance(aActor.stance);
            if (args != null)
            {
                Map.SkillArgs arg = (Map.SkillArgs)args;
                p1.SetTargetActor(arg.targetActorID);
            }
            C.netIO.SendPacket(p1, C.SessionID);
        }

        public void OnActorStartsMoving(Actor mActor, float[] pos, float[] accel, int yaw, ushort speed, uint delayTime)
        {
            //send actorMoveStart
             Packets.Server.ActorMoveStart p1 = new SagaMap.Packets.Server.ActorMoveStart();
            p1.SetActorID(mActor.id);
            p1.SetLocation(pos[0], pos[1], pos[2]);
            p1.SetAcceleration(accel[0], accel[1], accel[2]);
            p1.SetYaw(yaw);
            p1.SetSpeed(speed);
            p1.SetDelay1(delayTime);
            C.netIO.SendPacket(p1, C.SessionID);
        }

        public void OnActorStartsMoving(ActorNPC mActor, byte count, float[] waypoints,ushort speed)
        {
            if (mActor.npcType < 50000)
            {
                Packets.Server.ActorNPCMoveStart p1 = new SagaMap.Packets.Server.ActorNPCMoveStart(count);
                p1.SetActorID(mActor.id);
                p1.SetSpeed(speed);
                p1.SetWaypoints(waypoints);
                C.netIO.SendPacket(p1, C.SessionID);
            }
            else
            {
                Ship ship = (Ship)mActor.e;                
                Packets.Server.WideMoveStart p1 = new SagaMap.Packets.Server.WideMoveStart((byte)ship.CurrentWaypoints.Count);
                p1.SetActorID(mActor.id);
                p1.SetSpeed(speed);
                p1.SetWaypoints(ship.CurrentWaypoints, ship.CurrentYaws);
                C.netIO.SendPacket(p1, C.SessionID);
            }
        }
        
        public void OnActorStopsMoving(Actor mActor, float[] pos, int yaw, ushort speed, uint delayTime)
        {
            //send actorMoveStop
            Packets.Server.ActorMoveStop p1 = new SagaMap.Packets.Server.ActorMoveStop();
            p1.SetActorID(mActor.id);
            p1.SetLocation(pos[0], pos[1], pos[2]);
            p1.SetYaw(yaw);
            p1.SetSpeed(speed);
            p1.SetDelayTime(delayTime);
            C.netIO.SendPacket(p1, C.SessionID);
        }

        public void OnActorChat(Actor cActor, MapEventArgs args)
        {
            Map.ChatArgs cArgs = (Map.ChatArgs)args;
            this.C.SendMessage(cActor.name, cArgs.text, cArgs.mType);
        }

        public void OnSendWhisper(string name, string message, byte flag)
        {
            Packets.Server.SendWhisper sendPacket = new Packets.Server.SendWhisper(message.Length);
            sendPacket.SetName(name);
            sendPacket.SetUnknown(flag);
            sendPacket.SetMessage(message);
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnActorDisappears(Actor dActor)
        {
            //send actorDelete
            Packets.Server.ActorDelete p1 = new SagaMap.Packets.Server.ActorDelete();
            p1.SetActorID(dActor.id);
            C.netIO.SendPacket(p1, C.SessionID);
        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            Map.SkillArgs sArgs = (Map.SkillArgs)args;
            Actor aActor =C.map.GetActor(sArgs.targetActorID);
            if (sArgs.castcancel)
            {
                Packets.Server.SkillCastCancel p3 = new SagaMap.Packets.Server.SkillCastCancel();
                p3.SetSkillType(sArgs.skillType);
                p3.SetSkillID((uint)sArgs.skillID);
                p3.SetActors(sActor.id);
                C.netIO.SendPacket(p3, C.SessionID);
                return;
            }
            switch (sArgs.casting)
            {
                case false :
                    if (sArgs.failed == false)
                    {
                        Packets.Server.OffensiveSkill p1 = new SagaMap.Packets.Server.OffensiveSkill();
                        p1.SetSkillType(sArgs.skillType);
                        p1.SetIsCritical((byte)sArgs.isCritical);
                        p1.SetSkillID((uint)sArgs.skillID);
                        p1.SetActors(sActor.id, sArgs.targetActorID);
                        p1.SetDamage(sArgs.damage);
                        C.netIO.SendPacket(p1, C.SessionID);
                    }
                    else
                    {
                        Packets.Server.OffensiveSkillFailed p1 = new SagaMap.Packets.Server.OffensiveSkillFailed();
                        p1.SetSkillID((uint)sArgs.skillID);
                        p1.SetActor(sActor.id);
                        C.netIO.SendPacket(p1, C.SessionID);
                    }
                    break;
                case true:
                    Packets.Server.SkillCast p2 = new SagaMap.Packets.Server.SkillCast();
                    p2.SetSkillType(sArgs.skillType);
                    p2.SetSkillID((uint)sArgs.skillID);
                    p2.SetActors(sActor.id, sArgs.targetActorID);
                    p2.SetU1(0x10);
                    C.netIO.SendPacket(p2, C.SessionID);
                    break;
            }

           }

        public void OnActorChangeEquip(Actor sActor, MapEventArgs args)
        {
            Map.ChangeEquipArgs cArgs = (Map.ChangeEquipArgs)args;

            Packets.Server.ActorSetEquip p1 = new SagaMap.Packets.Server.ActorSetEquip();
            p1.SetActorID(sActor.id);
            p1.SetEquipSlot(cArgs.eSlot);
            p1.SetEquipItemID(cArgs.itemID);
            C.netIO.SendPacket(p1, C.SessionID);
        }

        public void OnAddItem( Item nItem, SagaDB.Items.ITEM_UPDATE_REASON reason ) 
        {
            byte index, amount;
            AddItemResult res = I.inv.AddItem(nItem, out index, out amount);
            if (res == AddItemResult.ERROR)
            {
                Logger.ShowWarning("pc event handler: cannot add item with ID " + nItem.id,null);
                return;
            }

            nItem.index = index;
            nItem.stack = amount;

            if (res == AddItemResult.NEW_INDEX)
            {
                Packets.Server.AddItem p1 = new SagaMap.Packets.Server.AddItem();
                p1.SetContainer(CONTAINER_TYPE.INVENTORY);
                p1.SetItem(nItem);
                C.netIO.SendPacket(p1, C.SessionID);
                MapServer.charDB.NewItem(this.C.Char, nItem);
            }

            Packets.Server.UpdateItem p2 = new SagaMap.Packets.Server.UpdateItem();
            p2.SetContainer(CONTAINER_TYPE.INVENTORY);
            p2.SetItemIndex(nItem.index);
            p2.SetAmount(nItem.stack);
            p2.SetUpdateType(SagaMap.Packets.Server.ITEM_UPDATE_TYPE.AMOUNT);
            p2.SetUpdateReason(reason);
            C.netIO.SendPacket(p2, C.SessionID);
            MapServer.charDB.UpdateItem(this.C.Char, nItem);
        }

        // Trading

        public void OnTradeStart(ActorPC source)
        {
            C.TradeItems = new Dictionary<Item, byte>();
            Packets.Server.TradeOther sendPacket = new SagaMap.Packets.Server.TradeOther();
            sendPacket.SetActorID(source.id);
            C.netIO.SendPacket(sendPacket, C.SessionID);          
        }

        public void OnTradeStatus(uint targetid, TradeResults status)
        {
            Packets.Server.Trade sendPacket = new SagaMap.Packets.Server.Trade();
            sendPacket.SetActorID(targetid);
            sendPacket.SetStatus(status);

            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnTradeConfirm()
        {
            Packets.Server.TradeConfirm sendPacket = new SagaMap.Packets.Server.TradeConfirm();
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnTradeResult(TradeResults result)
        {
            Packets.Server.TradeResult sendPacket = new SagaMap.Packets.Server.TradeResult();
            sendPacket.SetStatus(result);
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnTradeItem(byte Tradeslot, Item TradeItem)
        {
            Packets.Server.TradeItemOther sendPacket = new SagaMap.Packets.Server.TradeItemOther();
            sendPacket.SetSlot(Tradeslot);
            sendPacket.SetItem(TradeItem);
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnTradeZeny(int monies)
        {
            Packets.Server.TradeZenyOther sendPacket = new SagaMap.Packets.Server.TradeZenyOther();
            sendPacket.SetMoney(monies);
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnResetTradeItems()
        {
            C.TradeItems.Clear();
            C.TradeMoney = 0;
        }

        public void PerformTrade()
        {
            ActorPC target = (ActorPC)C.map.GetActor(I.TradeTarget);
            if (!Config.Instance.AllowGMTrade && (C.Char.GMLevel > 2 || target.GMLevel > 2))
            {
                C.SendMessage("Saga", "GMs are not allowed to trade");
                return;
            }
            else
            {

                //Swap the items and money            
                if (target == null) return;
                if (C.TradeMoney > 0)
                {
                    if (I.zeny >= C.TradeMoney)
                    {
                        target.zeny += (uint)C.TradeMoney;
                        I.zeny -= (uint)C.TradeMoney;
                        if (C.Char.GMLevel > 2 || target.GMLevel > 2)
                        {
                            Logger.gmlogger.WriteLog(C.Char.name + "->" + target.name + " | " + C.TradeMoney + "(zeny)");
                        }

                    }
                    else
                    {
                        C.SendTradeResult(TradeResults.NOT_ENOUGH_MONEY);
                        return;
                    }
                }

                foreach (Item SwapItem in C.TradeItems.Keys)
                {
                    SwapItem.stack = C.TradeItems[SwapItem];
                    C.map.AddItemToActor(target, SwapItem, ITEM_UPDATE_REASON.OTHER);
                    C.map.RemoveItemFromActorPC(I, SwapItem.id, SwapItem.stack, ITEM_UPDATE_REASON.OTHER);
                    if (C.Char.GMLevel > 2 || target.GMLevel > 2)
                    {
                        Logger.gmlogger.WriteLog(C.Char.name + "->" + target.name + " | " + SwapItem.id);
                    }
                }

                if (target.TradeStatus == TradeStatus.TRADE_CONFIRM)
                    target.e.PerformTrade();

                C.SendTradeResult(TradeResults.SUCCESS);
                C.ResetTradeItems(1);
                C.ResetTradeStatus(1);
            }
        }

        public void OnSelectButton(ActorPC sActor, int button)
        {

        }

        public void OnSendShopList(List<Item> items, uint money, uint ActorID)
        {
            Packets.Server.NpcShopList sendPacket = new Packets.Server.NpcShopList();
            sendPacket.SetMoney(money);
            sendPacket.SetActorID(ActorID);
            sendPacket.SetItems(items);
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnTimeWeatherChange(byte[] gameTime, Global.WEATHER_TYPE weather)
        {
            Packets.Server.SendTime sendPacket = new SagaMap.Packets.Server.SendTime();
            sendPacket.SetTime(gameTime[0],gameTime[1],gameTime[2]);
            sendPacket.SetWeather(weather);
            this.C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnTeleport(float x, float y, float z)
        {
            Packets.Server.ActorTeleport sendPacket = new SagaMap.Packets.Server.ActorTeleport();
            sendPacket.SetLocation(x, y, z);
            this.C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnPartyInvite(ActorPC sActor)
        {
             Packets.Server.SendPartyInvite sendPacket = new SagaMap.Packets.Server.SendPartyInvite();
             sendPacket.SetName(sActor.name);
             this.I.PartyTarget = sActor.id;
             this.C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnPartyAccept(ActorPC sActor) 
        {
            //unused
        }

        public void OnSendMessage(string from, string message)
        {
            Packets.Server.SendChat sendPacket = new Packets.Server.SendChat(message.Length);
            sendPacket.SetName(from);
            sendPacket.SetMessageType(0);
            sendPacket.SetMessage(message);
            C.netIO.SendPacket(sendPacket, C.SessionID);
        }

        public void OnChangeStatus(Actor sActor, MapEventArgs args)
        {
            Map.StatusArgs arg = (Map.StatusArgs)args;
            switch (arg.type)
            {
                case Map.StatusArgs.EventType.Add:
                    foreach (Map.StatusArgs.StatusInfo i in arg.StatusList)
                    {
                        Packets.Server.ExchangeAddition p1 = new SagaMap.Packets.Server.ExchangeAddition();
                        p1.SetID(sActor.id);
                        Skill info = SkillFactory.GetSkill(i.SkillID);
                        if (info != null)
                            p1.SetStatusID(info.addition);
                        else
                            p1.SetStatusID(i.SkillID);
                        p1.SetTime(i.time);
                        this.C.netIO.SendPacket(p1, C.SessionID);
                    }
                    break;
                case Map.StatusArgs.EventType.Remove :
                    foreach (Map.StatusArgs.StatusInfo i in arg.StatusList)
                    {
                        Packets.Server.DeleteExchangeAddition p1 = new SagaMap.Packets.Server.DeleteExchangeAddition();
                        p1.SetID(sActor.id);
                        Skill info = SkillFactory.GetSkill(i.SkillID);
                        if (info != null)
                            p1.SetStatusID(info.addition);
                        else
                            p1.SetStatusID(i.SkillID);
                        this.C.netIO.SendPacket(p1, C.SessionID);
                    }
                    break;
            }
        }

       public void OnActorSelection(ActorPC sActor, MapEventArgs args)
       {
           Map.ActorSelArgs arg = (Map.ActorSelArgs)args;           
           Packets.Server.ActorSelection sendSelectionPacket = new SagaMap.Packets.Server.ActorSelection();
           Map map;
           MapManager.Instance.GetMap(sActor.Map, out map);
           Actor dActor = map.GetActor(arg.target);
           sendSelectionPacket.SetSourceActorID(sActor.id);
           if (dActor != null)
           {
               if (dActor.type == ActorType.NPC)
               {
                   ActorNPC npc = (ActorNPC)dActor;
                   sendSelectionPacket.SetHP(npc.HP);
                   sendSelectionPacket.SetMaxHP(npc.maxHP);
                   sendSelectionPacket.SetSP(npc.SP);
                   sendSelectionPacket.SetMaxSP(npc.maxSP);
               }
               if (dActor.type == ActorType.PC)
               {
                   ActorPC pc = (ActorPC)dActor;
                   sendSelectionPacket.SetHP(pc.HP);
                   sendSelectionPacket.SetMaxHP(pc.maxHP);
                   sendSelectionPacket.SetSP(pc.SP);
                   sendSelectionPacket.SetMaxSP(pc.maxSP);
               }
               sendSelectionPacket.SetTargetActorID(dActor.id);
           }
           this.C.netIO.SendPacket(sendSelectionPacket, this.C.SessionID);
       }
    }
}
