   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Ireyneal : Npc
{
    //Kafra
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1023;
        Name = "Ireyneal Sheel";
        StartX = -1096F;
        StartY = -145F;
        StartZ = -3799F;
        Startyaw = 49857;
        SetScript(1810);
		SetSavePoint(13, -8175.749f, -22848.96f, 426.46f);
		
		/* Quest Monster */	
		List<uint> Mobs_363 = new List<uint>();
        Mobs_363.Add(10344);
        AddEnemyInfo(363, 36302, Mobs_363, 1);
		
		List<uint> Mobs_365_1 = new List<uint>();
        Mobs_365_1.Add(10322);
		Mobs_365_1.Add(10323);
        AddEnemyInfo(365, 36502, 1, Mobs_365_1, 6);
		
		List<uint> Mobs_365_2 = new List<uint>();
        Mobs_365_2.Add(10324);
		Mobs_365_2.Add(10325);
        AddEnemyInfo(365, 36502, 2, Mobs_365_2, 6);
		
		/* Quest Step */	
		AddQuestStep(363, 36301, StepStatus.Active);
		AddQuestStep(365, 36501, StepStatus.Active);
		AddQuestStep(365, 36503, StepStatus.Active);
		AddQuestStep(368, 36804, StepStatus.Active);
		
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.Kafra);
		AddButton(Functions.OfficialQuest, new func(OnQuest), true);        
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

    public void OnQuest(ActorPC pc)
    {
		if (GetQuestStepStatus(pc, 363, 36301) == StepStatus.Active)
		{
			UpdateQuest(pc, 363, 36301, StepStatus.Completed);
			UpdateIcon(pc);
			RemoveNavPoint(pc, 363);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
		}
		
		if (GetQuestStepStatus(pc, 365, 36501) == StepStatus.Active)
		{
			UpdateQuest(pc, 365, 36501, StepStatus.Completed);
			UpdateIcon(pc);
			RemoveNavPoint(pc, 365);
			AddNavPoint(365, 36503, 13, 1023, -1096f, -145f, -3799f); // Ireyneal
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
		}

		if (GetQuestStepStatus(pc, 365, 36503) == StepStatus.Active)
		{
		    UpdateQuest(pc, 365, 36503, StepStatus.Completed);
		    UpdateIcon(pc);
		    RemoveNavPoint(pc, 365);
		    QuestCompleted(pc, 365);
		    NPCSpeech(pc, 823);
		    NPCChat(pc, 0);
		    SetReward(pc, new rewardfunc(OnReward));
		}

		if (GetQuestStepStatus(pc, 368, 36804) == StepStatus.Active && CountItem(pc, 4210) > 0)
		{
			UpdateQuest(pc, 368, 36804, StepStatus.Completed);
			UpdateIcon(pc);
			TakeItem(pc, 4210, 1);
			RemoveNavPoint(pc, 368);
			AddNavPoint(368, 36805, 12, 1022, 1174f, -17256f, 5797f); // Cheyenne
			SendNavPoint(pc);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
		}
    }
	
	public void OnReward(ActorPC pc, uint QID)
    {
		if (QID == 365)
		{
			GiveExp(pc, 2474, 690);
			GiveZeny(pc, 913);
			GiveItem(pc, 1700114, 6);
			RemoveQuest(pc, 365);
		}
	}
}