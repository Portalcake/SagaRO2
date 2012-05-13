using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Aili : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f03";
        Type = 1028;
        Name = "Aili Weller";
        StartX = 34143.8F;
        StartY = -25935.78F;
        StartZ = -5200.784F;
        Startyaw = 15592;
        SetScript(823);
        SetSavePoint(15, 34537f, -24226f, -5525f);     

		///////////////////////
       //////Quest Steps//////
      ///////////////////////

		AddQuestStep(426, 42602, StepStatus.Active); 
  
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);     
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.Kafra);
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 426, 42602) == StepStatus.Active)
        {
			UpdateQuest(pc, 426, 42602, StepStatus.Completed);
			UpdateIcon(pc);
			QuestCompleted(pc, 426);
			NPCSpeech(pc, 823);
			NPCChat(pc, 0);
			SetReward(pc, new rewardfunc(OnReward));
		}
    }

    public void OnReward(ActorPC pc, uint QID)
    {
	if (QID == 426)
		{
            GiveExp(pc, 2379, 886);
            GiveZeny(pc, 1853);
			GiveItem(pc, 1700115, 1);
            RemoveQuest(pc, 426);
		}
    }
}
