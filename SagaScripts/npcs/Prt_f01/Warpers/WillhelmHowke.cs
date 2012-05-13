using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class WillhelmHowke : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f01";
        Type = 1282;
        Name = "Willhelm Howke";
        StartX = 12092F;
        StartY = 101706F;
        StartZ = 4188F;
        Startyaw = 56776;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}