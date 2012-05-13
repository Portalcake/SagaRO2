using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace rag2startzone_01
{
    public class TiboPascal : Npc
    {
        public override void OnInit()
        {
        MapName = "rag2startzone_01";
        Type = 1271;
        Name = "Tibo Pascal";
        StartX = 1353F;
        StartY = 6920F;
        StartZ = 84F;
        Startyaw = -7344;
        SetScript(5349);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 5350);
	}
    }
}