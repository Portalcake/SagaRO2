using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod_f02
{
public class HaultMontgomery : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f02";
        Type = 1275;
        Name = "Hault Montgomery";
        StartX = -42735F;
        StartY = -32162F;
        StartZ = 6118F;
        Startyaw = 45120;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}