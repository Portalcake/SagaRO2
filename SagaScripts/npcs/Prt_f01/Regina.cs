using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Regina : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1010;
            Name = "Regina Salisbury";
            StartX = 9752F;
            StartY = 75223F;
            StartZ = 5108;
            Startyaw = 65768;
            SetScript(3);
			
            AddQuestStep(165, 16501, StepStatus.Active);
            AddQuestStep(167, 16702, StepStatus.Active);
            AddQuestStep(177, 17702, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    	public void OnQuest(ActorPC pc)
    	{
        	if (GetQuestStepStatus(pc, 165, 16501) == StepStatus.Active)
        	{
				UpdateQuest(pc, 165, 16501, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 165);
				AddNavPoint(165, 16502, 6, 1152, -47520f, -49440f, 3094f); //Helena	
				SendNavPoint(pc);
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
        	}
			
			if (GetQuestStepStatus(pc, 167, 16702) == StepStatus.Active)
			{
				if(CountItem(pc, 3979) > 0)
				{
					TakeItem(pc, 3979, 1);
					UpdateQuest(pc, 167, 16702, StepStatus.Completed);
					UpdateIcon(pc);
					RemoveNavPoint(pc, 167);
					QuestCompleted(pc, 167);
					NPCSpeech(pc, 3);
					NPCChat(pc, 0);
					SetReward(pc, new rewardfunc(OnReward));
				}
			}
			
        	if (GetQuestStepStatus(pc, 177, 17702) == StepStatus.Active)
        	{
				UpdateQuest(pc, 177, 17702, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 177);
				AddNavPoint(177, 17703, 6, 1089, -29860f, 4500f, 1082f); //Hanne	
				SendNavPoint(pc);
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
        	}
		}
	
	   	public void OnReward(ActorPC pc, uint QID)
		{
			if (QID == 167)
			{
				GiveExp(pc, 0, 448);
				GiveZeny(pc, 212);
				GiveItem(pc, 1700113, 2);
				RemoveQuest(pc, 167);
			}
		}
	}
}