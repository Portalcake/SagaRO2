  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Whitney : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1148;
        Name = "Whitney";
        StartX = 17440F;
        StartY = 10272F;
        StartZ = 1888;
        Startyaw = 0;
        SetScript(2135);
        AddQuestStep(399, 39902, StepStatus.Active);
        AddQuestStep(400, 40002, StepStatus.Active);
        AddQuestStep(401, 40101, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);

        AddQuestItem(400, 40001, 2603, 3);
        AddMobLoot(10002, 400, 40001, 2603, 8000);
        AddMobLoot(10003, 400, 40001, 2603, 8000);
        AddMobLoot(10004, 400, 40001, 2603, 8000);
        
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2138);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 399, 39902) == StepStatus.Active)
        {
            if (CountItem(pc, 9344) >= 1)
            {
                UpdateQuest(pc, 399, 39902, StepStatus.Completed);
                QuestCompleted(pc, 399);
                TakeItem(pc, 9344, 1);
                NPCSpeech(pc, 3942);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 400, 40002) == StepStatus.Active)
        {
            if (CountItem(pc, 2603) >= 3)
            {
                TakeItem(pc, 2603, 3);
                UpdateQuest(pc, 400, 40002, StepStatus.Completed);
                NPCSpeech(pc, 3945);
                NPCChat(pc, 0);
                QuestCompleted(pc, 400);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 401, 40101) == StepStatus.Active)
        {
            UpdateQuest(pc, 401, 40101, StepStatus.Completed);
            NPCSpeech(pc, 4549);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 401);
            SendNavPoint(pc, 401, 1141, 2450f, -5540f, 1525f);
        }
        UpdateIcon(pc);
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        switch (QID)
        {
            case 399:
                GiveExp(pc, 200, 40);
                GiveZeny(pc, 6);
                RemoveNavPoint(pc, 399);
                RemoveQuest(pc, 399);
                AddStep(400, 40001);
                AddStep(400, 40002);
                QuestStart(pc);
                UpdateIcon(pc);                
                break;
            case 400:
                GiveExp(pc, 202, 75);
                GiveZeny(pc, 10);
                GiveItem(pc, 91, 1);
                RemoveNavPoint(pc, 400);
                RemoveQuest(pc, 400);
                AddStep(401, 40101);
                AddStep(401, 40102);
                AddStep(401, 40103);
                QuestStart(pc);
                UpdateIcon(pc);
                SendNavPoint(pc, 401, 1148, 17425f, 10411f, 1951f);
                break;
        }
    }
}