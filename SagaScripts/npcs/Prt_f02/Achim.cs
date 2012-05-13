using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Achim : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1080;
            Name = "Achim";
            StartX = -7360F;
            StartY = -3904F;
            StartZ = 180;
            Startyaw = 9008;
            SetScript(3);
			
		    AddQuestStep(232, 23203, StepStatus.Active);
		    AddQuestStep(233, 23301, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 232, 23203) == StepStatus.Active)
            {
                UpdateQuest(pc, 232, 23203, StepStatus.Completed);
                UpdateIcon(pc);
            	QuestCompleted(pc, 232);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
            	SetReward(pc, new rewardfunc(OnReward));
            }

            if (GetQuestStepStatus(pc, 233, 23301) == StepStatus.Active)
            {
                UpdateQuest(pc, 233, 23301, StepStatus.Completed);
				GiveItem(pc, 4020, 1);
				RemoveNavPoint(pc, 233);
				AddNavPoint(233, 23302, 5, 1006, 11616f, 69760f, 5194f); //Meinhard
				UpdateIcon(pc);
                NPCSpeech(pc, 232);
                NPCChat(pc, 0);
			}
        }

    	public void OnReward(ActorPC pc, uint QID)
    	{
		    if (QID == 232)
		    {
				GiveExp(pc, 0, 1488);
				GiveZeny(pc, 587);
				GiveItem(pc, 1700113, 14);
				RemoveQuest(pc, 232);
            	AddStep(233, 23301);
            	AddStep(233, 23302);
				AddStep(233, 23303);
				AddNavPoint(233, 23301, 6, 1080, -7360f, -3904f, 180f); //Achim	
            	QuestStart(pc);
				SendNavPoint(pc);
				UpdateIcon(pc);
			}
		}
    }
}