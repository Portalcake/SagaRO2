   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Tobi : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1231;
        Name = "Tobi Hull";
        StartX = -11561F;
        StartY = -27466F;
        StartZ = 673F;
        Startyaw = 9787;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
