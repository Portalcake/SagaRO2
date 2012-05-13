using System;
using System.Collections.Generic;
using SagaMap;

using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
	public class VolkerS : Npc
	{
	    public override void OnInit()
	    {
	        MapName = "Prt_f01";
	        Type = 1009;
	        Name = "Volker Stanwood";
	        StartX = 40375F;
	        StartY = 82998F;
	        StartZ = 3853F;
	        Startyaw = 7145;
	        SetScript(823);
			
			List<uint> Mobs = new List<uint>();
	        Mobs.Add(10081);
	        Mobs.Add(10082);
	        AddEnemyInfo(174, 17402, Mobs, 7);
			
			AddQuestItem(274, 27402, 4040, 8);
	        AddMobLoot(10294, 274, 27402, 4040, 2500);
	        AddMobLoot(10295, 274, 27402, 4040, 2500);
	        AddMobLoot(10296, 274, 27402, 4040, 2500);
	        AddMobLoot(10297, 274, 27402, 4040, 2500);
			
			AddQuestStep(173, 17302, StepStatus.Active);
			AddQuestStep(174, 17401, StepStatus.Active);
			AddQuestStep(175, 17501, StepStatus.Active);
			AddQuestStep(181, 18102, StepStatus.Active);
			AddQuestStep(274, 27401, StepStatus.Active);
	        AddButton(Functions.EverydayConversation, new func(OnButton));
	        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
	    }    

	    public void OnButton(ActorPC pc)
	    {
	        NPCChat(pc, 823);
	    }

	    public void OnQuest(ActorPC pc)
	    {
			if (GetQuestStepStatus(pc, 274, 27401) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 274, 27401, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 274);
	            NPCSpeech(pc, 823);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 173, 17302) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 173, 17302, StepStatus.Completed);
			    UpdateIcon(pc);
			    RemoveNavPoint(pc, 173);
	            QuestCompleted(pc, 173);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }

			if (GetQuestStepStatus(pc, 174, 17401) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 174, 17401, StepStatus.Completed);
				RemoveNavPoint(pc, 174);
				UpdateIcon(pc);
	            NPCSpeech(pc, 823);
	            NPCChat(pc, 0);
	        }
			if (GetQuestStepStatus(pc, 175, 17501) == StepStatus.Active)
	        {
	            UpdateQuest(pc, 175, 17501, StepStatus.Completed);
			    UpdateIcon(pc);
				RemoveNavPoint(pc, 175);
			    SendNavPoint(pc, 175, 1012, 13931.36f, 74893.79f, 5049.054f);
	            NPCSpeech(pc, 823);
	            NPCChat(pc, 0);
	        }

	        if (GetQuestStepStatus(pc, 181, 18102) == StepStatus.Active && CountItem(pc, 3986) > 0)
	        {
	            UpdateQuest(pc, 181, 18102, StepStatus.Completed);
			    UpdateIcon(pc);
			    TakeItem(pc, 3986, 1);
			    RemoveNavPoint(pc, 181);
	            QuestCompleted(pc, 181);
	            NPCSpeech(pc, 232);
	            NPCChat(pc, 0);
	            SetReward(pc, new rewardfunc(OnReward));
	        }
	    }

	    public void OnReward(ActorPC pc, uint QID)
	    {
	        if (QID == 173)
			{
				GiveExp(pc, 1240, 0);
				GiveZeny(pc, 300);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 173);
				AddStep(174, 17401);
				AddStep(174, 17402);
				AddStep(174, 17403);
				AddNavPoint(174, 17401, 5, 1009, 40375f, 82998f, 3853f); //Volker	
				QuestStart(pc); 
				UpdateIcon(pc);
				SendNavPoint(pc);
			}

	        if (QID == 181)
			{
				GiveExp(pc, 0, 448);
				GiveZeny(pc, 212);
				GiveItem(pc, 1700113, 2);
				RemoveQuest(pc, 181);
			}            
	    }
	}
}