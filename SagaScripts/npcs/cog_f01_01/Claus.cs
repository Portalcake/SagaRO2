   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Claus : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1203;
        Name = "Claus";
        StartX = 7577.642F;
        StartY = -10922.93F;
        StartZ = 5267.399F;
        Startyaw = -21224;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
