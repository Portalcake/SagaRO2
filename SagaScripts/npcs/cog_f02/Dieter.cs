using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Dieter : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f02";
        Type = 1206;
        Name = "Dieter";
        StartX = -43171.34F;
        StartY = -38798.25F;
        StartZ = -23309.3F;
        Startyaw = 0;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
