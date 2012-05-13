using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Items;

namespace SagaMap.Packets.Client
{
    public class SortInvList : Packet
    {
        public SortInvList()
        {
            this.size = 5;
            this.offset = 4;
        }

        public ITEM_TYPE GetSortType()
        {
            byte filter = this.GetByte(4);

            if (filter == (byte)ITEM_TYPE.USEABLE) return ITEM_TYPE.USEABLE;
            else if (filter == (byte)ITEM_TYPE.EQUIP) return ITEM_TYPE.EQUIP;
            else if (filter == (byte)ITEM_TYPE.ETC) return ITEM_TYPE.ETC;

            return ITEM_TYPE.USEABLE;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SortInvList();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSortInvList(this);
        }

    }
}
