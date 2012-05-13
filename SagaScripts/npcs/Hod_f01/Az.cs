  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Az : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1136;
        Name = "Az Askew";
        StartX = 4960F;
        StartY = -1664F;
        StartZ = -8640F;
        Startyaw = 30000;
        SetScript(2364);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2366);
    }
}
