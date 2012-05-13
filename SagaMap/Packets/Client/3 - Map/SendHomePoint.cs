using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;


namespace SagaMap.Packets.Client
{
    public class SendHomePoint : Packet
    {
        public SendHomePoint()
        {
            this.size = 5;
            this.offset = 4;
        }

        public new byte GetType()
        {
            return this.GetByte(4);
        }

        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.SendHomePoint();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSendHomePoint(this);
        }



    }
}
