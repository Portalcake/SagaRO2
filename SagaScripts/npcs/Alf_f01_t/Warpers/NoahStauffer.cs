using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class NoahStauffer : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1307;
            Name = "Noah Stauffer";
            StartX = -6943F;
            StartY = -21892F;
            StartZ = 9528;
            Startyaw = -54952;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}