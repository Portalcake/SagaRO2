using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
	public class ShantosK : Npc
	{
	    public override void OnInit()
	    {
	        MapName = "Prt_f04";
	        Type = 1092;
	        Name = "Shantos Kebby";
	        StartX = 8231.702F;
	        StartY = 46475.5F;
	        StartZ = -7303.248F;
	        Startyaw = 46929;
	        SetScript(823);
			
	        AddQuestItem(201, 20102, 3993, 8);
			
			AddQuestStep(200, 20002, StepStatus.Active);
			AddQuestStep(201, 20101, StepStatus.Active);
			AddQuestStep(201, 20103, StepStatus.Active);
			AddQuestStep(202, 20201, StepStatus.Active);
			
	        AddButton(Functions.EverydayConversation, new func(OnButton));
	        AddButton(Functions.OfficialQuest, new func(OnQuest), true);	
	    }
	    public void OnButton(ActorPC pc)
	    {
	        NPCChat(pc, 823);
	    }

	    public void OnQuest(ActorPC pc)
	    {
			if (GetQuestStepStatus(pc, 200, 20002) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 200, 20002, StepStatus.Completed);
	            QuestCompleted(pc, 200);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 200); 
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

			if (GetQuestStepStatus(pc, 201, 20101) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 201, 20101, StepStatus.Completed);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

			if (GetQuestStepStatus(pc, 201, 20103) == StepStatus.Active)
	        {
			    if (CountItem(pc, 3993) >= 8)
			    {
			    	UpdateQuest(pc, 201, 20103, StepStatus.Completed);
			    	TakeItem(pc, 3993, 8);
	            	QuestCompleted(pc, 201);
			    	UpdateIcon(pc);
					RemoveNavPoint(pc, 201);
	            	NPCSpeech(pc, 232);
	            	NPCChat(pc, 0);
	            	SetReward(pc, new rewardfunc(OnReward));
				}
	        }

			if (GetQuestStepStatus(pc, 202, 20201) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 202, 20201, StepStatus.Completed);
	            RemoveNavPoint(pc, 202);
				AddNavPoint(202, 20202, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
	            NPCSpeech(pc, 232);
				UpdateIcon(pc);
	            NPCChat(pc, 0);
	        }
	    }

	    public void OnReward(ActorPC pc, uint QID)
	    {
			if (QID == 200)
			{
				GiveExp(pc, 0, 792);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 200);
				AddStep(201, 20101);
				AddStep(201, 20102);
				AddStep(201, 20103);
				AddNavPoint(201, 20101, 8, 1092, 8231.702f, 46475.5f, -7303.248f); //Shantos
				QuestStart(pc); 
				UpdateIcon(pc);
				SendNavPoint(pc);
			}         

	        if (QID == 201)
			{
				GiveExp(pc, 0, 792);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 201);				
				AddStep(202, 20201);
				AddStep(202, 20202);
				AddNavPoint(202, 20201, 8, 1092, 8231.702f, 46475.5f, -7303.248f); //Shantos
				QuestStart(pc); 
				UpdateIcon(pc); 
				SendNavPoint(pc);
			}        
	    }
	}
}