using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Tasks
{
    public class SpawnMobs : MultiRunTask
    {
        private ScriptManager manager;

        public SpawnMobs(ScriptManager manager)
        {
            this.dueTime = 1000;
            this.period = 5000;
            this.manager = manager;
        }

        public override void  CallBack(object o)
        {
            // Do something intelligent with mobs that spawn at specific times
            // IDEA: use the old time system we had for MeukRO.
        }
    }
}
