using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f04
{
	public class KunoA : Npc
	{
	    public override void OnInit()
	    {
	        MapName = "Prt_f04";
	        Type = 1095;
	        Name = "Kuno Aston";
	        StartX = -41102.66F;
	        StartY = 29516.62F;
	        StartZ = -7638.911F;
	        Startyaw = 17363;
	        SetScript(823);
			
	        AddQuestItem(207, 20702, 3997, 1);			
			AddMobLoot(10122, 207, 20702, 3997, 2500); 
			AddMobLoot(10123, 207, 20702, 3997, 2500); 
			AddMobLoot(10124, 207, 20702, 3997, 2500);  
			AddMobLoot(10125, 207, 20702, 3997, 2500);  
			AddMobLoot(10126, 207, 20702, 3997, 2500);  
			AddMobLoot(10127, 207, 20702, 3997, 2500);  
			
			AddQuestStep(206, 20602, StepStatus.Active);
			AddQuestStep(207, 20701, StepStatus.Active);
			AddQuestStep(207, 20703, StepStatus.Active);
			AddQuestStep(208, 20801, StepStatus.Active);
			
	        AddButton(Functions.EverydayConversation, new func(OnButton));
 	        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
	   }
	   public void OnButton(ActorPC pc)
	   {
	        NPCChat(pc, 823);
	   }

	    public void OnQuest(ActorPC pc)
	    {
	        if (GetQuestStepStatus(pc, 206, 20602) == StepStatus.Active)
			{
 	           UpdateQuest(pc, 206, 20602, StepStatus.Completed);
				RemoveNavPoint(pc, 206);
	            QuestCompleted(pc, 206);
	            NPCSpeech(pc, 232);
				UpdateIcon(pc);
	            NPCChat(pc, 0);
 	            SetReward(pc, new rewardfunc(OnReward));
			}

	        if (GetQuestStepStatus(pc, 207, 20701) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 207, 20701, StepStatus.Completed); 
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
				UpdateIcon(pc);
	        }

			if (GetQuestStepStatus(pc, 207, 20703) == StepStatus.Active)
	        {
	            if (CountItem(pc, 3997) >= 1)
			    {
					TakeItem(pc, 3997, 1);
			    	UpdateQuest(pc, 207, 20703, StepStatus.Completed);
					RemoveNavPoint(pc, 207);
	            	QuestCompleted(pc, 207);
					UpdateIcon(pc);
	            	NPCSpeech(pc, 232);
	            	NPCChat(pc, 0);
	            	SetReward(pc, new rewardfunc(OnReward));
				}
	        }

	        if (GetQuestStepStatus(pc, 208, 20801) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 208, 20801, StepStatus.Completed);
			    RemoveNavPoint(pc, 208); 
				AddNavPoint(208, 20802, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
			    UpdateIcon(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }
	    }

	    public void OnReward(ActorPC pc, uint QID)
	    {
	        if (QID == 206)
			{
				GiveExp(pc, 0, 976);
				GiveZeny(pc, 348);
				RemoveQuest(pc, 206);
				AddStep(207, 20701);
				AddStep(207, 20702);
				AddStep(207, 20703);
				AddNavPoint(207, 20701, 8, 1095, -41102.66f, 29516.62f, -7938.911f); //Kuno
				QuestStart(pc); 
				UpdateIcon(pc);	
				SendNavPoint(pc);				
			}	            

	        if (QID == 207)
			{
 	           	GiveExp(pc, 0, 976);
				GiveZeny(pc, 348);
				RemoveQuest(pc, 207);
				AddStep(208, 20801);
				AddStep(208, 20802);
				AddNavPoint(208, 20801, 8, 1095, -41102.66f, 29516.62f, -7938.911f); //Kuno
 	           	QuestStart(pc); 
				UpdateIcon(pc);
				SendNavPoint(pc);
			}            
	    }
	}
}