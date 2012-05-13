  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Tevez : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1226;
        Name = "Tevez West";
        StartX = -913F;
        StartY = -13993F;
        StartZ = -6464F;
        Startyaw = 6208;
        SetScript(4396);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        AddButton(Functions.Regenbogen);
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 4399);
    }
}