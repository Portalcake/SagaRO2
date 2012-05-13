  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Averro : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1004;
        Name = "Averro Reinhold";
        StartX = 4672F;
        StartY = 9792F;
        StartZ = -9472F;
        Startyaw = 52000;
        SetScript(509);
        AddQuestStep(323, 32302, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.Shop);
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        AddGoods(50);
        AddGoods(9467);
            AddGoods(51500002); AddGoods(51500003); AddGoods(51500004); AddGoods(51500005); AddGoods(51500006); AddGoods(51500007); AddGoods(51500008); AddGoods(51500009); AddGoods(51500010); AddGoods(51500011); AddGoods(51500012);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 872);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 323, 32302) == StepStatus.Active)
        {
            UpdateQuest(pc, 323, 32302, StepStatus.Completed);
            GiveItem(pc, 2631, 1);
            UpdateIcon(pc);
            NPCSpeech(pc, 2225);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 323);
            SendNavPoint(pc, 323, 1000, -12092f, -6490f, -8284f); 
        }
    }
}