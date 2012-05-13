using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class LetteEir : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1239;
            Name = "LetteEir";
            StartX = 5738F;
            StartY = 4560F;
            StartZ = 7618;
            Startyaw = -40640;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}