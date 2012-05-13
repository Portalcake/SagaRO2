using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Alf_f01_t
{
    public class MessahGath : Npc
    {
        public override void OnInit()
        {
            MapName = "Alf_f01_t";
            Type = 1111;
            Name = "MessahGath";
            StartX = -21322F;
            StartY = -21779F;
            StartZ = 9505;
            Startyaw = 0;
            SetScript(823);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 824);
        }

    }
}