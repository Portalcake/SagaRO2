  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Kwanto : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1063;
        Name = "Kwanto Randal";
        StartX = -20091F;
        StartY = -2895F;
        StartZ = 2678F;
        Startyaw = -12376;
        SetScript(810);
        AddQuestStep(402, 40202, StepStatus.Active);
        AddQuestStep(406, 40601, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 813);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 402, 40202) == StepStatus.Active)
        {
            if (CountItem(pc, 2630) >= 3)
            {
                TakeItem(pc, 2630, 3);
                UpdateQuest(pc, 402, 40202, StepStatus.Completed);
                QuestCompleted(pc, 402);
                UpdateIcon(pc);
                NPCSpeech(pc, 3954);
                NPCChat(pc, 0);
                SetReward(pc, new rewardfunc(OnReward));
            }
        }
        if (GetQuestStepStatus(pc, 406, 40601) == StepStatus.Active)
        {
            UpdateQuest(pc, 406, 40601, StepStatus.Completed);
            NPCSpeech(pc, 4557);
            GiveItem(pc, 4245, 1);
            NPCChat(pc, 0);
            UpdateIcon(pc);
            RemoveNavPoint(pc, 406);
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        if (QID == 402)
        {
            GiveExp(pc, 140, 50);
            GiveZeny(pc, 7);
            GiveItem(pc, 13653, 1);
            RemoveQuest(pc, 402);
            AddStep(406, 40601);
            AddStep(406, 40602);            
            QuestStart(pc);
            UpdateIcon(pc);
        }
    }
}