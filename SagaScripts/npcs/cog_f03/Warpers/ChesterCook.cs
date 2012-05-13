using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class ChesterCook : Npc
{
    public override void OnInit()
    {
        MapName = "cog_f03";
        Type = 1303;
        Name = "Chester Cook";
        StartX = 7550F;
        StartY = 30275F;
        StartZ = -7953F;
        Startyaw = 58920;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}