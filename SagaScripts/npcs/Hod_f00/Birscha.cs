  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class Birscha : Npc
{
   public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1064;
        Name = "Hod03";
        StartX = 2646F;
        StartY = 18887F;
        StartZ = 2820F;
        Startyaw = 34168;
        SetScript(822);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 825);
    }

    public void OnQuest(ActorPC pc)
    {
        UpdateQuest(pc, 397, 39702, StepStatus.Completed);
        UpdateIcon(pc);
        RemoveNavPoint(pc, 397);
        QuestCompleted(pc, 397);
        SetReward(pc, new rewardfunc(OnReward));
        NPCChat(pc, 0);
    }
    public void OnReward(ActorPC pc, uint QID)
    {
        if (QID == 397)
        {
            GiveItem(pc, 13654, 1);
            GiveExp(pc, 180, 40);
            GiveZeny(pc, 4);
            RemoveQuest(pc, 397);
        }
    }

}