using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class HelgiPhilippus : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1105;
            Name = "HelgiPhilippus";
            StartX = 859F;
            StartY = 4063F;
            StartZ = 7510;
            Startyaw = -89704;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}