using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Vester : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1067;
        Name = "Vester Ling";
        StartX = 8256F;
        StartY = 1408F;
        StartZ = -7968F;
        Startyaw = 20000;
        SetScript(858);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2017);
    }
}