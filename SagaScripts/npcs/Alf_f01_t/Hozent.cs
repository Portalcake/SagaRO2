using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class Hozent : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1015;
            Name = "Hozent Byain";
            StartX = 2846F;
            StartY = 4737F;
            StartZ = 7703;
            Startyaw = -97016;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}