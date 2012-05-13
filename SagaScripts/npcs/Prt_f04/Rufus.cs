using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
	public class RufusH : Npc
	{
	    public override void OnInit()
	    {
	        MapName = "Prt_f04";
	        Type = 1098;
	        Name = "Rufus Haw";
	        StartX = 49415.03F;
	        StartY = 7792.097F;
	        StartZ = -6106.818F;
	        Startyaw = 30959;
	        SetScript(823);
			
	        List<uint> Mobs = new List<uint>();
	        Mobs.Add(10114);
	        Mobs.Add(10115);
	        AddEnemyInfo(198, 19802, Mobs, 8);
			
			AddQuestStep(197, 19702, StepStatus.Active);
	        AddQuestStep(198, 19801, StepStatus.Active);
			AddQuestStep(198, 19803, StepStatus.Active);
	        AddQuestStep(199, 19901, StepStatus.Active);
			
	        AddButton(Functions.EverydayConversation, new func(OnButton));
	        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
	    }
	    public void OnButton(ActorPC pc)
	    {
	        NPCChat(pc, 823);
	    }

	    public void OnQuest(ActorPC pc)
	    {
			if (GetQuestStepStatus(pc, 197, 19702) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 197, 19702, StepStatus.Completed);
	            QuestCompleted(pc, 197);
				RemoveNavPoint(pc, 197);
	            UpdateIcon(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 198, 19801) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 198, 19801, StepStatus.Completed);
	            UpdateIcon(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 198, 19803) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 198, 19803, StepStatus.Completed);
	            QuestCompleted(pc, 198);
				RemoveNavPoint(pc, 198);
	            UpdateIcon(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 199, 19901) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 199, 19901, StepStatus.Completed);
	            UpdateIcon(pc);
			    RemoveNavPoint(pc, 199); 
				AddNavPoint(199, 19902, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
				SendNavPoint(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }
	    }

	    public void OnReward(ActorPC pc, uint QID)
	    {
			if (QID == 197)
			{
				GiveExp(pc, 0, 792);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 197); 
				AddStep(198, 19801);
				AddStep(198, 19802);
		    	AddStep(198, 19803);
				AddNavPoint(198, 19801, 8, 1098, 49415.03f, 7792.097f, -6106.818f); //Rufus
	           	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}

	        if (QID == 198)
			{
				GiveExp(pc, 0, 792);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 198);  
				AddStep(199, 19901);
				AddStep(199, 19902);
				AddNavPoint(199, 19901, 8, 1098, 49415.03f, 7792.097f, -6106.818f); //Rufus
	           	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}               
	    }
	}
}