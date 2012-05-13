   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Hanna : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1186;
        Name = "Hanna";
        StartX = 5942F;
        StartY = -26441F;
        StartZ = -66F;
        Startyaw = 38621;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
