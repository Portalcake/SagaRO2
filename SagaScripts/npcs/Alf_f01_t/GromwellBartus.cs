using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class GromwellBartus : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1246;
            Name = "GromwellBartus";
            StartX = -11597F;
            StartY = -20612F;
            StartZ = 9574;
            Startyaw = -40400;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}