   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Moritz : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1026;
        Name = "Moritz Blauvelt";
        StartX = -5284F;
        StartY = -1663F;
        StartZ = -3931F;
        Startyaw = 10295;
        SetScript(512);
		
		AddMobLoot(10324, 355, 35502 , 4184, 5000);
		AddMobLoot(10325, 355, 35502 , 4184, 5000);
		AddMobLoot(10326, 355, 35502 , 4183, 5000);
		
		AddQuestItem(355, 35502, 1, 4183, 5);
		AddQuestItem(355, 35502, 2, 4184, 5);
		
		AddQuestStep(355, 35501, StepStatus.Active);
		AddQuestStep(356, 35601, StepStatus.Active);
		AddQuestStep(369, 36902, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));    
		AddButton(Functions.OfficialQuest, new func(OnQuest), true);   
        AddButton(Functions.Smith);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

    public void OnQuest(ActorPC pc)
    {
	if (GetQuestStepStatus(pc, 355, 35501) == StepStatus.Active)
	{
		UpdateQuest(pc, 355, 35501, StepStatus.Completed);
		UpdateIcon(pc);
		RemoveNavPoint(pc, 355);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
	}
	
	if (GetQuestStepStatus(pc, 356, 35601) == StepStatus.Active)
	{
		UpdateQuest(pc, 356, 35601, StepStatus.Completed);
		UpdateIcon(pc);
		RemoveNavPoint(pc, 356);
		AddNavPoint(356, 35602, 13, 1025, -2093f, -17903f, 681f); //Gretchel
		SendNavPoint(pc);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
	}
	
	if (GetQuestStepStatus(pc, 369, 36902) == StepStatus.Active)
	{
		UpdateQuest(pc, 369, 36902, StepStatus.Completed);
		UpdateIcon(pc);
		RemoveNavPoint(pc, 369);
		QuestCompleted(pc, 369);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
		SetReward(pc, new rewardfunc(OnReward));
	}
    }
	
	
    public void OnReward(ActorPC pc, uint QID)
    {
	if (QID == 369)
	{
		GiveExp(pc, 2142, 262);
		GiveZeny(pc, 792);
		GiveItem(pc, 1700114, 5);
		RemoveQuest(pc, 369);
	}
    }
}
