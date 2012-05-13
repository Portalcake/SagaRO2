  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Sez : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1141;
        Name = "Sez Lanez";
        StartX = 2450F;
        StartY = -5540F;
        StartZ = 1525F;
        Startyaw = 69352;
        money = 10000000;
        SetScript(1783);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        AddButton(Functions.Shop);

//Goods
AddGoods(51500002); AddGoods(16052); AddGoods(16098); AddGoods(100087); AddGoods(400077); AddGoods(300126); AddGoods(500107); AddGoods(570270); AddGoods(2010000); 

//Quest Steps
AddQuestStep(401, 40102, StepStatus.Active);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 1800);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 401, 40102) == StepStatus.Active)
        {
            UpdateQuest(pc, 401, 40102, StepStatus.Completed);
            UpdateIcon(pc);
            RemoveNavPoint(pc, 401);
            SendNavPoint(pc, 401, 1140, 9417f, -12150f, 657f);
            NPCSpeech(pc, 3938);
            NPCChat(pc, 0);
        }
    }

}