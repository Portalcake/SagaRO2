using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class Alfons : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1059;
        Name = "Alfons Marsh";
        StartX = 2444F;
        StartY = -14936F;
        StartZ = -6437F;
        Startyaw = 25000;
        SetScript(768);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 757);
    }
}