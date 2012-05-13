using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Seraphina : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1069;
            Name = "Seraphina Falke";
            StartX = 17119F;
            StartY = 92288F;
            StartZ = 5024;
            Startyaw = 79768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}