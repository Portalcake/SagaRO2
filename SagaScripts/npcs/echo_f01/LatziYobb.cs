using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class LatziYobb : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1263;
            Name = "Latzi Yobb";
            StartX = 14648F;
            StartY = -43152F;
            StartZ = -23341;
            Startyaw = 36304;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}