using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Binta : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f03";
        Type = 1123;
        Name = "Binta Granger";
        StartX = 36510.54F;
        StartY = -15962.06F;
        StartZ = -5241.041F;
        Startyaw = 25840;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
