using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Mastis : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1127;
        Name = "Mastis";
        StartX = -7214F;
        StartY = -22228F;
        StartZ = 6112F;
        Startyaw = 11488;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}
