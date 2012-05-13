  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Denev : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1168;
        Name = "Denev";
        StartX = -4380F;
        StartY = 2658F;
        StartZ = -10100F;
        Startyaw = 52000;
        SetScript(2368);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2369);
    }
}