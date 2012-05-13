   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Seberin : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1209;
        Name = "Seberin";
        StartX = 8150F;
        StartY = -4929F;
        StartZ = -4358F;
        Startyaw = 59041;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
