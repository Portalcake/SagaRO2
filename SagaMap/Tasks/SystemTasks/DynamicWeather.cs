using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Tasks.SystemTasks
{
    public class DynamicWeather : MultiRunTask
    {
        private Map map;

        public DynamicWeather(Map M)
        {
            this.dueTime = Global.MakeMilliDelay(100);
            this.period = Global.MakeMinDelay(3);
            this.map = M;
        }

        public override void CallBack(object o)
        {            
            ClientManager.EnterCriticalArea();
            try
            {
                if (Global.Random.Next(0, 99) <= 90)//90% sunny
                    this.map.UpdateWeather(Global.WEATHER_TYPE.SUNNY);
                else
                    this.map.UpdateWeather((Global.WEATHER_TYPE)Global.Random.Next(1, 8));
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
