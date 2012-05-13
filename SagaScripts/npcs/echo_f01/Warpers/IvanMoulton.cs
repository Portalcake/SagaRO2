using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class IvanMoulton : Npc
{
    public override void OnInit()
    {
        MapName = "echo_f01";
        Type = 1305;
        Name = "Ivan Moulton";
        StartX = -40603F;
        StartY = -678F;
        StartZ = -19784F;
        Startyaw = 31208;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}