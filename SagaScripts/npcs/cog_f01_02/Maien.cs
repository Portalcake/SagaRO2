   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Maien : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1193;
        Name = "Maien";
        StartX = -8895F;
        StartY = -19119F;
        StartZ = 587F;
        Startyaw = 48573;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
