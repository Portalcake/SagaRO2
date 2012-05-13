using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Henna : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1152;
            Name = "Helena";
            StartX = -47520F;
            StartY = -49440F;
            StartZ = 3094;
            Startyaw = 0;
            SetScript(3);
			
		    AddQuestItem(166, 16602, 3978, 5);
			
		    AddQuestStep(165, 16502, StepStatus.Active);
		    AddQuestStep(166, 16601, StepStatus.Active);
		    AddQuestStep(166, 16603, StepStatus.Active);
		    AddQuestStep(167, 16701, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
			AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }
		
    	public void OnQuest(ActorPC pc)
    	{
			if (GetQuestStepStatus(pc, 165, 16502) == StepStatus.Active)
			{
				UpdateQuest(pc, 165, 16502, StepStatus.Completed);
				UpdateIcon(pc);
				QuestCompleted(pc, 165);
				NPCSpeech(pc, 3);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
			}
			
        	if (GetQuestStepStatus(pc, 166, 16601) == StepStatus.Active)
        	{
				UpdateQuest(pc, 166, 16601, StepStatus.Completed);
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
        	}
			
			if (GetQuestStepStatus(pc, 166, 16603) == StepStatus.Active)
			{
				if(CountItem(pc, 3978) > 4)
				{
					TakeItem(pc, 3978, 5);
					GiveItem(pc, 3979, 1);
					RemoveNavPoint(pc, 166);
					UpdateQuest(pc, 166, 16603, StepStatus.Completed);
					UpdateIcon(pc);
					QuestCompleted(pc, 166);
					NPCSpeech(pc, 3);
					NPCChat(pc, 0);
					SetReward(pc, new rewardfunc(OnReward));
				}
			}
			
        	if (GetQuestStepStatus(pc, 167, 16701) == StepStatus.Active)
        	{
				UpdateQuest(pc, 167, 16701, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 167);
				AddNavPoint(167, 16702, 5, 1010, 9752f, 75223f, 5108f); //Regina	
				SendNavPoint(pc);
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
        	}
		}
		
	   	public void OnReward(ActorPC pc, uint QID)
		{
			if (QID == 165)
			{
				GiveExp(pc, 680, 0);
				GiveZeny(pc, 212);
				GiveItem(pc, 1700113, 2);
				RemoveQuest(pc, 165);
				AddStep(166, 16601);
				AddStep(166, 16602);
				AddStep(166, 16603);
				AddNavPoint(166, 16601, 6, 1152, -47520f, -49440f, 3094f); //Helena
				QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}
			
			if (QID == 166)
			{
				GiveExp(pc, 0, 448);
				GiveZeny(pc, 212);
				GiveItem(pc, 1700113, 2);
				RemoveQuest(pc, 166);
				AddStep(167, 16701);
				AddStep(167, 16702);
				AddNavPoint(167, 16701, 6, 1152, -47520f, -49440f, 3094f); //Helena
				QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}
		}
    }
}