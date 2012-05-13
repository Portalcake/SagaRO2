   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Yan : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1181;
        Name = "Yan";
        StartX = 3002F;
        StartY = -1210F;
        StartZ = -3986F;
        Startyaw = 43529;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
