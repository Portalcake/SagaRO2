using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f03
{
    public class SeahIconium : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f03";
            Type = 1249;
            Name = "Seah Iconium";
            StartX = -28387F;
            StartY = 6923F;
            StartZ = -564;
            Startyaw = -544;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}