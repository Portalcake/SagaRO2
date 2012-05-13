using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

namespace SagaMap.Scripting
{
    public abstract class QuestGroup
    {
        public delegate void OnAcceptPersonalQuest(ActorPC pc, uint QID, byte button);
        public Dictionary<int, QuestElement> handlers = new Dictionary<int, QuestElement>();
        public Dictionary<int, OnAcceptPersonalQuest> handlerAccept = new Dictionary<int, OnAcceptPersonalQuest>();

        protected void AddHandler(int step, QuestElement quest)
        {
            handlers.Add(step, quest);
        }

        protected void AddHandelerAccept(int step, OnAcceptPersonalQuest quest)
        {
            handlerAccept.Add(step, quest);
        }
    }
}
