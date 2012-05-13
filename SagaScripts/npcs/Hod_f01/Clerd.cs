  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Clerd : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1165;
        Name = "Clerd";
        StartX = 11936F;
        StartY = 8160F;
        StartZ = -9984F;
        Startyaw = 17500;
        SetScript(2374);
        AddQuestStep(397, 39702, StepStatus.Active);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 2375);
    }
}
