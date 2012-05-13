  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Knightdagger : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1166;
        Name = "Knightdagger";
        StartX = 1728F;
        StartY = -2016F;
        StartZ = -8640F;
        Startyaw = 60000;
        SetScript(2370);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2371);
    }
}