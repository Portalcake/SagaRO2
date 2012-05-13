using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Vanda : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1070;
            Name = "Vanda Acton";
            StartX = 17344F;
            StartY = 92287F;
            StartZ = 5024;
            Startyaw = 84768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}