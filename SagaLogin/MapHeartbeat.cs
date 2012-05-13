using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaLogin
{
    public class MapHeartbeat : MultiRunTask
    {
        private LoginClient client;

        public MapHeartbeat(LoginClient client)
        {
            this.dueTime = 1000;
            this.period = 60000;
            this.client = client;
        }

        public override void CallBack(object o)
        {
            this.client.mapServer.lastPing = DateTime.Now;
            this.client.pinging = true;
            this.client.RequestMapHeartbeat();
        }
    }
}
