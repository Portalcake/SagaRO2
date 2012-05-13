using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Quadro : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1068;
            Name = "Quadro Mann";
            StartX = 22528F;
            StartY = -33632F;
            StartZ = -4064;
            Startyaw = 33736;
            SetScript(3);
			
            AddQuestStep(183, 18302, StepStatus.Active);
			
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    	}
		
    	public void OnButton(ActorPC pc)
    	{
    	    NPCChat(pc, 823);
    	}

    	public void OnQuest(ActorPC pc)
    	{
    	    if (GetQuestStepStatus(pc, 183, 18302) == StepStatus.Active)
    	    {
    	        UpdateQuest(pc, 183, 18302, StepStatus.Completed);
    	        UpdateIcon(pc);
    	        QuestCompleted(pc, 183);
      	        RemoveNavPoint(pc, 183);
    	        NPCSpeech(pc, 232);
    	        NPCChat(pc, 0);
    	        SetReward(pc, new rewardfunc(OnReward));
    	    }
		}

    	public void OnReward(ActorPC pc, uint QID)
    	{
		    if (QID == 183)
	 	    {
            	GiveExp(pc, 800, 0);
            	GiveZeny(pc, 232);
				GiveItem(pc, 1700113, 3);
            	RemoveQuest(pc, 183); 
			}     
    	}
    }
}