using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f03
{
    public class Parios : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f03";
            Type = 1154;
            Name = "Parios";
            StartX = -6368F;
            StartY = -30912F;
            StartZ = -163;
            Startyaw = 8168;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}