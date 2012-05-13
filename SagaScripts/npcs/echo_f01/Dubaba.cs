using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class Dubaba : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1262;
            Name = "Dubaba";
            StartX = 15099F;
            StartY = -43583F;
            StartZ = -23331;
            Startyaw = -14528;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}