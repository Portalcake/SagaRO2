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
using SagaMap.Scripting;

namespace SagaMap
{
    public partial class MapClient
    {

        #region "0x05"
        // 0x05 Packets =========================================



        public void OnUsePromiseStone(byte map, float x, float y, float z)
        {
            if (map == this.Char.save_map && this.promisemap != 0 && this.promisemap != this.Char.save_map)
            {
                this.map.SendActorToMap(this.Char, this.promisemap, this.promiseX, this.promiseY, this.promiseZ);
            }
            else
            {
                if (this.Char.save_map == 0)
                {
                    this.Char.save_map = 3;
                    this.Char.save_x = -4811.951f;
                    this.Char.save_y = 15936.05f;
                    this.Char.save_z = 3894f;
                }
                this.map.SendActorToMap(this.Char, this.Char.save_map, this.Char.save_x, this.Char.save_y, this.Char.save_z);
            }
            this.Char.zeny -= ((this.Char.cLevel - 4) * 10);
            this.promisemap = map;
            this.promiseX = x;
            this.promiseY = y;
            this.promiseZ = z;
        }

        public void OnMoveItem(SagaMap.Packets.Client.MoveItem p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            Packets.Client.ITEM_MOVE_TYPE moveType = p.GetMoveType();

            if (moveType == Packets.Client.ITEM_MOVE_TYPE.EquToInv)
            {
                // unequip, equip items are not stackable?
                byte index, amount;
                UnequipItemResult ures = this.Char.inv.UnequipItem(p.GetSourceIndex(), out index, out amount);
                if (ures == UnequipItemResult.NEW_INDEX || ures == UnequipItemResult.STACKED)
                {
                    Packets.Server.MoveItem p1 = new SagaMap.Packets.Server.MoveItem();
                    p1.SetMoveType(SagaMap.Packets.Server.ITEM_MOVE_TYPE.EquToInv);
                    p1.SetSourceIndex(p.GetSourceIndex());
                    p1.SetDestIndex(index);
                    this.netIO.SendPacket(p1, this.SessionID);
                    Item item = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, index);
                    item.equipSlot = -1;
                    MapServer.charDB.UpdateItem(this.Char, item);
                    Bonus.BonusHandler.Instance.EquiqItem(this.Char, item, true);
                    SkillHandler.CalcHPSP(ref this.Char);
                    SendCharStatus(0);
                    SendExtStats();
                    SendBattleStatus();

                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, new Map.ChangeEquipArgs((EQUIP_SLOT)(p.GetSourceIndex()), 0), this.Char, false);
                }
                else SendMessage("Saga", "unequip error");
            }
            else if (moveType == Packets.Client.ITEM_MOVE_TYPE.InvToEqu)
            {
                // equip
                Item item;
                Item itemori = null;
                if (this.Char.inv.EquipList.ContainsKey((EQUIP_SLOT)p.GetDestIndex()))
                {
                    itemori = this.Char.inv.EquipList[(EQUIP_SLOT)p.GetDestIndex()];
                }
                EquipItemResult eres = this.Char.inv.EquipItem(p.GetSourceIndex(), p.GetDestIndex(), out item);

                if (eres == EquipItemResult.NO_ERROR)
                {
                    Packets.Server.MoveItem p1 = new SagaMap.Packets.Server.MoveItem();
                    p1.SetMoveType(SagaMap.Packets.Server.ITEM_MOVE_TYPE.InvToEqu);
                    p1.SetSourceIndex(p.GetSourceIndex());
                    p1.SetDestIndex(item.index);
                    item.equipSlot = item.index;
                    MapServer.charDB.UpdateItem(this.Char, item);
                    this.netIO.SendPacket(p1, this.SessionID);
                    if (itemori != null)
                    {
                        Bonus.BonusHandler.Instance.EquiqItem(this.Char, itemori, true);
                        itemori.equipSlot = -1;
                        MapServer.charDB.UpdateItem(this.Char, itemori);
                    }
                    Bonus.BonusHandler.Instance.EquiqItem(this.Char, item, false);
                    SkillHandler.CalcHPSP(ref this.Char);
                    SendCharStatus(0);
                    SendExtStats();
                    SendBattleStatus();
                    this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, new Map.ChangeEquipArgs((EQUIP_SLOT)(item.index), item.id), this.Char, false);
                }
                else if (eres == EquipItemResult.NOT_EQUIPABLE) SendMessage("Saga", "error, item is not equipable");
                else if (eres == EquipItemResult.WRONG_EQUIPSLOT) SendMessage("Saga", "error, wrong equip slot");
                else SendMessage("Saga", "error, cannot equip item");

            }
            else if (moveType == Packets.Client.ITEM_MOVE_TYPE.InvToSto)
            {
                Item item;
                byte index, amount;
                item = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, p.GetSourceIndex());
                if (item == null) return;
                if (!this.Char.inv.HasFreeSpaceStorage()) return;
                if (this.Char.inv.AddItemStorage(item, out index, out amount) == AddItemResult.ERROR)
                    return;

                this.Char.inv.DeleteItem(CONTAINER_TYPE.INVENTORY, p.GetSourceIndex(), item.id, p.GetAmount(), out amount);
                MapServer.charDB.DeleteItem(this.Char, item);
                MapServer.charDB.NewStorage(this.Char, item);
                Packets.Server.MoveItem p1 = new SagaMap.Packets.Server.MoveItem();
                p1.SetMoveType(SagaMap.Packets.Server.ITEM_MOVE_TYPE.InvToSto);
                p1.SetSourceIndex(p.GetSourceIndex());
                p1.SetDestIndex(item.index);
                this.netIO.SendPacket(p1, this.SessionID);
            }
            else if (moveType == Packets.Client.ITEM_MOVE_TYPE.StoToInv)
            {
                Item item;
                byte amount;
                item = this.Char.inv.GetItem(CONTAINER_TYPE.STORAGE, p.GetSourceIndex());
                if (item == null) return;
                if (!this.Char.inv.HasFreeSpace()) return;
                if (this.Char.inv.AddItem(item) == AddItemResult.ERROR)
                    return;
                this.Char.inv.DeleteItem(CONTAINER_TYPE.STORAGE, p.GetSourceIndex(), item.id, p.GetAmount(), out amount);
                MapServer.charDB.DeleteStorage(this.Char, item);
                MapServer.charDB.NewItem(this.Char, item);
                Packets.Server.MoveItem p1 = new SagaMap.Packets.Server.MoveItem();
                p1.SetMoveType(SagaMap.Packets.Server.ITEM_MOVE_TYPE.StoToInv);
                p1.SetSourceIndex(p.GetSourceIndex());
                p1.SetDestIndex(item.index);
                this.netIO.SendPacket(p1, this.SessionID); ;
            }

            //Send Answer
            // Do item movement stuff in inventory db?
            Packets.Server.MoveReply sendPacket = new SagaMap.Packets.Server.MoveReply();
            // Todo : Switch result of move and obtain relevent string to send from GameStringTable
            sendPacket.SetMessage(0);
            this.netIO.SendPacket(sendPacket, this.SessionID);
        }

        public void OnSortInvList(SagaMap.Packets.Client.SortInvList p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            this.Char.currentInvTab = p.GetSortType();

        }

        public void OnRepaireEquip(Packets.Client.RepaireEquip p)
        {
            if (Repaireread == 0 || Repaireread != p.GetAmount())
            {
                Repaireread = p.GetAmount();
                RepaireList = new List<SagaMap.Packets.Client.RepaireInfo>();
            }

            if (RepaireList == null) RepaireList = new List<SagaMap.Packets.Client.RepaireInfo>();
            byte amounttoread = (byte)(Repaireread - RepaireList.Count);
            if (amounttoread > 10) amounttoread = 10;
            SagaMap.Packets.Client.RepaireInfo[] tmp = p.GetEquipts(amounttoread);
            for (int i = 0; i < amounttoread; i++) RepaireList.Add(tmp[i]);
            if (RepaireList.Count == Repaireread)
            {
                for (int i = 0; i < Repaireread; i++) Npc.RepaireEquip(this.Char, RepaireList[i].Container, RepaireList[i].Slot);
                RepaireList = null;
                Repaireread = 0;
            }
        }

        public void OnWeaponUpgrade(Packets.Client.WeaponUpgrade p)
        {
            Weapon weapon = WeaponFactory.GetActiveWeapon(this.Char);
            int price = 0;
            if (weapon.exp >= ExperienceManager.Instance.GetExpForLevel(weapon.level, ExperienceManager.LevelType.WLEVEL))
            {
                for (int i = 1; i <= weapon.level; i++)
                {
                    int value = (i / 5) + 1;
                    int multpler = value * 25;
                    int rest = i % 5;
                    if (rest != 0)
                        price += multpler;
                    else
                    {
                        if ((i % 10) != 0)
                        {
                            int amount = 150 + ((value - 2) / 2) * 300;
                            price += amount;
                        }
                        else
                        {
                            int amount = 400;
                            value = (i / 10);
                            for (int j = 1; j < value; j++)
                            {
                                amount = amount + 300 + 600 * j;
                            }
                            price += amount;
                        }
                    }
                }
                weapon.level += 1;
                this.Char.zeny -= (uint)price;
                Packets.Server.WeaponAdjust p1 = new SagaMap.Packets.Server.WeaponAdjust();
                p1.SetFunction(SagaMap.Packets.Server.WeaponAdjust.Function.Level);
                p1.SetValue(weapon.level);
                this.netIO.SendPacket(p1, this.SessionID);
                this.SendZeny();
                this.SendBattleStatus();
                Packet p2 = new Packet();
                p2.data = new byte[5];
                p2.ID = 0x0517;//don't know its function,maybe to close the window?
                this.netIO.SendPacket(p2, this.SessionID);
            }
        }

        public void OnMoveWeapon(SagaMap.Packets.Client.SendWeaponMove p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            Logger.ShowInfo("Weapon movement", null);
            Packets.Server.WeaponMove p1 = new SagaMap.Packets.Server.WeaponMove();
            p1.SetDirection(p.GetDirection());
            p1.SetSlot(p.GetWeaponSlot());
            p1.SetPosition(p.GetPosition());
            p1.SetStatus(0);
            this.netIO.SendPacket(p1, this.SessionID);

        }

        public void OnWeaponSwitch(SagaMap.Packets.Client.SendWeaponSwitch p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            Packets.Server.WeaponSwitch p1 = new SagaMap.Packets.Server.WeaponSwitch();
            p1.SetID(p.GetID());
            this.netIO.SendPacket(p1, this.SessionID);
        }

        public void OnUseMap(Packets.Client.UseMap p)
        {
            byte index;
            Item item;
            index = p.GetIndex();
            item = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, index);
            byte map, value;
            map = (byte)(item.skillID / 10);
            value = (byte)(item.skillID % 10);
            this.Pc.OnUseMap(map, value);
            byte newAmount;
            DeleteItemResult res = this.Char.inv.DeleteItem(CONTAINER_TYPE.INVENTORY, index, item.id, 1, out newAmount);
            MapServer.charDB.DeleteItem(this.Char, item);
            Packets.Server.DeleteItem delI = new SagaMap.Packets.Server.DeleteItem();
            delI.SetContainer(CONTAINER_TYPE.INVENTORY);
            delI.SetAmount(1);
            delI.SetIndex(index);
            this.netIO.SendPacket(delI, this.SessionID);
            Packets.Server.ShowMap p2 = new SagaMap.Packets.Server.ShowMap();
            p2.SetMap(map);
            p2.SetZone(value);
            this.netIO.SendPacket(p2, this.SessionID); ;
        }

        public void OnDeleteItem(SagaMap.Packets.Client.DeleteItem p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            this.map.RemoveItemFromActorPC(this.Char, p.GetItemIndex(), p.GetItemID(), p.GetAmount(), ITEM_UPDATE_REASON.DISCARD);
            /*
            DeleteItemResult res = this.Char.inv.DeleteItem( p.GetContainter(), p.GetItemIndex(), p.GetItemID(), p.GetAmount(), out newAmount );
            if( res == DeleteItemResult.ALL_DELETED || res == DeleteItemResult.NOT_ALL_DELETED )
            {
                Packets.Server.DeleteItem delI = new SagaMap.Packets.Server.DeleteItem();
                delI.SetContainer( p.GetContainter() );
                delI.SetAmount( p.GetAmount() );
                delI.SetIndex( p.GetItemIndex() );
                this.netIO.SendPacket(delI, this.SessionID);
            }
            else if( res == DeleteItemResult.WRONG_ITEMID ) SendMessage( "Saga", "del error: wrong item id" );
            else SendMessage( "Saga", "del error" );*/
        }

        public void OnNPCDropList(SagaMap.Packets.Client.NPCDropList p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            ActorNPC Mob = (ActorNPC)this.map.GetActor(p.GetActorID());
            if (Mob.stance != Global.STANCE.DIE && Mob.HP != 0) return;
            if (Mob == null)
            {
                Packets.Server.NPCDropListResult sendPacket = new SagaMap.Packets.Server.NPCDropListResult();
                sendPacket.SetResult(SagaMap.Packets.Server.NPCDropListResult.Result.NO_RIGHT);
                this.netIO.SendPacket(sendPacket, this.SessionID);
                return;
            }
            Mob mob = (Mob)Mob.e;
            if (mob.timeSignature.actorID != this.Char.id)
            {
                Packets.Server.NPCDropListResult sendPacket = new SagaMap.Packets.Server.NPCDropListResult();
                sendPacket.SetResult(SagaMap.Packets.Server.NPCDropListResult.Result.NO_RIGHT);
                this.netIO.SendPacket(sendPacket, this.SessionID);
                return;
            }
            if (!this.Char.inv.HasFreeSpace())
            {
                Packets.Server.NPCDropListResult sendPacket = new SagaMap.Packets.Server.NPCDropListResult();
                sendPacket.SetResult(SagaMap.Packets.Server.NPCDropListResult.Result.INVENTORY_FULL);
                this.netIO.SendPacket(sendPacket, this.SessionID); ;
                return;
            }
            if (Mob.NPCinv == null) Mob.NPCinv = new List<Item>();
            this.Char.CurNPCinv = Mob.NPCinv;
            Packets.Server.SendNpcInventory sendPacket1 = new Packets.Server.SendNpcInventory();
            sendPacket1.SetActorID(this.Char.id);
            sendPacket1.SetItems(Mob.NPCinv);
            this.netIO.SendPacket(sendPacket1, this.SessionID); ;
            //this.map.DeleteActor(Mob);
        }
        public void OnNPCShopSell(SagaMap.Packets.Client.NPCShopSell p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            try
            {
                if (this.Char.CurTarget == null)
                    return;
                ActorNPC npc = (ActorNPC)this.Char.CurTarget;
                Item item = this.Char.inv.GetItem((CONTAINER_TYPE)p.GetContainer(), p.GetIndex()); // TODO:  Theres gotta be a better way to do this.
                byte ammount = p.GetAmount();
                uint price = item.price * ammount;

                this.map.RemoveItemFromActorPC(this.Char, p.GetIndex(), item.id, ammount, ITEM_UPDATE_REASON.SOLD);

                this.Char.zeny += (price / 4);
                this.SendZeny();
            }
            catch (Exception)
            {
                SendMessage("Saga", "Error on selling this item!", SagaMap.Packets.Server.SendChat.MESSAGE_TYPE.SYSTEM_MESSAGE);
                //Logger.ShowWarning( ex, null );
            }
        }

        public void OnNPCShopBuy(SagaMap.Packets.Client.NPCShopBuy p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;
            ActorNPC npc = (ActorNPC)this.Char.CurTarget;
            Item item = new Item(npc.NPCinv[p.GetIndex()].id);
            Npc n = (Npc)npc.e;
            item.creatorName = n.Name;
            byte amount = p.GetAmount();
            if (!this.Char.inv.HasFreeSpace())
            {
                this.SendMessage(n.Name, "You do not have enough space in your inventory!");
                return;
            }
            uint price = item.price * amount;
            if (this.Char.zeny < price) return;
            this.Char.zeny -= price;
            this.SendZeny();
            item.stack = amount;
            this.map.AddItemToActor(this.Char, item, ITEM_UPDATE_REASON.SOLD);

        }

        public void OnWeaponAuge(SagaMap.Packets.Client.WeaponAuge p)
        {
            if (this.state != SESSION_STATE.MAP_LOADED) return;

            Actor sActor = (Actor)this.Char;
            int id;
            byte index = p.GetSlot();
            byte slot = p.GetWeaponSlot();
            byte amount;
            Item item = this.Char.inv.GetItem(CONTAINER_TYPE.INVENTORY, index);
            DeleteItemResult res;
            Weapon weapon = WeaponFactory.GetActiveWeapon(this.Char);
            if (slot == 0)
            {
                weapon.augeSkillID = item.skillID;
            }
            weapon.stones[slot] = item.skillID;
            Packets.Server.WeaponStone p3 = new SagaMap.Packets.Server.WeaponStone();
            p3.SetUnknown(1);
            p3.SetWeaponSlot(slot);
            p3.SetValue(item.skillID);
            this.netIO.SendPacket(p3, this.SessionID);
            id = item.id;
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

        public void OnUseDye(SagaMap.Packets.Client.GetUseDye p)
        {
            Item newitem = null;
            byte dyeslot = p.GetDyeSlot();
            Item useddye = this.Char.Inventory[dyeslot];

            if (p.GetContainer() == CONTAINER_TYPE.EQUIP)
            {
                newitem = this.Char.inv.EquipList[p.GetSlot()];
            }
            else if (p.GetContainer() == CONTAINER_TYPE.INVENTORY)
            {
                newitem = this.Char.Inventory[(byte)p.GetSlot()];
            }
            Packets.Server.UseDyeingItem packet = new SagaMap.Packets.Server.UseDyeingItem();
            if (newitem != null)
            {
                packet.SetError((byte)Global.GENERAL_ERRORS.NO_ERROR);
                //Change the colour of the equip
                //Item dyes 4101 - 4106
                newitem.Dye = (byte)(useddye.id - 4101);
                Console.WriteLine("New colour: " + newitem.Dye + " for item: " + newitem.id + " used dye: " + useddye.id);
            }
            else
            {
                packet.SetError((byte)Global.GENERAL_ERRORS.INV_ITEM_NOT_FOUND);
            }
            packet.SetItemID(useddye.id);
            packet.SetContainer(p.GetContainer());
            packet.SetEquipment(p.GetSlot());
            this.netIO.SendPacket(packet, this.SessionID);
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, new Map.ChangeEquipArgs((EQUIP_SLOT)newitem.index, newitem.id), this.Char, true);
            this.map.RemoveItemFromActorPC(this.Char, useddye.id, 1, ITEM_UPDATE_REASON.OTHER);
            //As a test, try sending 0x0302 with the new info
            //TODO: proper packet updates
            this.Char.e.OnActorAppears(this.Char);
        }
        #endregion

    }
}
