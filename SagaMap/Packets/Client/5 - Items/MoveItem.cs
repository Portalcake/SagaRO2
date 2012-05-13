using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public enum ITEM_MOVE_TYPE { EquToInv=1, InvToEqu,InvToSto, StoToInv };

    public class MoveItem : Packet
    {
        public MoveItem()
        {
            this.size = 9;
            this.offset = 4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 only observed</returns>
        public uint GetUnknown()
        {
           return this.GetByte(4);
        }

        /// <summary>
        /// Get Type of movement. 
        /// 1 = Equip Slot -> Inventory
        /// 2 = Inventory -> Equipment
        /// 3 = Storage -> Inventory
        /// </summary>
        /// <returns>1,2,3</returns>
        public ITEM_MOVE_TYPE GetMoveType()
        {
           byte type = this.GetByte(5);
           if (type == (byte)ITEM_MOVE_TYPE.EquToInv) return ITEM_MOVE_TYPE.EquToInv;
           else if (type == (byte)ITEM_MOVE_TYPE.InvToEqu) return ITEM_MOVE_TYPE.InvToEqu;
           else if (type == (byte)ITEM_MOVE_TYPE.InvToSto) return ITEM_MOVE_TYPE.InvToSto;
           else if (type == (byte)ITEM_MOVE_TYPE.StoToInv) return ITEM_MOVE_TYPE.StoToInv;

           return ITEM_MOVE_TYPE.EquToInv;
        }

        /// <summary>
        /// Source Index
        /// </summary>
        public byte GetSourceIndex()
        {
            return this.GetByte(6);
        }

        /// <summary>
        /// Get the Destination index (255 = remove)
        /// </summary>
        /// <returns>Destination index (0 - 254)</returns>
        public byte GetDestIndex()
        {
            return this.GetByte(7);
        }

        public byte GetAmount()
        {
           return this.GetByte(8);
        }



        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.MoveItem();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMoveItem(this);
        }

    }
}
