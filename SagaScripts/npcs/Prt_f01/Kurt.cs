using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Kurt : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1085;
            Name = "Kurt Stover";
            StartX = 9241F;
            StartY = 24076F;
            StartZ = 16313F;
            Startyaw = 88;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}