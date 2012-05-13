   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Otto : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1180;
        Name = "Otto";
        StartX = 2132.833F;
        StartY = -25868.89F;
        StartZ = 6112F;
        Startyaw = 11256;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
