  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Eind : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1170;
        Name = "Eind";
        StartX = -224F;
        StartY = -3904F;
        StartZ = -8544F;
        Startyaw = 13000;
        SetScript(3467);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 3468);
    }
}