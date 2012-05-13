using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Dereck : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1021;
        Name = "Dereck Kroff";
        StartX = -9408F;
        StartY = -6118F;
        StartZ = -3949F;
        Startyaw = 60017;
        SetScript(497);
        AddButton(Functions.EverydayConversation, new func(OnButton));
	  AddButton(Functions.OfficialQuest, new func(OnQuest), true); 
        AddButton(Functions.Shop);

// Goods
AddGoods(100096); AddGoods(100097); AddGoods(100098); AddGoods(100099); AddGoods(400086); AddGoods(400087); AddGoods(400088); AddGoods(400089); AddGoods(300136); AddGoods(300137); AddGoods(300138); AddGoods(300139); AddGoods(500116); AddGoods(500117); AddGoods(500118); AddGoods(500119); AddGoods(570279); AddGoods(570280); AddGoods(570281); AddGoods(570282); AddGoods(700128); AddGoods(700129); AddGoods(800112); AddGoods(800113); AddGoods(2010002); AddGoods(2010011); AddGoods(2010020); AddGoods(2010028); AddGoods(2010035); AddGoods(2010044); 

// Quest Steps
AddQuestStep(368, 36803, StepStatus.Active);
AddQuestStep(357, 35701, StepStatus.Active);
    }
   
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }

    public void OnQuest(ActorPC pc)
    {
		if (GetQuestStepStatus(pc, 368, 36803) == StepStatus.Active && CountItem(pc, 4210) > 0)
		{
			UpdateQuest(pc, 368, 36803, StepStatus.Completed);
			UpdateIcon(pc);
			TakeItem(pc, 4210, 1);
			RemoveNavPoint(pc, 368);
			AddNavPoint(368, 36804, 13, 1023, -1096f, -145f, -3799f); // Ireyneal
			SendNavPoint(pc);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
		}
	
		if (GetQuestStepStatus(pc, 357, 35701) == StepStatus.Active)
		{
			UpdateQuest(pc, 357, 35701, StepStatus.Completed);
			UpdateIcon(pc);
			RemoveNavPoint(pc, 357);
			QuestCompleted(pc, 357);
			NPCSpeech(pc, 3);
			NPCChat(pc, 0);
			SetReward(pc, new rewardfunc(OnReward));
		}
	}
	
	public void OnReward(ActorPC pc, uint QID)
    {
		if (QID == 357)
		{
			GiveExp(pc, 2192, 0);
			GiveZeny(pc, 913);
			RemoveQuest(pc, 357);
		}
    }
}