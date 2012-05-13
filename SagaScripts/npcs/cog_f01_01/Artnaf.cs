   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Artnaf : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1184;
        Name = "Artnaf";
        StartX = 3725.916F;
        StartY = -19434.18F;
        StartZ = 5702.666F;
        Startyaw = 24856;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
