using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class HoondMakkus : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1242;
            Name = "HoondMakkus";
            StartX = -4865F;
            StartY = -22991F;
            StartZ = 9812;
            Startyaw = -46952;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}