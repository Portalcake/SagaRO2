  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Zarko : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1005;
        Name = "Zarko Ruzolli";
        StartX = -1216F;
        StartY = 3328F;
        StartZ = -10144F;
        Startyaw = 11000;
        SetScript(512);
        AddQuestStep(9, 901, StepStatus.Active);
        AddQuestStep(9, 903, StepStatus.Active);
        AddQuestStep(24, 2402, StepStatus.Active);
        AddQuestStep(25, 2502, StepStatus.Active);
        AddQuestStep(333, 33301, StepStatus.Active);
        AddQuestStep(333, 33303, StepStatus.Active);
        
        AddQuestStep(334, 33401, StepStatus.Active);
        AddQuestItem(334, 33402, 1, 2652, 1);
        AddQuestItem(334, 33402, 2, 2653, 1);
        AddQuestItem(334, 33402, 3, 2654, 1);
        AddQuestStep(334, 33403, StepStatus.Active);
        
        AddQuestStep(327, 32701, StepStatus.Active);
        AddPersonalQuestStep(5, 502, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));        
        AddButton(Functions.Smith);
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
        AddButton(Functions.PersonalQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 923);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 5, 502) == StepStatus.Active)
        {
            UpdateQuest(pc, 5, 502, StepStatus.Completed);
            UpdateIcon(pc);
            GiveItem(pc, 2618, 1);
            RemoveNavPoint(pc, 5);
            SendNavPoint(pc, 5, 1003, 12484f, -15132f, -4779f);
            NPCSpeech(pc, 39);
            NPCChat(pc, 0);            
        }
        if (GetQuestStepStatus(pc, 9, 901) == StepStatus.Active)
        {
            UpdateQuest(pc, 9, 901, StepStatus.Completed);
            UpdateIcon(pc);
            RemoveNavPoint(pc, 9);
            NPCSpeech(pc, 87);
            NPCChat(pc, 0);
        }
        if (GetQuestStepStatus(pc, 9, 903) == StepStatus.Active)
        {
            if (CountItem(pc, 2621) > 0 && CountItem(pc, 2622) > 0 && CountItem(pc, 2623) > 0)
            {
                UpdateQuest(pc, 9, 903, StepStatus.Completed);
                QuestCompleted(pc, 9);
                UpdateIcon(pc);
                TakeItem(pc, 2621, 1);
                TakeItem(pc, 2622, 1);
                TakeItem(pc, 2623, 1);
                NPCSpeech(pc, 90);
                AddRewardChoice(pc, 51300002);
                AddRewardChoice(pc, 51300004);
                UpdateIcon(pc);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 24, 2402) == StepStatus.Active)
        {
            UpdateQuest(pc, 24, 2402, StepStatus.Completed);
            QuestCompleted(pc, 24);
            UpdateIcon(pc);
            NPCSpeech(pc, 220);
            AddRewardChoice(pc, 50500000);
            AddRewardChoice(pc, 50400000);
            UpdateIcon(pc);
            NPCChat(pc, 0);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 25, 2502) == StepStatus.Active)
        {
            UpdateQuest(pc, 25, 2502, StepStatus.Completed);
            QuestCompleted(pc, 25);
            UpdateIcon(pc);
            NPCSpeech(pc, 232);
            UpdateIcon(pc);
            NPCChat(pc, 0);
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 333, 33301) == StepStatus.Active)
        {
            UpdateQuest(pc, 333, 33301, StepStatus.Completed);
            UpdateIcon(pc);
            GiveItem(pc, 2651, 1);
            SendNavPoint(pc, 333, 1002, 1460f, -13664f, -6472f);
            NPCSpeech(pc, 2273);
            NPCChat(pc, 0);
        }
        if (GetQuestStepStatus(pc, 333, 33303) == StepStatus.Active)
        {
            UpdateQuest(pc, 333, 33303, StepStatus.Completed);
            QuestCompleted(pc, 333);
            UpdateIcon(pc);
            NPCSpeech(pc, 226);
            UpdateIcon(pc);
            NPCChat(pc, 0);
            RemoveNavPoint(pc, 333);                
            SetReward(pc, new rewardfunc(OnReward));
        }
        if (GetQuestStepStatus(pc, 334, 33401) == StepStatus.Active)
        {
            UpdateQuest(pc, 334, 33401, StepStatus.Completed);
            UpdateIcon(pc);
            SendNavPoint(pc, 334, 1002, 1460f, -13664f, -6472f);
            NPCSpeech(pc, 2276);
            NPCChat(pc, 0);
        }
        if (GetQuestStepStatus(pc, 334, 33403) == StepStatus.Active)
        {
            if (CountItem(pc, 2653) > 0 && CountItem(pc, 2652) > 0)
            {
                UpdateQuest(pc, 334, 33403, StepStatus.Completed);
                TakeItem(pc, 2652, 1);
                TakeItem(pc, 2653, 1);
                QuestCompleted(pc, 334);
                UpdateIcon(pc);
                RemoveNavPoint(pc, 334);
                NPCSpeech(pc, 244);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 327, 32701) == StepStatus.Active)
        {
            UpdateQuest(pc, 327, 32701, StepStatus.Completed);
            UpdateIcon(pc);
            GiveItem(pc, 2624, 1);
            SendNavPoint(pc, 327, 1003, 12484f, -15132f, -4779f);
            NPCSpeech(pc, 8);
            NPCChat(pc, 0);
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        switch (QID)
        {
            case 9:
                GiveExp(pc, 83, 23);
                GiveZeny(pc, 99);
                RemoveQuest(pc, 9);
                AddStep(327, 32701);
                AddStep(327, 32702);
                QuestStart(pc);
                UpdateIcon(pc);
                break;
            case 24:
                GiveExp(pc, 63, 13);
                GiveZeny(pc, 32);
                RemoveQuest(pc, 24);
                AddStep(333, 33301);
                AddStep(333, 33302);
                AddStep(333, 33303);
                QuestStart(pc);
                UpdateIcon(pc);
                break;
            case 25:
                GiveExp(pc, 73, 18);
                GiveZeny(pc, 30);                
                RemoveQuest(pc, 25);
                AddStep(334, 33401);
                AddStep(334, 33402);
                AddStep(334, 33403);
                QuestStart(pc);
                UpdateIcon(pc);
                break;
            case 333:
                GiveExp(pc, 73, 18);
                GiveZeny(pc, 20);
                GiveItem(pc, 51200001, 1);
                GiveItem(pc, 16098, 1);
                RemoveQuest(pc, 333);
                break;
            case 334:
                GiveExp(pc, 73, 18);
                GiveZeny(pc, 40);
                RemoveQuest(pc, 334);                
                break;
        }
    }

}
