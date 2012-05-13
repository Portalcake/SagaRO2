using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetWhisper : Packet
    {
        public GetWhisper()
        {
            this.offset = 4;
            this.size = 8;
        }

        public override bool isStaticSize() { return false; }

        public string GetName()
        {
            return this.GetStringFixedSize(4, 34);
        }

        public string GetMessage()
        {
            if (this.isValid())
                return this.GetStringFixedSize(39, this.GetByte(38));
            else
                return "NOT_VALID";
        }

        public bool isValid()
        {
            ushort packetSize = this.GetUShort(0);
            byte textSize = this.GetByte(38);

           return true;
            
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.GetWhisper();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendWhisper(this);
        }
    }
}
