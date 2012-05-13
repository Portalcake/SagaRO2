   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Tilo : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1183;
        Name = "Tilo";
        StartX = 7198.916F;
        StartY = -26377.71F;
        StartZ = 6112F;
        Startyaw = -25328;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
