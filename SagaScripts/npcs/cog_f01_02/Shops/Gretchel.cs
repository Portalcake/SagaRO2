using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Gretchel : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
		Type = 1025;
		Name = "Gretchel Ortrud";
		StartX = -2093F;
		StartY = -17903F;
		StartZ = 681F;
		Startyaw = 26791;
		SetScript(509);
		AddButton(Functions.EverydayConversation, new func(OnButton));    
		AddButton(Functions.OfficialQuest, new func(OnQuest), true);

// Quest Steps
AddQuestStep(356, 35602, StepStatus.Active);
AddQuestStep(356, 35604, StepStatus.Active);
AddQuestStep(368, 36802, StepStatus.Active);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

    public void OnQuest(ActorPC pc)
    {
	if (GetQuestStepStatus(pc, 356, 35602) == StepStatus.Active)
	{
		UpdateQuest(pc, 356, 35602, StepStatus.Completed);
		UpdateIcon(pc);
		RemoveNavPoint(pc, 356);
		AddNavPoint(356, 35603, 12, 1024, -8408f, -34514f, 6913f); //Pretan
		SendNavPoint(pc);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
	}

	if (GetQuestStepStatus(pc, 368, 36802) == StepStatus.Active && CountItem(pc, 4210) > 0)
	{
		UpdateQuest(pc, 368, 36802, StepStatus.Completed);
		UpdateIcon(pc);
		TakeItem(pc, 4210, 1);
		RemoveNavPoint(pc, 368);
		AddNavPoint(368, 36803, 13, 1021, -9408f, -6118f, -3949f); // Derek
		SendNavPoint(pc);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
	}

	if (GetQuestStepStatus(pc, 356, 35604) == StepStatus.Active)
	{
		UpdateQuest(pc, 356, 35604, StepStatus.Completed);
		UpdateIcon(pc);
		QuestCompleted(pc, 356);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
		SetReward(pc, new rewardfunc(OnReward));
	}
    }

    public void OnReward(ActorPC pc, uint QID)
    {
	if (QID == 356)
	{
		GiveExp(pc, 2464, 689);
		GiveZeny(pc, 1411);
		GiveItem(pc, 1700114, 12);
		RemoveQuest(pc, 356);
		AddStep(357, 35701);
		AddNavPoint(357, 35701, 13, 1021, -9408f, -6118f, -3949f); // Derek
		QuestStart(pc);
		SendNavPoint(pc);
	}
    }
}