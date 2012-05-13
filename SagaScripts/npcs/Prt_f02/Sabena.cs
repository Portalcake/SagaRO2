using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f02
{
    public class Sabena : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f02";
            Type = 1082;
            Name = "Sabena Denton";
            StartX = -24992F;
            StartY = -2176F;
            StartZ = -3218;
            Startyaw = 0;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}