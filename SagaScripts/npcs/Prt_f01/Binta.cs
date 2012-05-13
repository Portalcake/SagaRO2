using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Binta : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1093;
            Name = "Binta Heaton";
            StartX = 13107F;
            StartY = 98984F;
            StartZ = 4193;
            Startyaw = 81968;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}