using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class JojeffHofman : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1201;
        Name = "Jojeff Hofman";
        StartX = -7908F;
        StartY = -31890F;
        StartZ = 6784F;
        Startyaw = 11640;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}
