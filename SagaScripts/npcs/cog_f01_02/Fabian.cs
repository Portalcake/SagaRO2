   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Fabian : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1210;
        Name = "Fabian";
        StartX = 8205F;
        StartY = -4020F;
        StartZ = -4315F;
        Startyaw = 27031;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
