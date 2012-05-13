   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Ruud : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1190;
        Name = "Ruud";
        StartX = -5218F;
        StartY = -17108F;
        StartZ = -737F;
        Startyaw = 42645;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
