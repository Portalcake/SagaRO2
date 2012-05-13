using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f02
{
public class MattSinclair : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f02";
        Type = 1299;
        Name = "Matt Sinclair";
        StartX = -19343F;
        StartY = -25280F;
        StartZ = -21971F;
        Startyaw = -6208;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}