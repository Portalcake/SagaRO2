using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class GaborKlein : Npc
{
    public override void OnInit()
    {
        MapName = "Prt_f01";
        Type = 1281;
        Name = "Gabor Klein";
        StartX = 39252F;
        StartY = 82494F;
        StartZ = 6421F;
        Startyaw = 31184;
        SetScript(823);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 824);
    }
}