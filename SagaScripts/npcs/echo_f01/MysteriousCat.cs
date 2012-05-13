using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class MysteriousCat : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1256;
            Name = "Mysterious Cat";
            StartX = -39545F;
            StartY = 21254F;
            StartZ = -21981;
            Startyaw = 18616;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}