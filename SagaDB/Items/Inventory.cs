using System;
using System.Collections.Generic;
using System.Text;

namespace SagaDB.Items
{
    [Serializable]
    public enum EQUIP_SLOT : ushort { TOP_HEAD, MIDDLE_HEAD, BOTTOM_HEAD, BODY, PANTS, HANDS, LEGS, BELT, BACK, RIGHT_RING, LEFT_RING, NECKLET, EARRING, AMMO, LEFT_HAND, RIGHT_HAND, U1, U2, NO_EQUIP };
    [Serializable]
    public enum CONTAINER_TYPE : ushort { EQUIP = 1, INVENTORY = 2, STORAGE = 3 };

    [Serializable]
    public enum AddItemResult { ERROR, NEW_INDEX, STACKED };
    [Serializable]
    public enum UnequipItemResult { ERROR, NEW_INDEX, STACKED };
    [Serializable]
    public enum EquipItemResult { ERROR, NOT_EQUIPABLE, WRONG_EQUIPSLOT, NO_ERROR };
    [Serializable]
    public enum DeleteItemResult { ERROR, WRONG_ITEMID, ALL_DELETED, NOT_ALL_DELETED };

    [Serializable]
    public enum TradeResults { SUCCESS, NO_TARGET, TARGET_CANCELLED, NOT_ENOUGH_ITEM, NOT_TRADEABLE, NOT_ENOUGH_MONEY, NO_SPACE, TARGET_NO_SPACE, TARGET_TRADE_ACTIVE, ERROR } 

    //todo:
    // make equipType work
    [Serializable]
    public class Inventory
    {
        private Dictionary<EQUIP_SLOT, Item> equip;
        public Dictionary<byte, Item> inv;
        public Dictionary<byte, Item> storage;
        public uint invMaxSlots;

        // for db4o
        public Inventory()
        {
        }


        public Inventory(uint invMaxSlots)
        {
            if (invMaxSlots > 254) invMaxSlots = 254;
            this.invMaxSlots = invMaxSlots;
            this.equip = new Dictionary<EQUIP_SLOT, Item>();
            this.inv = new Dictionary<byte,Item>();
            this.storage = new Dictionary<byte, Item>();
        }

        public bool HasFreeSpace()
        {
            if (this.inv.Count >= this.invMaxSlots)
                return false;
            else
                return true;
        }

        public bool HasFreeSpaceStorage()
        {
            if (this.storage.Count >= this.invMaxSlots)
                return false;
            else
                return true;
        }

        private bool GetFreeInventoryIndex(out byte index)
        {
            index = 0;
            if (this.inv.Count >= this.invMaxSlots) return false;

            for (index = 0; index < this.invMaxSlots; index++)
                if (!this.inv.ContainsKey(index)) break;

            return true;
        }

        private bool GetFreeStorageIndex(out byte index)
        {
            index = 0;
            if (this.storage.Count >= this.invMaxSlots) return false;

            for (index = 0; index < this.invMaxSlots; index++)
                if (!this.storage.ContainsKey(index)) break;

            return true;
        }

        public AddItemResult AddItem(Item item)
        {
            if (item == null) return AddItemResult.ERROR;

            //stack if possible
            //Notice: stackable items should never be allowed to be stackable,
            //        as their unique settings will get lost in this loop
            foreach (byte keyIndex in this.inv.Keys)
            {
                if (this.inv[keyIndex].id == item.id)
                    if (this.inv[keyIndex].stack < this.inv[keyIndex].maxStack)
                    {
                        if (this.inv[keyIndex].stack + item.stack <= this.inv[keyIndex].maxStack)
                        {
                            this.inv[keyIndex].stack += item.stack;
                            item = this.inv[keyIndex];
                            return AddItemResult.STACKED;
                        }
                        else
                        {
                            int diff = (this.inv[keyIndex].stack + item.stack) - this.inv[keyIndex].maxStack;
                            this.inv[keyIndex].stack = this.inv[keyIndex].maxStack;
                            item.stack = (byte)diff;
                        }
                    }
            }

            byte index;
            if (!GetFreeInventoryIndex(out index)) return AddItemResult.ERROR;
            item.index = index;

            this.inv.Add(index, item);

            return AddItemResult.NEW_INDEX;
        }

        public AddItemResult AddItemStorage(Item item)
        {
            if (item == null) return AddItemResult.ERROR;

            //stack if possible
            //Notice: stackable items should never be allowed to be stackable,
            //        as their unique settings will get lost in this loop
            foreach (byte keyIndex in this.storage.Keys)
            {
                if (this.storage[keyIndex].id == item.id)
                    if (this.storage[keyIndex].stack < this.storage[keyIndex].maxStack)
                    {
                        if (this.storage[keyIndex].stack + item.stack <= this.storage[keyIndex].maxStack)
                        {
                            this.storage[keyIndex].stack += item.stack;
                            item = this.storage[keyIndex];
                            return AddItemResult.STACKED;
                        }
                        else
                        {
                            int diff = (this.storage[keyIndex].stack + item.stack) - this.storage[keyIndex].maxStack;
                            this.storage[keyIndex].stack = this.storage[keyIndex].maxStack;
                            item.stack = (byte)diff;
                        }
                    }
            }

            byte index;
            if (!GetFreeStorageIndex(out index)) return AddItemResult.ERROR;
            item.index = index;

            this.inv.Add(index, item);

            return AddItemResult.NEW_INDEX;
        }

        public AddItemResult AddItemStorage(Item item, out byte index, out byte amount)
        {
            index = 0;
            amount = 0;
            if (item == null) return AddItemResult.ERROR;

            //stack if possible
            //Notice: stackable items should never be allowed to be stackable,
            //        as their unique settings will get lost in this loop
            foreach (byte keyIndex in this.storage.Keys)
            {
                if (this.storage[keyIndex].id == item.id)
                    if (this.storage[keyIndex].stack < this.storage[keyIndex].maxStack)
                    {
                        if (this.storage[keyIndex].stack + item.stack <= this.storage[keyIndex].maxStack)
                        {
                            this.storage[keyIndex].stack += item.stack;
                            item = this.storage[keyIndex];
                            index = keyIndex;
                            amount = this.storage[keyIndex].stack;
                            return AddItemResult.STACKED;
                        }
                        else
                        {
                            int diff = (this.storage[keyIndex].stack + item.stack) - this.storage[keyIndex].maxStack;
                            this.storage[keyIndex].stack = this.storage[keyIndex].maxStack;
                            item.stack = (byte)diff;
                        } 
                    }
            }

            if (!GetFreeStorageIndex(out index)) return AddItemResult.ERROR;
            item.index = index;

            this.storage.Add(index, item);
            amount = item.stack;

            return AddItemResult.NEW_INDEX;
        }


        public AddItemResult AddItem(Item item, out byte index, out byte amount)
        {
            index = 0;
            amount = 0;
            if(item == null) return AddItemResult.ERROR;

            //stack if possible
            //Notice: stackable items should never be allowed to be stackable,
            //        as their unique settings will get lost in this loop
            foreach (byte keyIndex in this.inv.Keys)
            {
                if(this.inv[keyIndex].id == item.id)
                    if (this.inv[keyIndex].stack < this.inv[keyIndex].maxStack)
                    {
                        if (this.inv[keyIndex].stack + item.stack <= this.inv[keyIndex].maxStack)
                        {
                            this.inv[keyIndex].stack += item.stack;
                            item = this.inv[keyIndex];
                            index = keyIndex;
                            amount = this.inv[keyIndex].stack;
                            return AddItemResult.STACKED;
                        }
                        else
                        {
                            int diff = (this.inv[keyIndex].stack + item.stack) - this.inv[keyIndex].maxStack;
                            this.inv[keyIndex].stack = this.inv[keyIndex].maxStack;
                            item.stack = (byte)diff;
                        } 
                    }
            }

            if(!GetFreeInventoryIndex(out index)) return AddItemResult.ERROR;
            item.index = index;

            this.inv.Add(index, item);
            amount = item.stack;

            return AddItemResult.NEW_INDEX;
        }

        public DeleteItemResult DeleteItem(CONTAINER_TYPE container, byte index, int itemID, byte amount, out byte newAmount)
        {
            newAmount = 0;

            Item item = this.GetItem(container, index);
            if (item == null) return DeleteItemResult.ERROR;
            if (item.id != itemID) return DeleteItemResult.WRONG_ITEMID;
            if (item.stack < amount) return DeleteItemResult.ERROR;
            if (item.stack == amount)
            {
                switch (container)
                {
                    case CONTAINER_TYPE.INVENTORY:
                        this.inv.Remove(index);
                        break;
                    case CONTAINER_TYPE.STORAGE :
                        this.storage.Remove(index);
                        break;                    
                }
                return DeleteItemResult.ALL_DELETED;
            }
            else
            {
                item.stack -= amount;
                newAmount = item.stack;
                return DeleteItemResult.NOT_ALL_DELETED;
            }
            
        }
        public DeleteItemResult DeleteItem(CONTAINER_TYPE container,  int itemID, byte amount,out byte index, out byte newAmount)
        {
            newAmount = 0;
            index = 0;
            
            foreach (byte i in this.inv.Keys)
            {
                Item item = null;
                if (container == CONTAINER_TYPE.INVENTORY)
                    item = this.inv[i];
                if (container == CONTAINER_TYPE.STORAGE)
                    item = this.storage[i];

                if (item.id != itemID) continue;
                if (item.stack < amount) return DeleteItemResult.ERROR;
                index = i;
                if (item.stack == amount)
                {
                    switch (container)
                    {
                        case CONTAINER_TYPE.INVENTORY:
                            this.inv.Remove(index);
                            break;
                        case CONTAINER_TYPE.STORAGE:
                            this.storage.Remove(index);
                            break;
                    } 
                    return DeleteItemResult.ALL_DELETED;
                }
                else
                {
                    item.stack -= amount;
                    newAmount = item.stack;
                    return DeleteItemResult.NOT_ALL_DELETED;
                }
            }

            return DeleteItemResult.ERROR;
        }

        public Item GetItem(CONTAINER_TYPE container, byte index)
        {
            switch (container)
            {
                case CONTAINER_TYPE.INVENTORY:
                    if (!this.inv.ContainsKey(index)) return null;
                    return this.inv[index];
                case CONTAINER_TYPE.STORAGE :
                    if (!this.storage.ContainsKey(index)) return null;
                    return this.storage[index];
                default:
                    return null;
            }
        }

        public Item GetItem(CONTAINER_TYPE container, int itemID)
        {
            switch (container)
            {
                case CONTAINER_TYPE.INVENTORY:
                    foreach (byte i in this.inv.Keys)
                    {
                        Item item = this.inv[i];
                        if (item.id != itemID) continue;
                        return item;
                    }
                    return null;
                case CONTAINER_TYPE.STORAGE:
                    foreach (byte i in this.storage.Keys)
                    {
                        Item item = this.storage[i];
                        if (item.id != itemID) continue;
                        return item;
                    }
                    return null;
                default:
                    return null;
            }
        }

        public List<Item> GetInventoryList(ITEM_TYPE listType)
        {
            List<Item> ret = new List<Item>();

            foreach (Item item in this.inv.Values)
                if (item.type == listType) ret.Add(item);

            return ret;
        }

        public List<Item> GetInventoryList()
        {
            List<Item> ret = new List<Item>();

            foreach (Item item in this.inv.Values)
               ret.Add(item);

            return ret;
        }

        public List<Item> GetStorageList()
        {
            List<Item> ret = new List<Item>();

            foreach (Item item in this.storage.Values)
                ret.Add(item);

            return ret;
        }

        public Dictionary<EQUIP_SLOT, Item> EquipList { get { return this.equip; } }


        public UnequipItemResult UnequipItem(byte sourceIndex, out byte index, out byte amount)
        {
            index = 0;
            amount = 0;

            EQUIP_SLOT fromSlot = (EQUIP_SLOT)sourceIndex;

            // equip slot is not empty
            if (!this.equip.ContainsKey(fromSlot)) return UnequipItemResult.ERROR;

            // move the item into the inventory
            AddItemResult aRes = this.AddItem(this.equip[fromSlot], out index, out amount);
            if(aRes == AddItemResult.ERROR) return UnequipItemResult.ERROR;

            // unequip the item
            this.equip.Remove(fromSlot);

            if(aRes == AddItemResult.NEW_INDEX) return UnequipItemResult.NEW_INDEX;
            else if(aRes == AddItemResult.STACKED) return UnequipItemResult.STACKED;

            return UnequipItemResult.ERROR;
        }


        public EquipItemResult EquipItem(byte sourceIndex, byte destIndex, out Item item)
        {
            item = null;

            EQUIP_SLOT toSlot = (EQUIP_SLOT)destIndex;

            // does the item exist?
            item = this.GetItem(CONTAINER_TYPE.INVENTORY, sourceIndex);
            if (item == null) return EquipItemResult.ERROR;

            // item is equipable
            //if (item.type != ITEM_TYPE.EQUIP) return EquipItemResult.NOT_EQUIPABLE;//is not working properly in OB Database

            //make sure the equipSlots match *does not work properly, yet*
            //if (this.inv[fromIndex].equipSlot != toSlot) return EquipItemResult.WRONG_EQUIPSLOT;

            // is the slot unequiped?
            if (!this.equip.ContainsKey(toSlot))
            {
                // if the equip slot was empty, we delete the sourceItemIndex from the inventory
                byte nAmount;
                this.DeleteItem(CONTAINER_TYPE.INVENTORY, sourceIndex, item.id, 1, out nAmount);
            }
            else
            {
                // if there's already another item at this slot, the client expects that
                // its switched with the new equip item
                this.inv[sourceIndex] = this.equip[toSlot];
                this.inv[sourceIndex].index = item.index;
                this.equip.Remove(toSlot);
            }


            this.equip.Add(toSlot, item);
            item.index = destIndex;

            return EquipItemResult.NO_ERROR;
        }

        public int[] GetEquipIDs()
        {
            int[] ret = new int[18];

            for (int i = 0; i < 18; i++)
            {
                if (this.equip.ContainsKey((EQUIP_SLOT)i))
                    ret[i] = this.equip[(EQUIP_SLOT)i].id;
                else ret[i] = 0;
            }
            return ret;
        }

        public byte[] GetEquipDyes()
        {
            byte[] dyes = new byte[18];
            for (int i = 0; i < 18; i++)
            {
                if (this.equip.ContainsKey((EQUIP_SLOT)i))
                    dyes[i] = this.equip[(EQUIP_SLOT)i].Dye;
                else
                    dyes[i] = 0;
            }
            return dyes;
        }

    }
}
