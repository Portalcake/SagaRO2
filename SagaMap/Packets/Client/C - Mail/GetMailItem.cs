using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetMailItem : Packet
    {
        public GetMailItem() 
        {
            this.size = 12;
        }

        public uint GetMailID()
        {
            return this.GetUInt(4);
        }

        public uint GetItemID()
        {
            return this.GetUInt(8);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetMailItem();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGetMailItem(this);
        }
    }
}
