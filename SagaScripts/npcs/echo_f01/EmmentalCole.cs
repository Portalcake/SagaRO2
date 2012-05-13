using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace echo_f01
{
    public class EmmentalCole : Npc
    {
        public override void OnInit()
        {
            MapName = "echo_f01";
            Type = 1257;
            Name = "Emmental Cole";
            StartX = 18055F;
            StartY = -43464F;
            StartZ = -23377;
            Startyaw = 30584;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}