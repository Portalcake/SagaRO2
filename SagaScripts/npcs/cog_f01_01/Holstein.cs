   //////////////////////////////////
  ///        Chii 2/12/07       ///
 ///      Cognito Npc-Pack      ///
//////////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Holstein : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f01_01";
        Type = 1211;
        Name = "Holstein";
        StartX = -9347.107F;
        StartY = -33926.07F;
        StartZ = 6050.417F;
        Startyaw = 27856;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 823);
    }
}
