   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Alef : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1179;
        Name = "Alef";
        StartX = -4896F;
        StartY = -24922F;
        StartZ = 628F;
        Startyaw = 10095;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
