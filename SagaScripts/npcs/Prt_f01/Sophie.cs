using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Sophie : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1151;
            Name = "Sophie Tristan";
            StartX = 21668F;
            StartY = 7200F;
            StartZ = 13318F;
            Startyaw = 16500;
            SetScript(3);
			
		    AddQuestStep(158, 15803, StepStatus.Active);
		    AddQuestStep(227, 22702, StepStatus.Active);
		    AddQuestStep(228, 22801, StepStatus.Active);
		    AddQuestStep(229, 22902, StepStatus.Active);
		    AddQuestStep(230, 23002, StepStatus.Active);
		    AddQuestStep(234, 23402, StepStatus.Active);
		
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
            {
                 NPCChat(pc, 823);
            }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 158, 15803) == StepStatus.Active && CountItem(pc, 3972) > 0)
            {
				UpdateQuest(pc, 158, 15803, StepStatus.Completed);
				TakeItem(pc, 3972, 1);
				GiveItem(pc, 3987, 1);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 158);
				SendNavPoint(pc, 158, 1013, 21109f, 68107f, 5209f);
				NPCSpeech(pc, 232);
				NPCChat(pc, 0);
			}

            if (GetQuestStepStatus(pc, 227, 22702) == StepStatus.Active)
            {
                UpdateQuest(pc, 227, 22702, StepStatus.Completed);
				UpdateIcon(pc);
                RemoveNavPoint(pc, 227);
            	QuestCompleted(pc, 227);
            	NPCSpeech(pc, 232);
            	NPCChat(pc, 0);
            	SetReward(pc, new rewardfunc(OnReward));
			}

            if (GetQuestStepStatus(pc, 228, 22801) == StepStatus.Active)
            {
                UpdateQuest(pc, 228, 22801, StepStatus.Completed);
				UpdateIcon(pc);
				RemoveNavPoint(pc, 228);
				SendNavPoint(pc, 228, 1012, 13931.36f, 74893.79f, 5049.054f);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
		    }

            if (GetQuestStepStatus(pc, 229, 22902) == StepStatus.Active)
            {
                UpdateQuest(pc, 229, 22902, StepStatus.Completed);
                UpdateIcon(pc);
                RemoveNavPoint(pc, 229);
                SendNavPoint(pc, 229, 1012, 13931.36f, 74893.79f, 5049.054f);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
            }

            if (GetQuestStepStatus(pc, 230, 23002) == StepStatus.Active)
            {
                UpdateQuest(pc, 230, 23002, StepStatus.Completed);
                UpdateIcon(pc);
                RemoveNavPoint(pc, 230);
                SendNavPoint(pc, 230, 1012, 13931.36f, 74893.79f, 5049.054f);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
            }

            if (GetQuestStepStatus(pc, 234, 23402) == StepStatus.Active)
            {
                UpdateQuest(pc, 234, 23402, StepStatus.Completed);
                UpdateIcon(pc);
                RemoveNavPoint(pc, 234);
                SendNavPoint(pc, 234, 1012, 13931.36f, 74893.79f, 5049.054f);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
            }
        }

    	public void OnReward(ActorPC pc, uint QID)
    	{
			if (QID == 227)
			{
				GiveExp(pc, 578, 0);
				GiveZeny(pc, 330);
				RemoveQuest(pc, 227);
            	AddStep(228, 22801);
            	AddStep(228, 22802);
            	AddStep(228, 22803);
            	AddStep(228, 22804);
				AddNavPoint(228, 22801, 5, 1151, 21668f, 7200f, 13318f); //Sophie
				UpdateIcon(pc);				
            	QuestStart(pc);
				SendNavPoint(pc);
			}
		}
    }
}