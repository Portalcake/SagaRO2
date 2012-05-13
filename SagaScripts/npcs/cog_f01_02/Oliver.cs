   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Oliver : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1178;
        Name = "Oliver";
        StartX = -2241F;
        StartY = -30232F;
        StartZ = 533F;
        Startyaw = 5079;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
