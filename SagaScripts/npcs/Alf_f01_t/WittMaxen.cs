using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class WittMaxen : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1244;
            Name = "WittMaxen";
            StartX = -12796F;
            StartY = -13430F;
            StartZ = 9520;
            Startyaw = 30424;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}