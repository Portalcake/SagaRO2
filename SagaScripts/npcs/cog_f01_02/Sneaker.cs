   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Sneaker : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1191;
        Name = "Sneaker";
        StartX = -6533F;
        StartY = -11324F;
        StartZ = -494F;
        Startyaw = 47725;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
