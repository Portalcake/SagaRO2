  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Sally : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1147;
        Name = "Sally";
        StartX = 11520F;
        StartY = 17632F;
        StartZ = 2432;
        Startyaw = 14000;
        SetScript(2130);
        AddQuestStep(398, 39803, StepStatus.Active);
        AddQuestStep(399, 39901, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 4009);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 398, 39803) == StepStatus.Active)
        {
            if (CountItem(pc, 2666) >= 1)
            {
                TakeItem(pc, 2666, 1);
                UpdateQuest(pc, 398, 39803, StepStatus.Completed);
                QuestCompleted(pc, 398);
                UpdateIcon(pc);
                NPCSpeech(pc, 3936);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));

            }
        }
        if (GetQuestStepStatus(pc, 399, 39901) == StepStatus.Active)
        {
            UpdateQuest(pc, 399, 39901, StepStatus.Completed);
            UpdateIcon(pc);
            RemoveNavPoint(pc, 399);
            SendNavPoint(pc, 399, 1148, 17425f, 10411f, 1951f);
            GiveItem(pc, 9344, 1);
            NPCSpeech(pc, 3939);
            NPCChat(pc, 0);
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        if (QID == 398)
        {
            GiveExp(pc, 140, 50);
            GiveZeny(pc, 8);
            RemoveQuest(pc, 398);
            AddStep(399, 39901);
            AddStep(399, 39902);
            UpdateIcon(pc);
            QuestStart(pc);
            SendNavPoint(pc, 399, 1147, 11614f, 17682f, 2487f);
        }
    }

}
