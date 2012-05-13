using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod_f02
{
public class XenoMiller2 : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f02";
        Type = 1276;
        Name = "Xeno Miller";
        StartX = 42255F;
        StartY = -31241F;
        StartZ = 3862F;
        Startyaw = -26384;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}