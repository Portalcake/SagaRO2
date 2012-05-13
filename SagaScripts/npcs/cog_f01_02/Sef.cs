   //////////////////////////////////
  ///        Chii 21/11/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Sef : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_02";
        Type = 1176;
        Name = "Sef Coward";
        StartX = 1375F;
        StartY = -8864F;
        StartZ = -454F;
        Startyaw = 41713;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
