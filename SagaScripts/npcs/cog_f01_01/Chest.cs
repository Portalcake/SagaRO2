using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Chest : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1126;
        Name = "Chest";
        StartX = -7834F;
        StartY = -22550F;
        StartZ = 6144F;
        Startyaw = 14040;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}
