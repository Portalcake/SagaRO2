   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Yosef : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1201;
        Name = "Yosef Hufman";
        StartX = -7908.375F;
        StartY = -31890.86F;
        StartZ = 6784F;
        Startyaw = 11640;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
