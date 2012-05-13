  //////////////////////////////
 /// Npc Script Set by Chii ///
//////////////////////////////

using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace rag2startzone_01
{
    public class RaulDietrich : Npc
    {
        public override void OnInit()
        {
        MapName = "rag2startzone_01";
        Type = 1270;
        Name = "Raul Dietrich";
        StartX = -18240F;
        StartY = 5696F;
        StartZ = 96F;
        Startyaw = 16168;
        SetScript(5349);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 5350);
        }
    }
}