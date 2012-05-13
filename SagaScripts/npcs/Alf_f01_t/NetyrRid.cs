using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Netyr_Rid : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1018;
            Name = "Netyr_Rid";
            StartX = -6850F;
            StartY = 6306F;
            StartZ = 7641;
            Startyaw = -65880;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}