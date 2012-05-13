using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_01
{
public class Chayenne : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1022;
        Name = "Cheyenne Yule";
        StartX = 1174F;
        StartY = -17256F;
        StartZ = 5797F;
        Startyaw = 60300;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true); 
        AddButton(Functions.Shop);

//Goods
AddGoods(4101); AddGoods(2583); AddGoods(2584); AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012); 

//Quest Steps
AddQuestStep(368, 36801, StepStatus.Active);
AddQuestStep(368, 36805, StepStatus.Active);
        }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }

    public void OnQuest(ActorPC pc)
    {
	if (GetQuestStepStatus(pc, 368, 36801) == StepStatus.Active)
	{
		UpdateQuest(pc, 368, 36801, StepStatus.Completed);
		UpdateIcon(pc);
		GiveItem(pc, 4210, 3);
		RemoveNavPoint(pc, 368);
		AddNavPoint(368, 36802, 13, 1025, -2093f, -17903f, 681f); // Gretchel
		SendNavPoint(pc);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
	}

	if (GetQuestStepStatus(pc, 368, 36805) == StepStatus.Active)
	{
		UpdateQuest(pc, 368, 36805, StepStatus.Completed);
		UpdateIcon(pc);
		RemoveNavPoint(pc, 368);
		QuestCompleted(pc, 368);
		NPCSpeech(pc, 3);
		NPCChat(pc, 0);
		SetReward(pc, new rewardfunc(OnReward));
	}
    }

    public void OnReward(ActorPC pc, uint QID)
    {
	if (QID == 368)
	{
		GiveExp(pc, 2136, 264);
		GiveZeny(pc, 1224);
		RemoveQuest(pc, 368);
	}
    }
}
}
