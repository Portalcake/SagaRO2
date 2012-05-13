using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_01
{
    public class Pretan : Npc
    {
        public override void OnInit()
        {
            MapName = "cog_f01_01";
            Type = 1024;
            Name = "Pretan Cornit";
            StartX = -8408F;
            StartY = -34514F;
            StartZ = 6913F;
            Startyaw = 27555;
            SetScript(3);
			AddQuestStep(420, 42002, StepStatus.Active);
			AddQuestStep(356, 35603, StepStatus.Active);
			AddQuestStep(353, 35301, StepStatus.Active);
            AddButton(Functions.EverydayConversation, new func(OnButton));    
			AddButton(Functions.OfficialQuest, new func(OnQuest), true);        
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }
		
    	public void OnQuest(ActorPC pc)
    	{

			if (GetQuestStepStatus(pc, 353, 35301) == StepStatus.Active)
			{
				UpdateQuest(pc, 353, 35301, StepStatus.Completed);
				UpdateIcon(pc);
				GiveItem(pc, 4182, 1);
				RemoveNavPoint(pc, 353);
				AddNavPoint(353, 35302, 14, 1112, -50240f, -48480f, 21447.35f); //Isidor
				SendNavPoint(pc);
				NPCSpeech(pc, 3);
				NPCChat(pc, 0);
			}

			if (GetQuestStepStatus(pc, 420, 42002) == StepStatus.Active)
			{
				UpdateQuest(pc, 420, 42002, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 420);
				SendNavPoint(pc, 420, 1182, 4934f, -17852f, 5756f);
				NPCSpeech(pc, 3);
				NPCChat(pc, 0);
			}

			if (GetQuestStepStatus(pc, 356, 35603) == StepStatus.Active)
			{
				UpdateQuest(pc, 356, 35603, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 356);
				AddNavPoint(356, 35604, 13, 1025, -2093f, -17903f, 681f); //Gretchel
				SendNavPoint(pc);
				NPCSpeech(pc, 3);
				NPCChat(pc, 0);
			}
		}
    }
}