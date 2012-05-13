using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace cog_f01_02
{
public class BenjaminMeier : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1297;
        Name = "Benjamin Meier";
        StartX = -9559F;
        StartY = -30578F;
        StartZ = 6814F;
        Startyaw = 2568;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}