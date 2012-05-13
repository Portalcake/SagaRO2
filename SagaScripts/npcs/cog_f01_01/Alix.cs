   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Alix : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1057;
        Name = "Alix Reed";
        StartX = 6965F;
        StartY = -21167.73F;
        StartZ = 6176F;
        Startyaw = -4936;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
