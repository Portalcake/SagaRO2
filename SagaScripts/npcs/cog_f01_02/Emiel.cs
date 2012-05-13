   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Emiel : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1188;
        Name = "Emiel";
        StartX = 5955F;
        StartY = -26747F;
        StartZ = 89F;
        Startyaw = 25683;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
