using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Miu : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1156;
            Name = "Miu";
            StartX = 6592F;
            StartY = 92607F;
            StartZ = 4192;
            Startyaw = 56768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}