using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_01
{
    public class Alina : Npc
    {
        public override void OnInit()
        {
            MapName = "cog_f01_01";
            Type = 1054;
            Name = "Alina Meiwes";
            StartX = -7713F;
            StartY = -14312F;
            StartZ = 6294F;
            Startyaw = 44001;
            SetScript(823);
            AddQuestStep(420, 42001, StepStatus.Active);
            AddButton(Functions.EverydayConversation, new func(OnButton));    
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);        
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }
    	public void OnQuest(ActorPC pc)
    	{
		if (GetQuestStepStatus(pc, 420, 42001) == StepStatus.Active)
		{
			UpdateQuest(pc, 420, 42001, StepStatus.Completed);
			UpdateIcon(pc);
			RemoveNavPoint(pc, 420);
	    	SendNavPoint(pc, 420, 1024, -8408f, -34514f, 6913f);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
		}
	}
    }
}