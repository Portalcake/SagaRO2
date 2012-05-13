using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace rag2startzone_01
{
public class Dein : Npc
{
    public override void OnInit()
    {
        MapName = "rag2startzone_01";
        Type = 1174;
        Name = "Dein";
        StartX = 9184.0F;
        StartY = 7584.0F;
        StartZ = 32F;
        Startyaw = 40000;
        SetScript(3997);
        AddQuestStep(396, 39601, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.OfficialQuest, new func(OnQuest), true);
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 4000);
    }

    public void OnQuest(ActorPC pc)
    {
        UpdateQuest(pc, 396, 39601, StepStatus.Completed);
        UpdateIcon(pc);
        RemoveNavPoint(pc, 396);
        NPCChat(pc, 3919);
        }

    }
}