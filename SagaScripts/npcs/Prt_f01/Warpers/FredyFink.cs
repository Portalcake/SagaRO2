using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class FredyFink : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f01";
        Type = 1282;
        Name = "FredyFink";
        StartX = -27198F;
        StartY = -33929F;
        StartZ = -970F;
        Startyaw = 16376;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}