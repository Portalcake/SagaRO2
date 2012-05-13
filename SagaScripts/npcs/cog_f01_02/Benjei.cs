   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Benjei : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1204;
        Name = "Benjei";
        StartX = -10228F;
        StartY = -24897F;
        StartZ = 624F;
        Startyaw = 9851;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
