   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Heidi : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1058;
        Name = "Heidi Bath";
        StartX = 8105.971F;
        StartY = -21332.83F;
        StartZ = 6144F;
        Startyaw = -21304;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
