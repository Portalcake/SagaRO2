using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetSelectButton : Packet
    {
        public GetSelectButton()
        {
            this.size = 6;
        }

        public byte GetSelection()
        {
            return this.GetByte(4);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetSelectButton();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSelectButton(this);
        }
    }
}
