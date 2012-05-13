using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace rag2startzone_01
{
    public class Echlo : Npc
    {
        public override void OnInit()
        {
        MapName = "rag2startzone_01";
        Type = 1175;
        Name = "Echlo";
        StartX = -6624.0F;
        StartY = -11008.0F;
        StartZ = 96F;
        Startyaw = 14000;
        SetScript(3991);
        AddButton(Functions.EverydayConversation, new func(OnButton));
        }

    public void OnButton(ActorPC pc)
    {
        NPCChat(pc, 3994);
        }
    }
}