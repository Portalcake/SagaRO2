   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Anche : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1196;
        Name = "Anche";
        StartX = -1280F;
        StartY = -20640F;
        StartZ = 6144F;
        Startyaw = -21760;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
