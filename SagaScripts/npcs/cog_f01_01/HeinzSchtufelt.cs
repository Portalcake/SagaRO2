using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class HeinzSchtufelt : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1200;
        Name = "Heinz Schtufelt";
        StartX = -10265.61F;
        StartY = -33205.69F;
        StartZ = 6048F;
        Startyaw = 11184;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
