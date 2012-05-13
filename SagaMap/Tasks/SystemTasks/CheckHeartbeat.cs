using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaMap.Tasks.SystemTasks
{
    public class CheckHeartbeat : MultiRunTask
    {
        private MapClient client;

        public CheckHeartbeat(MapClient client)
        {
            this.dueTime = 1000;
            this.period = 10000;
            this.client = client;
        }

        public override void CallBack(object o)
        {
            //ClientManager.EnterCriticalArea();

            // kick the client if he didn't send a heartbeat in the last 300s
            /*if ((this.client.lastHeartbeatRequest - this.client.lastHeartbeat) > this.period)
            {
                if (this.client.lastHeartbeatRequest != 0)
                {
                    //Logger.ShowWarning(this.client.netIO.sock.RemoteEndPoint.ToString() + " did not respond to heartbeat, kicking...", null);
                    //this.client.netIO.Disconnect();
                }
            }
            else
            {*/
                this.client.lastHeartbeatRequest = DateTime.Now.Ticks;
                this.client.RequestHeartbeat();
            //}


            //ClientManager.LeaveCriticalArea();
        }
    }
}
