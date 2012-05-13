  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class ChrisSeidel : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1274;
        Name = "Chris Seidel";
        StartX = 3485F;
        StartY = 4884F;
        StartZ = -9812F;
        Startyaw = 26948;
        SetScript(5349);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 5350);
    }
}