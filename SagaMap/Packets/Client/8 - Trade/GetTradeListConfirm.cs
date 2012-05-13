using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTradeListConfirm : Packet // 0x0805
    {
       public GetTradeListConfirm()
       {
           this.size = 4;
       }
        
       public override SagaLib.Packet New()
       {
           return (SagaLib.Packet)new SagaMap.Packets.Client.GetTradeListConfirm();
       }

       public override void Parse(SagaLib.Client client)
       {
           ((MapClient)(client)).OnTradeListConfirm(this);
       }
    }
}