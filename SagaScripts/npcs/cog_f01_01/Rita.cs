   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Rita : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1177;
        Name = "Rita";
        StartX = 7444.252F;
        StartY = -4087.072F;
        StartZ = 5312F;
        Startyaw = 11312;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
