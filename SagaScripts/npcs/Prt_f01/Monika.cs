using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
	public class MonikaR : Npc
	{
	    public override void OnInit()
	    {
	        MapName = "Prt_f01";
	        Type = 1012;
	        Name = "Monika Reynolds";
	        StartX = 13931.36F;
	        StartY = 74893.79F;
	        StartZ = 5094.054F;
	        Startyaw = 16411;
	        SetScript(823);

			///////////////////////
	       //////Quest Mobs///////
	      ///////////////////////	

			List<uint> Mobs = new List<uint>();
	        Mobs.Add(10088);
	        AddEnemyInfo(176, 17602, Mobs, 5);

			///////////////////////
	       //////Quest Items//////
	      ///////////////////////

			AddQuestItem(228, 22803, 4018, 1);
	        AddMobLoot(10116, 228, 22803, 4018, 1000);
	        AddMobLoot(10117, 228, 22803, 4018, 1000);

			AddQuestItem(231, 23102, 4019, 1);
	        AddMobLoot(10074, 231, 23102, 4019, 1000);
	        AddMobLoot(10075, 231, 23102, 4019, 1000);

			///////////////////////
	       //////Quest Steps//////
	      ///////////////////////

			AddQuestStep(156, 15602, StepStatus.Active);
			AddQuestStep(157, 15701, StepStatus.Active);
			AddQuestStep(157, 15703, StepStatus.Active);
			AddQuestStep(158, 15801, StepStatus.Active);
			AddQuestStep(173, 17301, StepStatus.Active);
			AddQuestStep(175, 17502, StepStatus.Active);
			AddQuestStep(176, 17601, StepStatus.Active);
			AddQuestStep(177, 17701, StepStatus.Active);
			AddQuestStep(178, 17804, StepStatus.Active);
	        AddQuestStep(197, 19701, StepStatus.Active);
	        AddQuestStep(199, 19902, StepStatus.Active);
	        AddQuestStep(200, 20001, StepStatus.Active);
			AddQuestStep(202, 20202, StepStatus.Active);
			AddQuestStep(203, 20301, StepStatus.Active);
			AddQuestStep(205, 20502, StepStatus.Active);
			AddQuestStep(206, 20601, StepStatus.Active);
			AddQuestStep(208, 20802, StepStatus.Active);
			AddQuestStep(227, 22701, StepStatus.Active);
			AddQuestStep(228, 22802, StepStatus.Active);
			AddQuestStep(228, 22804, StepStatus.Active);
			AddQuestStep(229, 22901, StepStatus.Active);
			AddQuestStep(229, 22903, StepStatus.Active);
			AddQuestStep(230, 23001, StepStatus.Active);
			AddQuestStep(230, 23003, StepStatus.Active);
			AddQuestStep(231, 23101, StepStatus.Active);
			AddQuestStep(232, 23201, StepStatus.Active);
			AddQuestStep(233, 23303, StepStatus.Active);
			AddQuestStep(234, 23401, StepStatus.Active);
			AddQuestStep(234, 23403, StepStatus.Active);
			
	        AddButton(Functions.EverydayConversation, new func(OnButton));
	        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
	    }
	    public void OnButton(ActorPC pc)
	    {
	        NPCChat(pc, 823);
	    }

	    public void OnQuest(ActorPC pc)
	    {
			if (GetQuestStepStatus(pc, 156, 15602) == StepStatus.Active)
			{
				UpdateQuest(pc, 156, 15602, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 156);
				QuestCompleted(pc, 156);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
			}

			if (GetQuestStepStatus(pc, 157, 15701) == StepStatus.Active)
			{
				UpdateQuest(pc, 157, 15701, StepStatus.Completed);
				RemoveNavPoint(pc, 157);
				AddNavPoint(157, 15703, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
				SendNavPoint(pc);				
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
			}

			if (GetQuestStepStatus(pc, 157, 15703) == StepStatus.Active)
			{
			    UpdateQuest(pc, 157, 15703, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 157);
			    QuestCompleted(pc, 157);
			    NPCSpeech(pc, 823);
			    NPCChat(pc, 0);
			    SetReward(pc, new rewardfunc(OnReward));
			}

	        if (GetQuestStepStatus(pc, 158, 15801) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 158, 15801, StepStatus.Completed);
				UpdateIcon(pc);
	            RemoveNavPoint(pc, 158);
				SendNavPoint(pc, 158, 1014, 14032f, -347f, 13233f);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 173, 17301) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 173, 17301, StepStatus.Completed);
	            RemoveNavPoint(pc, 173);
				SendNavPoint(pc, 173, 1009, 40375f, 82998f, 3853f);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

			if (GetQuestStepStatus(pc, 175, 17502) == StepStatus.Active)
			{
			    UpdateQuest(pc, 175, 17502, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 175);
			    QuestCompleted(pc, 175);
			    NPCSpeech(pc, 823);
			    NPCChat(pc, 0);
			    SetReward(pc, new rewardfunc(OnReward));
			}

	        if (GetQuestStepStatus(pc, 176, 17601) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 176, 17601, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 176);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 177, 17701) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 177, 17701, StepStatus.Completed);
	            NPCSpeech(pc, 232);
				SendNavPoint(pc, 177, 1010, 9752f, 75223f, 5108f);
	            NPCChat(pc, 0);
	        }

			if (GetQuestStepStatus(pc, 178, 17804) == StepStatus.Active)
			{
			    UpdateQuest(pc, 178, 17804, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 178);
			    QuestCompleted(pc, 178);				
			    NPCSpeech(pc, 823);
			    NPCChat(pc, 0);
			    SetReward(pc, new rewardfunc(OnReward));
			}

	        if (GetQuestStepStatus(pc, 197, 19701) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 197, 19701, StepStatus.Completed);
				UpdateIcon(pc);
	            RemoveNavPoint(pc, 197);
				AddNavPoint(197, 19702, 8, 1098, 49415.03f, 7792.097f, -6106.818f); //Rufus	
				SendNavPoint(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 199, 19902) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 199, 19902, StepStatus.Completed);
				RemoveNavPoint(pc, 199);
	            QuestCompleted(pc, 199); 
	            NPCSpeech(pc, 232);
				UpdateIcon(pc);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 200, 20001) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 200, 20001, StepStatus.Completed);
				UpdateIcon(pc);
	            RemoveNavPoint(pc, 200);
				AddNavPoint(200, 20002, 8, 1092, 8231.702f, 46475.5f, -7303.248f); //Shantos	
				SendNavPoint(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 202, 20202) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 202, 20202, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 202);
	            QuestCompleted(pc, 202);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 203, 20301) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 203, 20301, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 203); 
				AddNavPoint(203, 20302, 8, 1097, -14230.25f, 471.966f, -7404.452f); //Arno
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 205, 20502) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 205, 20502, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 205);
	            QuestCompleted(pc, 205);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 206, 20601) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 206, 20601, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 206); 
				AddNavPoint(206, 20602, 8, 1095, -41102.66f, 29516.62f, -7938.911f); //Kuno
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 208, 20802) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 208, 20802, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 208);
	            QuestCompleted(pc, 208);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 227, 22701) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 227, 22701, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 227);
			    SendNavPoint(pc, 227, 1151, 21668f, 7200f, 13318f); 
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 228, 22802) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 228, 22802, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 228);
				AddNavPoint(228, 22804, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika					
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 228, 22804) == StepStatus.Active && CountItem(pc, 4018) > 0)
	        {
	            UpdateQuest(pc, 228, 22804, StepStatus.Completed);
				TakeItem(pc, 4018, 1);
				RemoveNavPoint(pc, 228);
	            QuestCompleted(pc, 228);
	            UpdateIcon(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
			}

	        if (GetQuestStepStatus(pc, 229, 22901) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 229, 22901, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 229);
			    SendNavPoint(pc, 229, 1151, 21668f, 7200f, 13318f);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 229, 22903) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 229, 22903, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 229);
	            QuestCompleted(pc, 229);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 230, 23001) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 230, 23001, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 230);
			    SendNavPoint(pc, 230, 1151, 21668f, 7200f, 13318f);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 230, 23003) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 230, 23003, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 230);
	            QuestCompleted(pc, 230);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 231, 23101) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 231, 23101, StepStatus.Completed);
				UpdateIcon(pc);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 232, 23201) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 232, 23201, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 232);
			    SendNavPoint(pc, 232, 1006, 11616f, 69760f, 5194f);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 233, 23303) == StepStatus.Active && CountItem(pc, 4021) > 0)
	        {
	            UpdateQuest(pc, 233, 23303, StepStatus.Completed);
			    UpdateIcon(pc);
			    TakeItem(pc, 4021, 1);
			    RemoveNavPoint(pc, 233);
	            QuestCompleted(pc, 233);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

	        if (GetQuestStepStatus(pc, 234, 23401) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 234, 23401, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 234);
			    SendNavPoint(pc, 234, 1151, 21668f, 7200f, 13318f);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 234, 23403) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 234, 23403, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 234);
	            QuestCompleted(pc, 234);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }
	    }

	    public void OnReward(ActorPC pc, uint QID)
	    {
			if (QID == 156)
			{
				GiveExp(pc, 680, 0);
				GiveZeny(pc, 212);
				GiveItem(pc, 1700113, 2);
				RemoveQuest(pc, 156);
			}

			if (QID == 157)
			{
				GiveExp(pc, 0, 448);
				GiveZeny(pc, 212);
				GiveItem(pc, 1700113, 2);
				RemoveQuest(pc, 157);
				UpdateIcon(pc);
				AddStep(158, 15801);
				AddStep(158, 15802);
				AddStep(158, 15803);
				AddStep(158, 15804);
				AddStep(158, 15805);
				AddNavPoint(158, 15801, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika				
				QuestStart(pc);  
				UpdateIcon(pc); 
				SendNavPoint(pc);
			} 

			if (QID == 175)
			{
				GiveExp(pc, 1240, 0);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 175);
				AddStep(176, 17601);
				AddStep(176, 17602);
				AddStep(176, 17603);
				AddNavPoint(176, 17601, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika	
				QuestStart(pc);  
				UpdateIcon(pc); 
				SendNavPoint(pc);
			} 

			if (QID == 178)
			{
				GiveExp(pc, 3500, 0);
				GiveZeny(pc, 810);
				GiveItem(pc, 1700114, 2);
				RemoveQuest(pc, 178);
			}

			if (QID == 199)
			{
				GiveExp(pc, 0, 792);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700114, 1);
				RemoveQuest(pc, 199);
				AddStep(200, 20001);
				AddStep(200, 20002);
				AddNavPoint(200, 20001, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika				
				QuestStart(pc);  
				UpdateIcon(pc); 
				SendNavPoint(pc);
			}           

	        if (QID == 202)
			{
				GiveExp(pc, 0, 792);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700114, 1);
				RemoveQuest(pc, 202);				
				AddStep(203, 20301);
				AddStep(203, 20302);
				AddNavPoint(203, 20301, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
				QuestStart(pc); 
				UpdateIcon(pc);
				SendNavPoint(pc);
			}          

	        if (QID == 205)
			{
				GiveExp(pc, 0, 884);
				GiveZeny(pc, 324);
				RemoveQuest(pc, 205);			
				AddStep(206, 20601);
				AddStep(206, 20602);
				AddNavPoint(206, 20601, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
	           	QuestStart(pc);  
				UpdateIcon(pc);	
				SendNavPoint(pc);				
			}           

	        if (QID == 208)
			{
	           	GiveExp(pc, 0, 976);
				GiveZeny(pc, 348);
				RemoveQuest(pc, 208);
			} 
	  
	        if (QID == 228)
			{
				GiveExp(pc, 1700, 0);
				GiveZeny(pc, 493);
				GiveItem(pc, 1700113, 12);
				RemoveQuest(pc, 228);
				AddStep(229, 22901);
				AddStep(229, 22902);
				AddStep(229, 22903);
				AddNavPoint(229, 22901, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
	           	QuestStart(pc);   
				UpdateIcon(pc);
				SendNavPoint(pc);
			} 
	        
	        if (QID == 229)
			{
				GiveExp(pc, 0, 726);
				GiveZeny(pc, 319);
				GiveItem(pc, 1700113, 6);
				RemoveQuest(pc, 229);
				AddStep(230, 23001);
				AddStep(230, 23002);
				AddStep(230, 23003);
				AddNavPoint(230, 23001, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
	           	QuestStart(pc);   
				UpdateIcon(pc);
				SendNavPoint(pc);
			}     

	        if (QID == 230)
			{
				GiveExp(pc, 0, 1225);
				GiveZeny(pc, 483);
				GiveItem(pc, 1700113, 10);
				RemoveQuest(pc, 230);
				AddStep(231, 23101);
				AddStep(231, 23102);
				AddStep(231, 23103);
				AddNavPoint(231, 23101, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
	           	QuestStart(pc);   
				UpdateIcon(pc);
				SendNavPoint(pc);
			}    

	        if (QID == 233)
			{
				GiveExp(pc, 0, 963);
				GiveZeny(pc, 380);
				GiveItem(pc, 1700113, 7);
				RemoveQuest(pc, 233);
				AddStep(234, 23401);
				AddStep(234, 23402);
				AddStep(234, 23403);
				AddNavPoint(234, 23401, 5, 1012, 13931.36f, 74893.79f, 5049.054f); //Monika
	           	QuestStart(pc); 
				UpdateIcon(pc);	
				SendNavPoint(pc);				
			}  

	        if (QID == 234)
			{
				GiveItem(pc, 1700113, 14);
				GiveItem(pc, 51700059, 1);
				GiveExp(pc, 0, 1488);
				GiveZeny(pc, 587);
				RemoveQuest(pc, 234); 
			} 
	    }
	}
}