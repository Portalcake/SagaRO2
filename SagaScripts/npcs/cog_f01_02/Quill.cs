   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Quill : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1121;
        Name = "Quill Ramsden";
        StartX = 898F;
        StartY = -23003F;
        StartZ = 657F;
        Startyaw = 53849;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
