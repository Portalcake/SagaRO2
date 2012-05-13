using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class PritzLeodolf : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1265;
            Name = "Pritz Leodolf";
            StartX = 11624F;
            StartY = -39183F;
            StartZ = -23671;
            Startyaw = 18568;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}