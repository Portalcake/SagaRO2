  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

public class Ivo : Npc//Guard
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1062;
        Name = "Ivo Stover";
        StartX = -12520F;
        StartY = -3044F;
        StartZ = -9223F;
        Startyaw = 18000;
        SetScript(798);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 801);
    }

}