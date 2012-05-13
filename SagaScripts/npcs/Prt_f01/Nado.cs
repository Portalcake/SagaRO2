using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Nado : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1075;
            Name = "Nado Archibald";
            StartX = 14528F;
            StartY = 66464F;
            StartZ = 5024;
            Startyaw = 81768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}