  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class MartinJay : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1273;
        Name = "Martin Jay";
        StartX = 2698F;
        StartY = -12044F;
        StartZ = -6616F;
        Startyaw = 6760;
        SetScript(5349);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 5350);
    }
}