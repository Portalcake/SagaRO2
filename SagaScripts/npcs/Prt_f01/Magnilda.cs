using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Magnilda : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1077;
            Name = "Magnilda Haswell";
            StartX = 12480F;
            StartY = 79039F;
            StartZ = 5056;
            Startyaw = 92768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}