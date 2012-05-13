   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Jacques : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1128;
        Name = "Jacques";
        StartX = -14037F;
        StartY = -21125F;
        StartZ = 462F;
        Startyaw = 9991;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
