   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Bruger : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1192;
        Name = "Bruger";
        StartX = 6945.431F;
        StartY = -19532F;
        StartZ = 5728F;
        Startyaw = 11528;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
