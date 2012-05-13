using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
	public class ArnoL : Npc
	{
	    public override void OnInit()
	    {
	        MapName = "Prt_f04";
	        Type = 1097;
	        Name = "Arno Ling";
	        StartX = -14230.25F;
	        StartY = 470.966F;
	        StartZ = -7404.452F;
	        Startyaw = 2743;
	        SetScript(823);
			
	        AddQuestItem(204, 20402, 3995, 7);			
			AddMobLoot(10289, 204, 20402, 3995, 2500);
			AddMobLoot(10290, 204, 20402, 3995, 2500);
			AddMobLoot(10291, 204, 20402, 3995, 2500);
			AddMobLoot(10292, 204, 20402, 3995, 2500);
			
			AddQuestStep(203, 20302, StepStatus.Active);
			AddQuestStep(204, 20401, StepStatus.Active);
			AddQuestStep(204, 20403, StepStatus.Active);
			AddQuestStep(205, 20501, StepStatus.Active);
			
	        AddButton(Functions.EverydayConversation, new func(OnButton));
	        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
	    }

	    public void OnButton(ActorPC pc)
	    {
	        NPCChat(pc, 823);
	    }

	    public void OnQuest(ActorPC pc)
	    {
	        if (GetQuestStepStatus(pc, 203, 20302) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 203, 20302, StepStatus.Completed);
				RemoveNavPoint(pc, 203);
	            QuestCompleted(pc, 203);
				UpdateIcon(pc); 
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 204, 20401) == StepStatus.Active)
	        {
	       	    UpdateQuest(pc, 204, 20401, StepStatus.Completed); 
				UpdateIcon(pc);
	            NPCSpeech(pc, 232); 
	            NPCChat(pc, 0);
	        }

			if (GetQuestStepStatus(pc, 204, 20403) == StepStatus.Active)
	        {
				if (CountItem(pc, 3995) >= 7)
				{
					TakeItem(pc, 3995, 7);
					UpdateQuest(pc, 204, 20403, StepStatus.Completed);			
					RemoveNavPoint(pc, 204);
	            	QuestCompleted(pc, 204);
	            	NPCSpeech(pc, 232);
					UpdateIcon(pc); 
	            	NPCChat(pc, 0);
	            	SetReward(pc, new rewardfunc(OnReward));
				}
        	}

	        if (GetQuestStepStatus(pc, 205, 20501) == StepStatus.Active)
        	{
	            UpdateQuest(pc, 205, 20501, StepStatus.Completed); 
			    UpdateIcon(pc); 
			    RemoveNavPoint(pc, 205); 
				AddNavPoint(205, 20502, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }
	    }

	    public void OnReward(ActorPC pc, uint QID)
	    {
			if (QID == 203)
			{
				GiveExp(pc, 0, 884);
				GiveZeny(pc, 324);
				RemoveQuest(pc, 203);				
				AddStep(204, 20401);
				AddStep(204, 20402);
		    	AddStep(204, 20403);
				AddNavPoint(204, 20401, 8, 1097, -14230.25f, 471.966f, -7404.452f); //Arno
	           	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}              
	
	        if (QID == 204)
			{
				GiveExp(pc, 0, 884);
				GiveZeny(pc, 324);
	           	RemoveQuest(pc, 204);
				AddStep(205, 20501);
				AddStep(205, 20502);
				AddNavPoint(205, 20501, 8, 1097, -14230.25f, 471.966f, -7404.452f); //Arno
	           	QuestStart(pc);  
				UpdateIcon(pc);		
				SendNavPoint(pc);				
	        }
	    }	
	}
}