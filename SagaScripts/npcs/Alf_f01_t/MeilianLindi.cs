using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class MeilianLindi : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1240;
            Name = "MeilianLindi";
            StartX = 5742F;
            StartY = 4860F;
            StartZ = 7626;
            Startyaw = -20336;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}