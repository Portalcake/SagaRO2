using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;

namespace SagaMap.Tasks
{
    public class OxygenUsage : MultiRunTask
    {
        private MapClient client;
        public bool diving = true;
        public OxygenUsage(MapClient client)
        {
            this.dueTime = 1;
            this.period = 1000;
            this.client = client;            
        }

        public override void  CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (diving)
                {
                    if (client.Char.LC > 0)
                    {
                        client.Char.LC--;
                    }
                    else
                    {
                        client.OxygenTakeDamage();
                        if (client.Char.HP == 0)
                            this.Deactivate();
                    }
                }
                else
                {
                    if (client.Char.LC < (client.Char.maxLC - 10))
                    {
                        client.Char.LC += 10;
                    }
                    else
                    {
                        client.Char.LC = client.Char.maxLC;
                        this.Deactivate();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
