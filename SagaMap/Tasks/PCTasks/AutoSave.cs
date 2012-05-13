using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaMap.Tasks
{
    public class AutoSave : MultiRunTask
    {
        private MapClient client;

        public AutoSave(MapClient client)
        {
            this.dueTime = 1000;
            this.period = 1800000;
            this.client = client;
        }

        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();   //lock for saving, needed?

            try
            {
                MapServer.charDB.SaveChar(client.Char);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex.InnerException);
                ClientManager.LeaveCriticalArea();
                return;
            }
            Logger.ShowInfo("Character: " + client.Char.name + " saved", null);

            ClientManager.LeaveCriticalArea();
        }
    }
}
