using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class EliasRasch : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1308;
            Name = "Elias Rasch";
            StartX = 2723F;
            StartY = 419F;
            StartZ = 7560;
            Startyaw = -55032;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}