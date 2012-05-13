using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Irpa : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1261;
            Name = "Irpa";
            StartX = 15898F;
            StartY = -44780F;
            StartZ = -23313;
            Startyaw = 15536;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}