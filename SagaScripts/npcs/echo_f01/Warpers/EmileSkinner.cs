using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class EmileSkinner : Npc
{
    public override void OnInit()
    {
        MapName = "echo_f01";
        Type = 1306;
        Name = "Emile Skinner";
        StartX = 7461F;
        StartY = 15753F;
        StartZ = -24992F;
        Startyaw = 36320;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}