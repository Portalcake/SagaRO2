using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class MulderLouth : Npc
{
    public override void OnInit()
    {
        MapName = "echo_f01";
        Type = 1304;
        Name = "Mulder Louth";
        StartX = 11878F;
        StartY = -40575F;
        StartZ = -23494F;
        Startyaw = 3328;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}