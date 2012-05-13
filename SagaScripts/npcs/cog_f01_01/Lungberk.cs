using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Lungberk : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1202;
        Name = "Lungberk";
        StartX = -2594F;
        StartY = -8344F;
        StartZ = 5165F;
        Startyaw = 51653;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}
