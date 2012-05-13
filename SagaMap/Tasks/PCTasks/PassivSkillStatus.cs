using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaDB.Actors;
using SagaLib;

namespace SagaMap.Tasks
{
    public class PassiveSkillStatus : MultiRunTask
    {
        public byte level;
        public delegate void CallBackFunc(Actor client);
        public delegate void DeactivateFunc(Actor obj);
        public CallBackFunc Func;
        public DeactivateFunc DeactFunc;
        public Actor client;
        public object var;
        public PassiveSkillStatus(byte level)
        {
            this.level = level;
            this.period = int.MaxValue;
        }

        public override void  CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            if (Func != null)
            {
                Func.Invoke(client);                
            }
            ClientManager.LeaveCriticalArea();
        }

        public override void Deactivate()
        {
            if (DeactFunc != null)
            {
                DeactFunc.Invoke(client);
            }
            base.Deactivate();
        }
    }
}
