  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Waynedi : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1140;
        Name = "Waynedi Arga";
        StartX = 8922F;
        StartY = -11445F;
        StartZ = 697F;
        Startyaw = 42000;
        SetScript(1780);
        AddQuestStep(401, 40103, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.Smith);
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 1793);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 401, 40103) == StepStatus.Active)
        {
            UpdateQuest(pc, 401, 40103, StepStatus.Completed);
            QuestCompleted(pc, 401);
            UpdateIcon(pc);
            NPCSpeech(pc, 3951);
            NPCChat(pc, 0);
            SetReward(pc, new rewardfunc(OnReward));
        }
    }

    public void OnReward(ActorPC pc, uint QID)
    {
        if (QID == 401)
        {
            GiveExp(pc, 140, 50);
            GiveZeny(pc, 5);
            RemoveNavPoint(pc, 401);
            RemoveQuest(pc, 401);
        }
    }
}