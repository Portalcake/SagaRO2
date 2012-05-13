using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Mark : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f03";
        Type = 1232;
        Name = "Mark Olin";
        StartX = -44.001F;
        StartY = -10.245F;
        StartZ = -10624.24F;
        Startyaw = 0;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
