using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Isidor : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f02";
        Type = 1112;
        Name = "Isidor Dering";
        StartX = -50240F;
        StartY = -48480F;
        StartZ = -21447.35F;
        Startyaw = 0;
        SetScript(823);
		AddQuestStep(353, 35302, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
		AddButton(Functions.OfficialQuest, new func(OnQuest), true); 
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

    public void OnQuest(ActorPC pc)
    {
		if(GetQuestStepStatus(pc, 353, 35302) == StepStatus.Active && CountItem(pc, 4182) > 0)
		{
			UpdateQuest(pc, 353, 35302, StepStatus.Completed);
			UpdateIcon(pc);
			TakeItem(pc, 4182, 1);
			RemoveNavPoint(pc, 353);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
		}
    }
}
