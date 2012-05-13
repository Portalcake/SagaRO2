  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Conrad : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1146;
        Name = "Conrad";
        StartX = 6808F;
        StartY = 18568F;
        StartZ = 2622;
        Startyaw = 30000;
        SetScript(2121);
        AddQuestStep(398, 39801, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2124);
    }
    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 398, 39801) == StepStatus.Active)
        {
            UpdateQuest(pc, 398, 39801, StepStatus.Completed);
            UpdateIcon(pc);
            RemoveNavPoint(pc, 398);
            NPCSpeech(pc, 3933);
            NPCChat(pc, 0);
        }
    }
}