using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class Davemaul : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f01";
        Type = 1279;
        Name = "Dave maul";
        StartX = 14818F;
        StartY = 43329F;
        StartZ = 9593F;
        Startyaw = 84432;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}