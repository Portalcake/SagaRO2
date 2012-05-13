using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Server
{
    public enum ITEM_MOVE_TYPE { EquToInv=1, InvToEqu,InvToSto, StoToInv };

    public class MoveItem : Packet
    {

        public MoveItem()
        {
            this.data = new byte[7];
            this.ID = 0x0514;
            this.offset = 4;
        }

        public void SetMoveType(ITEM_MOVE_TYPE type)
        {
            this.PutByte((byte)type, 4);
        }

        public void SetSourceIndex(byte index)
        {
            this.PutByte(index, 5);
        }

        public void SetDestIndex(byte index)
        {
            this.PutByte(index, 6);
        }


    }

}