using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Roy : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1254;
            Name = "Roy";
            StartX = 14109F;
            StartY = -45047F;
            StartZ = -23379;
            Startyaw = 9896;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}