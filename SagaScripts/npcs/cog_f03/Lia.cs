using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Lia : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f03";
        Type = 1199;
        Name = "Lia";
        StartX = 41205.24F;
        StartY = -21330.33F;
        StartZ = -5222.291F;
        Startyaw = 27616;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
