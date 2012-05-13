   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Silvia : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1197;
        Name = "Silvia";
        StartX = 1900F;
        StartY = -12717F;
        StartZ = 5144F;
        Startyaw = 10887;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
