using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Emma : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1056;
            Name = "Emma Whitlef";
            StartX = 9503F;
            StartY = 85988F;
            StartZ = 4192;
            Startyaw = 32768;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
            AddButton(Functions.Regenbogen);
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}