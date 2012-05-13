   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Edmund : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1185;
        Name = "Edmund Veervon";
        StartX = -2094F;
        StartY = -12718F;
        StartZ = -3988F;
        Startyaw = 29523;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
