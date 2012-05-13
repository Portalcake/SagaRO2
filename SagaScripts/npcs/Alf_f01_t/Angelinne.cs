using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Angelinne : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1245;
            Name = "Angelinne";
            StartX = -12805F;
            StartY = -13255F;
            StartZ = 9504;
            Startyaw = 37592;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}