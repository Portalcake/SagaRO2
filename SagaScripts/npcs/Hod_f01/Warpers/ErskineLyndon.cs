  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
using SagaDB.Actors;
using SagaDB.Items;

public class ErskineLyndon : Npc
{
    public override void OnInit()
    {
        MapName = "Hod_f01";
        Type = 1272;
        Name = "Erskine Lyndon";
        StartX = -11570.44F;
        StartY = -1312.455F;
        StartZ = -9316F;
        Startyaw = 47008;
        SetScript(5349);
        AddButton(Functions.EverydayConversation, new func(OnButton));
    }
    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 5350);
    }
}