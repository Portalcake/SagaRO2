using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod_f02
{
public class FrankBurbley : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f02";
        Type = 1277;
        Name = "Frank Burbley";
        StartX = -27408F;
        StartY = 36094F;
        StartZ = 4217F;
        Startyaw = 3984;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}