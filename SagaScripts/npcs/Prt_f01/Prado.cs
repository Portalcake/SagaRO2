using System;
using System.Collections.Generic;

using SagaMap;
 
using SagaDB.Actors;
using SagaDB.Items;

namespace Prt_f01
{
    public class Prado : Npc
    {
        public override void OnInit()
        {
            MapName = "Prt_f01";
            Type = 1094;
            Name = "Prado Alden";
            StartX = 14752F;
            StartY = 98984F;
            StartZ = 4193;
            Startyaw = 81968;
            SetScript(3);
            AddButton(Functions.EverydayConversation, new func(OnButton));
        }

        public void OnButton(ActorPC pc)
        {
            NPCChat(pc, 823);
        }

    }
}