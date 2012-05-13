  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Nolang : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1167;
        Name = "Nolang";
        StartX = 7648F;
        StartY = -12576F;
        StartZ = -5952F;
        Startyaw = 30000;
        SetScript(2372);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2373);
    }
}
