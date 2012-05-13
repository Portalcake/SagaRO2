  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Arolas : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1065;
        Name = "Arolas Botolph";
        StartX = 352F;
        StartY = -10880F;
        StartZ = -6577F;
        Startyaw = -12400;
        SetScript(834);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 837);
    }
}