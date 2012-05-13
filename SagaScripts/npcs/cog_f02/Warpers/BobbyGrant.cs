using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;


namespace cog_f02
{
public class BobbyGrant : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f02";
        Type = 1298;
        Name = "Bobby Grant";
        StartX = -42931F;
        StartY = -41613F;
        StartZ = -23099F;
        Startyaw = 17392;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
  }
}