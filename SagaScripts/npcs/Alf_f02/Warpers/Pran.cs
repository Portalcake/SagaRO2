using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f02
{
    public class Pran : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f02";
            Type = 1311;
            Name = "Pran";
            StartX = -34413F;
            StartY = 40529F;
            StartZ = 1365;
            Startyaw = -14368;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}