   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Berickson : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1194;
        Name = "Berickson";
        StartX = -2725F;
        StartY = -5817F;
        StartZ = -3988F;
        Startyaw = 27655;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
