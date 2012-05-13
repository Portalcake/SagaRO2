using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Alment : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1153;
            Name = "Reymond";
            StartX = 46020F;
            StartY = -51421F;
            StartZ = 2698;
            Startyaw = 37112;
            SetScript(3); 
			
		    List<uint> Mobs = new List<uint>();
            Mobs.Add(10069);
            Mobs.Add(10070);
            AddEnemyInfo(182, 18202, Mobs, 6);
			
            AddQuestStep(182, 18201, StepStatus.Active);
            AddQuestStep(182, 18203, StepStatus.Active);
            AddQuestStep(183, 18301, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    	}
    	public void OnButton(ActorPC pc)
    	{
    	    NPCChat(pc, 823);
    	}

    	public void OnQuest(ActorPC pc)
    	{
    	    if (GetQuestStepStatus(pc, 182, 18201) == StepStatus.Active)
    	    {
    	        UpdateQuest(pc, 182, 18201, StepStatus.Completed);
    	        UpdateIcon(pc);
    	        NPCSpeech(pc, 232);
    	        NPCChat(pc, 0);
    	    }

    	    if (GetQuestStepStatus(pc, 182, 18203) == StepStatus.Active)
    	    {
    	        UpdateQuest(pc, 182, 18203, StepStatus.Completed);
				RemoveNavPoint(pc, 182);
    	        QuestCompleted(pc, 182);
    	        NPCSpeech(pc, 232);
     	        NPCChat(pc, 0);
    	        SetReward(pc, new rewardfunc(OnReward));
      	    }

    	    if (GetQuestStepStatus(pc, 183, 18301) == StepStatus.Active)
    	    {
    	        UpdateQuest(pc, 183, 18301, StepStatus.Completed);
    	        UpdateIcon(pc);
				RemoveNavPoint(pc, 183);
				SendNavPoint(pc, 183, 1068, 22528f, -33632f, -4064f);
     	        NPCSpeech(pc, 232);
    	        NPCChat(pc, 0);
    	    }
    	}

    	public void OnReward(ActorPC pc, uint QID)
		{
		   if (QID == 182)
		   {
				GiveExp(pc, 0, 528);
				GiveZeny(pc, 232);
				GiveItem(pc, 1700113, 3);
				RemoveQuest(pc, 182); 
				AddStep(183, 18301);
				AddStep(183, 18302);
				AddNavPoint(183, 18301, 6, 1153, 46020f, -51421f, 2698f); //Reymond
	           	QuestStart(pc);
				UpdateIcon(pc);
				SendNavPoint(pc);
			}     
        }
    }
}