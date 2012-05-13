using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaMap.Tasks
{
    public class LPReduction : MultiRunTask
    {
        private MapClient client;
        public LPReduction(MapClient client)
        {
            this.dueTime = 1000;
            this.period = 10000;
            this.client = client;            
        }

        public override void  CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            if (this.client.Char.state == 1)
            {
                //donot recover during combat
                ClientManager.LeaveCriticalArea();                
                return;
            }
            if (client.Char.LP > 0)
            {
                client.Char.LP--;
                client.SendCharStatus(0);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
