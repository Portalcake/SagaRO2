using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actors;
using SagaMap.Scripting;

namespace SagaMap.Tasks
{
    public class DeleteCorpse : MultiRunTask
    {
        private Mob npc;
        public DeleteCorpse(Mob npc)
        {
            this.dueTime = 30000;
            this.period = 30000;
            this.npc = npc;            
        }

        public override void  CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                npc.Map.DeleteActor(npc.Actor);
                this.Deactivate();
            }
            catch (Exception)
            {              
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
