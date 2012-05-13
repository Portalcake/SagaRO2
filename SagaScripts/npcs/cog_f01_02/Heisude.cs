   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Heisude : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1135;
        Name = "Portman Heisude";
        StartX = 7578F;
        StartY = -7310F;
        StartZ = -4358F;
        Startyaw = 59437;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
