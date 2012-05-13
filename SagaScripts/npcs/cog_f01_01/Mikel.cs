using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_01
{
    public class Mikel : Npc
    {
        public override void OnInit()
        {
            MapName = "cog_f01_01";
            Type = 1182;
            Name = "Mikel";
            StartX = 4934F;
            StartY = -17852F;
            StartZ = 5756F;
            Startyaw = 27527;
            SetScript(3);
	    AddQuestStep(420, 42003, StepStatus.Active);
            AddButton(Functions.EverydayConversation, new func(OnButton));    
	    AddButton(Functions.OfficialQuest, new func(OnQuest), true);        
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }
    	public void OnQuest(ActorPC pc)
    	{
		if (GetQuestStepStatus(pc, 420, 42003) == StepStatus.Active)
		{
			UpdateQuest(pc, 420, 42003, StepStatus.Completed);
			UpdateIcon(pc);
			RemoveNavPoint(pc, 420);
			QuestCompleted(pc, 420);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
			SetReward(pc, new rewardfunc(OnReward));
		}
	}
   	public void OnReward(ActorPC pc, uint QID)
    	{
		if (QID == 420)
		{
			GiveExp(pc, 2370, 680);
			GiveZeny(pc, 1453);
			RemoveQuest(pc, 420);
		}
	}
    }
}