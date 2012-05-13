   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Rossen : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1230;
        Name = "Rossen Bice";
        StartX = 7273.712F;
        StartY = -15505.71F;
        StartZ = 5109.176F;
        Startyaw = 28080;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
