using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Client
{
    public class GetTradeMoney : Packet // 0x0804
    {
        
       public GetTradeMoney()
       {
           this.size = 8;
       }

       public int GetMoney()
       {
           return this.GetInt(4);
       }

       public override SagaLib.Packet New()
       {
           return (SagaLib.Packet)new SagaMap.Packets.Client.GetTradeMoney();
       }

       public override void Parse(SagaLib.Client client)
       {
           ((MapClient)(client)).OnTradeMoney(this);
       }
       
    }
}
