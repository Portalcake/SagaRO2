using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Nikki : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1051;
            Name = "Nikki Rozense";
            StartX = 14783F;
            StartY = 78975F;
            StartZ = 5088;
            Startyaw = 32768;
            SetScript(3);

		    AddQuestStep(304, 30402, StepStatus.Active);
		    AddQuestStep(305, 30501, StepStatus.Active);

            AddButton(Functions.OfficialQuest, new func(OnQuest), true);       
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

        public void OnQuest(ActorPC pc)
        {
            if (GetQuestStepStatus(pc, 304, 30402) == StepStatus.Active && CountItem(pc, 4053) > 0)
            {
                UpdateQuest(pc, 304, 30402, StepStatus.Completed);
				TakeItem(pc, 4053, 1);
                RemoveNavPoint(pc, 304);
				UpdateIcon(pc);
				QuestCompleted(pc, 304);
				NPCSpeech(pc, 823);
				NPCChat(pc, 0);
				SetReward(pc, new rewardfunc(OnReward));
			}

            if (GetQuestStepStatus(pc, 305, 30501) == StepStatus.Active)
            {
                UpdateQuest(pc, 305, 30501, StepStatus.Completed);
				UpdateIcon(pc);
				NPCSpeech(pc, 823);
				RemoveNavPoint(pc, 305);
				SendNavPoint(pc, 305, 1007, 22528f, 88672f, 5120f);
				NPCChat(pc, 0);
			}
        }

    	public void OnReward(ActorPC pc, uint QID)
    	{
		    if (QID == 304)
		    {
				GiveExp(pc, 0, 2763);
				GiveZeny(pc, 927);
				GiveItem(pc, 1700114, 6);
				RemoveQuest(pc, 304);
            	AddStep(305, 30501);
            	AddStep(305, 30502);
				AddNavPoint(305, 30501, 5, 1051, 14783f, 78975f, 5088f); //Nikki	
            	QuestStart(pc);
				SendNavPoint(pc);
				UpdateIcon(pc);
			}
		}
    }
}