  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Redick : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1139;
        Name = "Redick Dass";
        StartX = -4832F;
        StartY = 16992.0F;
        StartZ = 3904F;
        SetScript(1777);
        AddQuestStep(397, 39701, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 1786);
    }

    public void OnQuest(ActorPC pc)
    {
        if (GetQuestStepStatus(pc, 397, 39701) == StepStatus.Active)
        {
            UpdateQuest(pc, 397, 39701, StepStatus.Completed);
            SendNavPoint(pc, 397, 1067, 2646f, 18759f, 2820f);
            UpdateIcon(pc);
            NPCChat(pc, 0);
        }
    }
}
