using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Eglon8 : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1250;
            Name = "Eglon8";
            StartX = 2848F;
            StartY = 8478F;
            StartZ = 7982;
            Startyaw = 35697;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}