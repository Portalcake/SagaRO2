using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Lucia : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f02";
        Type = 1198;
        Name = "Lucia Haswell";
        StartX = -44662.73F;
        StartY = -43572.38F;
        StartZ = -22834.75F;
        Startyaw = 30000;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
