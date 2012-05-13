using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actors;

namespace SagaMap.Scripting
{
    public class QuestElement : Npc
    {
        public delegate void OnQuest(ActorPC pc);
        private Dictionary<uint, OnQuest> handlers = new Dictionary<uint, OnQuest>();
        
        public void AddStepHandler(uint step,OnQuest quest)
        {
            handlers.Add(step, quest);
        }

        public void ProcessQuest(ActorPC pc, Npc npc, SagaDB.Quest.Quest quest)
        {
            this.Functable = npc.Functable;
            this.I = npc.Actor;
            this.ID = npc.ID;
            this.map = npc.Map;
            this.MapName = npc.MapName;
            this.Name = npc.Name;
            this.PersonalQuests = npc.PersonalQuests;
            this.PersonalQuesttable = npc.PersonalQuesttable;
            this.Questtable = npc.Questtable;
            this.SavePoint = npc.SavePoint;
            this.SupplyMatrials = npc.SupplyMatrials;
            this.SupplyMenuID = npc.SupplyMenuID;
            this.SupplyProducts = npc.SupplyProducts;
            this.Type = npc.Type;
            foreach (SagaDB.Quest.Step i in quest.Steps.Values)
            {
                if (i.Status == 1)
                {
                    if (handlers.ContainsKey(i.ID))
                    {
                        handlers[i.ID].Invoke(pc);
                    }
                }
            }
        }

    }
}
