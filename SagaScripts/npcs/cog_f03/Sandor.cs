using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Sandor : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f03";
        Type = 1187;
        Name = "Sandor";
        StartX = 30450.71F;
        StartY = -18228.69F;
        StartZ = -5772.02F;
        Startyaw = -3968;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
