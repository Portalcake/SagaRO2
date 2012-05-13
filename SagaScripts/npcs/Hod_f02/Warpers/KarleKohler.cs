using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

namespace Hod_f02
{
public class XenoMiller : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f02";
        Type = 1278;
        Name = "XenoMiller";
        StartX = -2119F;
        StartY = -15122F;
        StartZ = 202F;
        Startyaw = 1000;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}