  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class Aigor : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f00";
        Type = 1145;
        Name = "Aigor Derleth";
        StartX = 10073F;
        StartY = 2314F;
        StartZ = 2386F;
        Startyaw = 26560;
        SetScript(2110);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2113);
    }
}